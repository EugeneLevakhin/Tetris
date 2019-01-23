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
                if (_currentShape.IsStacked(canvas)) // null reference exception thrown here, when game over
                {
                    RemoveFullLinesIfEnabled();

                    if (IsCanvasOverflow())
                    {
                        gameOverTxtBlk.Text = "GAME OVER";
                        _gameStarted = false;
                        _gameTimer.Stop();
                        return;
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

            if (e.Key == Key.Pause)
            {
                SwitchPause();
                return;
            }

            if (_pause) return;

            lock (this)
            {
                if (e.Key == Key.Left)
                {
                    _currentShape.MoveLeft(canvas);
                }
                else if (e.Key == Key.Right)
                {
                    _currentShape.MoveRight(canvas);
                }
                else if (e.Key == Key.Down)
                {
                    if (_gameTimer.Interval != 20) _gameTimer.Interval = 20;
                }
                else if (e.Key == Key.Space)
                {
                    _currentShape.Rotate(canvas);
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

        private bool IsCanvasOverflow()
        {
            foreach (Border itemOfCanvas in canvas.Children)
            {
                double topOfCanvasItem = Canvas.GetTop(itemOfCanvas);

                if (topOfCanvasItem <= 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void RemoveFullLinesIfEnabled()
        {
            Dictionary<double, List<Border>> itemsDictionary = new Dictionary<double, List<Border>>();

            // just initialize
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