using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        public void LocateShapeItemOnCanvas(Canvas canvas, double xPos, double yPos)
        {
            Border item = new Border();
            item.Width = _itemSize;
            item.Height = _itemSize;
            item.Background = Brushes.Yellow;
            item.CornerRadius = new CornerRadius(2);

            canvas.Children.Add(item);
            Items.Add(item);

            Canvas.SetLeft(item, xPos);
            Canvas.SetTop(item, yPos);
        }

        public void MoveDown()
        {
            try         // when Application shutdown, can be exception here
            {
                foreach (var item in Items)
                {
                    double previosTop = Canvas.GetTop(item);
                    Canvas.SetTop(item, previosTop + _moveStep);
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
                double previosLeft = Canvas.GetLeft(item);
                Canvas.SetLeft(item, previosLeft - _itemSize);
            }
        }

        public void MoveRight()
        {
            foreach (var item in Items)
            {
                double previosLeft = Canvas.GetLeft(item);
                Canvas.SetLeft(item, previosLeft + _itemSize);
            }
        }

        public virtual void Rotate()
        {
        }
    }
}