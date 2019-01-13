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
            _currrentShape.MoveDown();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                _currrentShape.MoveLeft();
            }
            else if (e.Key == Key.Right)
            {
                _currrentShape.MoveRight();
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

        private void Menu_NewGame_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuPause_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}