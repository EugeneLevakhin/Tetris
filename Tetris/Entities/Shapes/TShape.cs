using System.Windows;
using System.Windows.Controls;
using Tetris.Shapes;

namespace Tetris.Entities.Shapes
{
    class TShape : Shape
    {
        public TShape(Canvas canvas)
        {
            double xPos = 5 * _itemSize;
            double yPos = -80;

            for (int i = 0; i < 3; i++)
            {
                base.LocateShapeItemOnCanvas(canvas, xPos, yPos);

                yPos += _itemSize;
            }
            xPos -= _itemSize;
            yPos -= (2 * _itemSize);
            base.LocateShapeItemOnCanvas(canvas, xPos, yPos);

            CenterPoint = new Point(xPos + _itemSize / 2, yPos + _itemSize / 2);
        }
    }
}