using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tetris.Shapes;

namespace Tetris.Entities.Shapes
{
    class ZShape : Shape
    {
        public ZShape(Canvas canvas)
        {
            double xPos = 5 * _itemSize;
            double yPos = -80;

            for (int i = 0; i < 2; i++)
            {
                base.LocateShapeItemOnCanvas(canvas, xPos, yPos, Brushes.HotPink);
                yPos += _itemSize;
                base.LocateShapeItemOnCanvas(canvas, xPos, yPos, Brushes.HotPink);
                xPos -= _itemSize;
            }

            CenterPoint = new Point(xPos + 2 * _itemSize, yPos);
        }
    }
}