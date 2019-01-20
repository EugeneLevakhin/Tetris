using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tetris.Shapes;

namespace Tetris.Entities.Shapes
{
    class Stick : Shape
    {
        public Stick(Canvas canvas)
        {
            double xPos = 3 * _itemSize;
            double yPos = -20;

            for (int i = 0; i < 4; i++)
            {
                base.LocateShapeItemOnCanvas(canvas, xPos, yPos, Brushes.Green);

                xPos += _itemSize;
            }

            CenterPoint = new Point(xPos - _itemSize - _itemSize / 2, yPos + _itemSize / 2);
        }
    }
}