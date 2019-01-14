using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tetris.Shapes;
using Shape = Tetris.Shapes.Shape;

namespace Tetris
{
    // TODO : add all shapes
    // TODO : Rotate shapes if it is possible 
    // TODO : Remove lines, counting score, reduce timer interval
    // TODO : End game
    // TODO : pause and new game
    // TODO : change 30 literal (try GetBottom, GetRight)
    // TODO : optimization

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer _gameTimer;
        private Shape _currrentShape;

        public MainWindow()
        {
            InitializeComponent();

            _currrentShape = new Square(canvas);

            _gameTimer = new Timer(200);
            _gameTimer.Elapsed += _gameTimer_Elapsed;
            _gameTimer.Start();
        }

        private void _gameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (IsCurrentShapeStacked())
                {
                    _currrentShape = new Stick(canvas);
                }
                else
                {
                    _currrentShape.MoveDown();
                }
            });
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                if (IsLeftMoveAllowed()) _currrentShape.MoveLeft();
            }
            else if (e.Key == Key.Right)
            {
                if (IsRightMoveAllowed()) _currrentShape.MoveRight();
            }
            else if (e.Key == Key.Down)
            {
                if (_gameTimer.Interval != 20) _gameTimer.Interval = 20;
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
            foreach (var itemOfCurrentShape in _currrentShape.Items)
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
            foreach (var itemOfCurrentShape in _currrentShape.Items)
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

        private bool IsCurrentShapeStacked()
        {
            foreach (var itemOfCurrentShape in _currrentShape.Items)
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
            double topOfItem = Canvas.GetTop((Border)item);
            double leftOfItem = Canvas.GetLeft((Border)item);

            foreach (var itemOfCurrentShape in _currrentShape.Items)
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

        private void Menu_NewGame_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MenuPause_Click(object sender, RoutedEventArgs e)
        {

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