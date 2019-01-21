using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tetris.Entities;
using Shape = Tetris.Shapes.Shape;

namespace Tetris
{
    // TODO : Rotate shapes if it is possible 
    // TODO : counting score, reduce timer interval
    // TODO : Additional preview canvas
    // TODO : refactoring and optimization (change 30 literal (try GetBottom, GetRight))

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer _gameTimer;
        private Shape _currentShape;
        private bool _gameStarted;
        private bool _pause;

        public MainWindow()
        {
            InitializeComponent();

            _gameTimer = new Timer(200);
            _gameTimer.Elapsed += _gameTimer_Elapsed;
        }

        private void _gameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _gameTimer.Stop();

            Dispatcher.Invoke(() =>
            {
                if (IsSetOfBordersStacked(_currentShape.Items.ToArray()))
                {
                    _currentShape = null;

                    RemoveFullLinesIfEnabled();

                    if (IsCanvasOverflow())
                    {
                        gameOverTxtBlk.Text = "GAME OVER";
                        _gameStarted = false;
                        _gameTimer.Stop();
                    }
                    else
                    {
                        _currentShape = ShapesFactory.CreateShape(canvas);
                    }
                }
                else
                {
                    try     // when Application shutdown, can be exception here
                    {
                        // method works in another thread
                        lock (this) { _currentShape.MoveDown(); }
                    }
                    catch (Exception)
                    {

                    }
                }
            });

            _gameTimer.Start();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_gameStarted) return;

            lock (this)
            {
                if (e.Key == Key.Left)
                {
                    if (IsLeftMoveAllowed()) _currentShape.MoveLeft();
                }
                else if (e.Key == Key.Right)
                {
                    if (IsRightMoveAllowed()) _currentShape.MoveRight();
                }
                else if (e.Key == Key.Down)
                {
                    if (_gameTimer.Interval != 20) _gameTimer.Interval = 20;
                }
                else if (e.Key == Key.Space)
                {
                    _currentShape.Rotate();
                }
                else if (e.Key == Key.Pause)
                {
                    SwitchPause();
                }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                _gameTimer.Interval = 200;
            }
        }

        private bool IsLeftMoveAllowed()
        {
            foreach (var itemOfCurrentShape in _currentShape.Items)
            {
                double leftOfItemOfCurrentShape = Canvas.GetLeft(itemOfCurrentShape);
                double topOfItemOfCurrentShape = Canvas.GetTop(itemOfCurrentShape);
                double bottomOfItemOfCurrentShape = topOfItemOfCurrentShape + 30;

                if (leftOfItemOfCurrentShape == 0)  // if left edge of canvas
                {
                    return false;
                }
                else
                {
                    foreach (var itemOfCanvas in canvas.Children)
                    {
                        double topOfCanvasItem = Canvas.GetTop((Border)itemOfCanvas);
                        double bottomOfCanvasItem = topOfCanvasItem + 30;
                        double rightOfCanvasItem = Canvas.GetLeft((Border)itemOfCanvas) + 30;

                        if (IsItemOfCurrentShape((Border)itemOfCanvas))  // if self
                        {
                            continue;
                        }
                        else if (leftOfItemOfCurrentShape == rightOfCanvasItem &&
                                    (
                                        (topOfItemOfCurrentShape > topOfCanvasItem && topOfItemOfCurrentShape < bottomOfCanvasItem)
                                        || (bottomOfItemOfCurrentShape > topOfCanvasItem && bottomOfItemOfCurrentShape < bottomOfCanvasItem)
                                        || (leftOfItemOfCurrentShape == rightOfCanvasItem && topOfItemOfCurrentShape == topOfCanvasItem)
                                    )
                                )
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool IsRightMoveAllowed()
        {
            foreach (var itemOfCurrentShape in _currentShape.Items)
            {
                double rightOfItemOfCurrentShape = Canvas.GetLeft(itemOfCurrentShape) + 30;
                double topOfItemOfCurrentShape = Canvas.GetTop(itemOfCurrentShape);
                double bottomOfItemOfCurrentShape = topOfItemOfCurrentShape + 30;

                if (rightOfItemOfCurrentShape == canvas.Width)  // if left edge of canvas
                {
                    return false;
                }
                else
                {
                    foreach (var itemOfCanvas in canvas.Children)
                    {
                        double topOfCanvasItem = Canvas.GetTop((Border)itemOfCanvas);
                        double bottomOfCanvasItem = topOfCanvasItem + 30;
                        double leftOfCanvasItem = Canvas.GetLeft((Border)itemOfCanvas);

                        if (IsItemOfCurrentShape((Border)itemOfCanvas))  // if self
                        {
                            continue;
                        }
                        else if (rightOfItemOfCurrentShape == leftOfCanvasItem &&
                                    (
                                        (topOfItemOfCurrentShape > topOfCanvasItem && topOfItemOfCurrentShape < bottomOfCanvasItem)
                                        || (bottomOfItemOfCurrentShape > topOfCanvasItem && bottomOfItemOfCurrentShape < bottomOfCanvasItem)
                                        || (rightOfItemOfCurrentShape == leftOfCanvasItem && topOfItemOfCurrentShape == topOfCanvasItem)
                                    )
                                )
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool IsSetOfBordersStacked(params Border[] borders)
        {
            foreach (var itemOfCurrentShape in borders)
            {
                double bottomOfItemOfCurrentShape = Canvas.GetTop(itemOfCurrentShape) + 30;
                double leftOfItemOfCurrentShape = Canvas.GetLeft(itemOfCurrentShape);

                if (bottomOfItemOfCurrentShape == canvas.Height)  // if bottom edge of canvas
                {
                    return true;
                }
                else
                {
                    foreach (var itemOfCanvas in canvas.Children)
                    {
                        double topOfCanvasItem = Canvas.GetTop((Border)itemOfCanvas);
                        double leftOfCanvasItem = Canvas.GetLeft((Border)itemOfCanvas);

                        if (IsItemOfCurrentShape((Border)itemOfCanvas))  // if self
                        {
                            continue;
                        }
                        else if (bottomOfItemOfCurrentShape == topOfCanvasItem && leftOfItemOfCurrentShape == leftOfCanvasItem)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsItemOfCurrentShape(Border item)
        {
            if (_currentShape == null) return false;

            double topOfItem = Canvas.GetTop((Border)item);
            double leftOfItem = Canvas.GetLeft((Border)item);

            foreach (var itemOfCurrentShape in _currentShape.Items)
            {
                double topOfItemOfCurrentShape = Canvas.GetTop(itemOfCurrentShape);
                double leftOfItemOfCurrentShape = Canvas.GetLeft(itemOfCurrentShape);

                if (topOfItem == topOfItemOfCurrentShape && leftOfItem == leftOfItemOfCurrentShape)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsCanvasOverflow()
        {
            foreach (var itemOfCanvas in canvas.Children)
            {
                double topOfCanvasItem = Canvas.GetTop((Border)itemOfCanvas);

                if (topOfCanvasItem < 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void RemoveFullLinesIfEnabled()
        {
            Dictionary<double, List<Border>> itemsDictionary = new Dictionary<double, List<Border>>();

            for (double i = 0; i < canvas.Height; i += 30)
            {
                itemsDictionary.Add(i, new List<Border>());
            }



            foreach (Border item in canvas.Children)
            {
                double key = Canvas.GetTop(item);
                if (itemsDictionary.ContainsKey(key)) itemsDictionary[key].Add(item);
            }

            int countRemovedLines = 0;

            double lowestRemovedLineTopCoord = 0;
            // remove lines
            foreach (var dicItem in itemsDictionary)
            {
                if (dicItem.Value.Count >= 10)
                {
                    if (dicItem.Key > lowestRemovedLineTopCoord) lowestRemovedLineTopCoord = dicItem.Key;

                    foreach (var border in dicItem.Value)
                    {
                        canvas.Children.Remove(border);
                    }
                    countRemovedLines++;
                }
            }

            if (countRemovedLines > 0) FallAllHangingItems(countRemovedLines, lowestRemovedLineTopCoord);
        }

        private void FallAllHangingItems(int countRemovedLines, double lowestRemovedLineTopCoord)
        {
            for (int i = 0; i < countRemovedLines; i++)
            {
                foreach (Border item in canvas.Children)
                {
                    double top = Canvas.GetTop(item);

                    if (top < lowestRemovedLineTopCoord)
                    {
                        Canvas.SetTop(item, top + 30);
                    }
                }
            }


            //bool fallingOccured;

            //do
            //{
            //    fallingOccured = false;

            //    foreach (Border item in canvas.Children)
            //    {
            //        while (!IsSetOfBordersStacked(item))
            //        {
            //            double top = Canvas.GetTop(item);
            //            Canvas.SetTop(item, top + 30);
            //            fallingOccured = true;
            //        }
            //    }

            //} while (fallingOccured);
        }

        private void Menu_NewGame_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            _currentShape = ShapesFactory.CreateShape(canvas);
            _gameTimer.Start();
            _gameStarted = true;
            _pause = false;
            gameOverTxtBlk.Text = string.Empty;
        }

        private void MenuPause_Click(object sender, RoutedEventArgs e)
        {
            SwitchPause();
        }

        private void SwitchPause()
        {
            if (!_gameStarted) return;

            if (_pause)
            {
                _gameTimer.Start();
            }
            else
            {
                _gameTimer.Stop();
            }
            _pause = !_pause;
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            _gameTimer.Stop();
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}