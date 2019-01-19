using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;

namespace Tetris.Shapes
{
    abstract class Shape
    {
        private double _moveStep;
        protected double _itemSize;

        public List<Border> Items { get; }
        public Point CenterPoint { get; set; }

        public Shape()
        {
            _moveStep = 10;
            _itemSize = 30;

            Items = new List<Border>();
        }

        protected void LocateShapeItemOnCanvas(Canvas canvas, double xPos, double yPos)
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
            foreach (var item in Items)
            {
                double previosTop = Canvas.GetTop(item);
                CenterPoint = new Point(CenterPoint.X, CenterPoint.Y - _moveStep);
                Canvas.SetTop(item, previosTop + _moveStep);
            }
        }

        public void MoveLeft()
        {
            foreach (var item in Items)
            {
                double previosLeft = Canvas.GetLeft(item);
                CenterPoint = new Point(CenterPoint.X - _itemSize, CenterPoint.Y);
                Canvas.SetLeft(item, previosLeft - _itemSize);
            }
        }

        public void MoveRight()
        {
            foreach (var item in Items)
            {
                double previosLeft = Canvas.GetLeft(item);
                CenterPoint = new Point(CenterPoint.X + _itemSize, CenterPoint.Y);
                Canvas.SetLeft(item, previosLeft + _itemSize);
            }
        }

        public virtual void Rotate()
        {
        }
    }
}