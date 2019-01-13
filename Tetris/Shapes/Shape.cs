using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Tetris.Shapes
{
    abstract class Shape
    {
        private double _moveStep;
        protected double _itemSize;

        public List<Border> Items { get; }

        public Shape()
        {
            _moveStep = 10;
            _itemSize = 30;

            Items = new List<Border>();
        }

        public virtual void Rotate()
        {
        }

        public void MoveDown()
        {
            try
            {
                foreach (var item in Items)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        double previosTop = Canvas.GetTop(item);
                        Canvas.SetTop(item, previosTop + _moveStep);
                    });
                }
            }
            catch (Exception)
            {
            }
        }

        public void MoveLeft()
        {
            foreach (var item in Items)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    double previosLeft = Canvas.GetLeft(item);
                    Canvas.SetLeft(item, previosLeft - _itemSize);
                });
            }
        }

        public void MoveRight()
        {
            foreach (var item in Items)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    double previosLeft = Canvas.GetLeft(item);
                    Canvas.SetLeft(item, previosLeft + _itemSize);
                });
            }
        }
    }
}