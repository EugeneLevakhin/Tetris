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

        public void MoveLeft(Canvas canvas)
        {
            if (!IsLeftMoveAllowed(canvas)) return;

            foreach (var item in Items)
            {
                double previosLeft = Canvas.GetLeft(item);
                Canvas.SetLeft(item, previosLeft - _itemSize);
            }

            CenterPoint = new Point(CenterPoint.X - _itemSize, CenterPoint.Y);
        }

        public void MoveRight(Canvas canvas)
        {
            if (!IsRightMoveAllowed(canvas)) return;

            foreach (var item in Items)
            {
                double previosLeft = Canvas.GetLeft(item);
                Canvas.SetLeft(item, previosLeft + _itemSize);
            }

            CenterPoint = new Point(CenterPoint.X + _itemSize, CenterPoint.Y);
        }

        public virtual void Rotate(Canvas canvas)
        {
            // save shape position before rotate
            List<Point> originalShapePoints = new List<Point>();

            foreach (var item in Items)
            {
                originalShapePoints.Add(new Point(Canvas.GetLeft(item), Canvas.GetTop(item)));
            }

            // rotate and hide shape
            foreach (var item in Items)
            {
                Point itemCoord = new Point(Canvas.GetLeft(item), Canvas.GetTop(item));
                Point relativeCanvasCenterPoint = new Point(itemCoord.X - CenterPoint.X, itemCoord.Y - CenterPoint.Y);

                Point relativeCanvasCenterPointRotated = new Point(relativeCanvasCenterPoint.Y, -relativeCanvasCenterPoint.X); // rotate

                double x = relativeCanvasCenterPointRotated.X + CenterPoint.X;
                double y = relativeCanvasCenterPointRotated.Y + CenterPoint.Y;

                Canvas.SetLeft(item, x);
                Canvas.SetTop(item, y - _itemSize);

                item.Visibility = Visibility.Hidden;
            }

            if (IsCollisionOccured(this, canvas))
            {
                bool collisionOccured = true;

                if (IsLeftMoveAllowed(canvas))
                {
                    MoveLeft(canvas);
                    collisionOccured = false;

                    if (IsCollisionOccured(this, canvas))
                    {
                        collisionOccured = true;

                        if (IsLeftMoveAllowed(canvas))
                        {
                            MoveLeft(canvas);
                            collisionOccured = false;
                        }
                    }
                }
                else if (IsRightMoveAllowed(canvas))
                {
                    MoveRight(canvas);
                    collisionOccured = false;

                    if (IsCollisionOccured(this, canvas))
                    {
                        collisionOccured = true;

                        if (IsRightMoveAllowed(canvas))
                        {
                            MoveRight(canvas);
                            collisionOccured = false;
                        }
                    }
                }
                if (collisionOccured)
                {
                    // restore initial shape position if rotate not allowed
                    for (int i = 0; i < Items.Count; i++)
                    {
                        Canvas.SetLeft(Items[i], originalShapePoints[i].X);
                        Canvas.SetTop(Items[i], originalShapePoints[i].Y);
                    }
                }
            }

            // show shape
            foreach (var item in Items)
            {
                item.Visibility = Visibility.Visible;
            }
        }

        private bool IsLeftMoveAllowed(Canvas canvas)
        {
            foreach (var itemOfCurrentShape in Items)
            {
                double leftOfItemOfCurrentShape = Canvas.GetLeft(itemOfCurrentShape);
                double topOfItemOfCurrentShape = Canvas.GetTop(itemOfCurrentShape);
                double bottomOfItemOfCurrentShape = topOfItemOfCurrentShape + _itemSize;

                if (leftOfItemOfCurrentShape == 0)  // if left edge of canvas
                {
                    return false;
                }
                else
                {
                    foreach (Border itemOfCanvas in canvas.Children)
                    {
                        double topOfCanvasItem = Canvas.GetTop(itemOfCanvas);
                        double bottomOfCanvasItem = topOfCanvasItem + _itemSize;
                        double rightOfCanvasItem = Canvas.GetLeft(itemOfCanvas) + _itemSize;

                        if (IsItemOfCurrentShape(itemOfCanvas))  // if self
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

        private bool IsRightMoveAllowed(Canvas canvas)
        {
            foreach (var itemOfCurrentShape in Items)
            {
                double rightOfItemOfCurrentShape = Canvas.GetLeft(itemOfCurrentShape) + _itemSize;
                double topOfItemOfCurrentShape = Canvas.GetTop(itemOfCurrentShape);
                double bottomOfItemOfCurrentShape = topOfItemOfCurrentShape + _itemSize;

                if (rightOfItemOfCurrentShape == canvas.Width)  // if left edge of canvas
                {
                    return false;
                }
                else
                {
                    foreach (Border itemOfCanvas in canvas.Children)
                    {
                        double topOfCanvasItem = Canvas.GetTop(itemOfCanvas);
                        double bottomOfCanvasItem = topOfCanvasItem + _itemSize;
                        double leftOfCanvasItem = Canvas.GetLeft(itemOfCanvas);

                        if (IsItemOfCurrentShape(itemOfCanvas))  // if self
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

        public bool IsStacked(Canvas canvas)
        {
            foreach (var itemOfCurrentShape in Items)
            {
                double bottomOfItemOfCurrentShape = Canvas.GetTop(itemOfCurrentShape) + _itemSize;
                double leftOfItemOfCurrentShape = Canvas.GetLeft(itemOfCurrentShape);

                if (bottomOfItemOfCurrentShape == canvas.Height)  // if bottom edge of canvas
                {
                    return true;
                }
                else
                {
                    foreach (Border itemOfCanvas in canvas.Children)
                    {
                        double topOfCanvasItem = Canvas.GetTop(itemOfCanvas);
                        double leftOfCanvasItem = Canvas.GetLeft(itemOfCanvas);

                        if (IsItemOfCurrentShape(itemOfCanvas))  // if self
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
            double topOfItem = Canvas.GetTop(item);
            double leftOfItem = Canvas.GetLeft(item);

            foreach (var itemOfCurrentShape in Items)
            {
                if (item.Equals(itemOfCurrentShape)) return true;

                //double topOfItemOfCurrentShape = Canvas.GetTop(itemOfCurrentShape);
                //double leftOfItemOfCurrentShape = Canvas.GetLeft(itemOfCurrentShape);

                //if (topOfItem == topOfItemOfCurrentShape && leftOfItem == leftOfItemOfCurrentShape)
                //{
                //    return true;
                //}
            }
            return false;
        }

        // TODO : review
        private bool IsCollisionOccured(Shape shape, Canvas canvas)
        {
            foreach (Border itemOfShape in shape.Items)
            {
                Point shapeItemCoord = new Point(Canvas.GetLeft(itemOfShape), Canvas.GetTop(itemOfShape));

                // if item outside canvas
                if (shapeItemCoord.X < 0 || shapeItemCoord.X > canvas.Width - _itemSize || shapeItemCoord.Y + _itemSize > canvas.Height)
                {
                    return true;
                }

                foreach (Border itemOfCanvas in canvas.Children)
                {
                    if (IsItemOfCurrentShape(itemOfCanvas)) continue;

                    Point canvasItemCoord = new Point(Canvas.GetLeft(itemOfCanvas), Canvas.GetTop(itemOfCanvas));

                    if (shapeItemCoord.X >= canvasItemCoord.X + _itemSize || shapeItemCoord.X + _itemSize <= canvasItemCoord.X
                        || shapeItemCoord.Y >= canvasItemCoord.Y + _itemSize || shapeItemCoord.Y + _itemSize <= canvasItemCoord.Y)
                    {
                        continue;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}