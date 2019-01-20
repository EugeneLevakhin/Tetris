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

        protected void LocateShapeItemOnCanvas(Canvas canvas, double xPos, double yPos, Brush color)
        {
            Border item = new Border();
            item.Width = _itemSize;
            item.Height = _itemSize;
            item.Background = color;
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
                Canvas.SetTop(item, previosTop + _moveStep);
            }

            CenterPoint = new Point(CenterPoint.X, CenterPoint.Y + _moveStep);
        }

        public void MoveLeft()
        {
            foreach (var item in Items)
            {
                double previosLeft = Canvas.GetLeft(item);
                Canvas.SetLeft(item, previosLeft - _itemSize);
            }

            CenterPoint = new Point(CenterPoint.X - _itemSize, CenterPoint.Y);
        }

        public void MoveRight()
        {
            foreach (var item in Items)
            {
                double previosLeft = Canvas.GetLeft(item);
                Canvas.SetLeft(item, previosLeft + _itemSize);
            }

            CenterPoint = new Point(CenterPoint.X + _itemSize, CenterPoint.Y);
        }

        public virtual void Rotate()
        {
            foreach (var item in Items)
            {
                Point itemCoord = new Point(Canvas.GetLeft(item), Canvas.GetTop(item));
                Point relativeCanvasCenterPoint = new Point(itemCoord.X - CenterPoint.X, itemCoord.Y - CenterPoint.Y);

                Point relativeCanvasCenterPointRotated = new Point(relativeCanvasCenterPoint.Y, -relativeCanvasCenterPoint.X); // rotate

                double x = relativeCanvasCenterPointRotated.X + CenterPoint.X;
                double y = relativeCanvasCenterPointRotated.Y + CenterPoint.Y;

                Canvas.SetLeft(item, x);
                Canvas.SetTop(item, y - _itemSize);
            }
        }
    }
}