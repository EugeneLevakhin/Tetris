using System.Windows;
using System.Windows.Controls;
using Tetris.Shapes;

namespace Tetris.Entities.Shapes
{
    class Square : Shape
    {
        public Square(Canvas canvas)
        {
            double xPos = 4 * _itemSize;
            double yPos = -50;

            for (int i = 0; i < 2; i++)
            {
                xPos = 4 * _itemSize;

                for (int j = 0; j < 2; j++)
                {
                    base.LocateShapeItemOnCanvas(canvas, xPos, yPos);

                    xPos += _itemSize;
                }
                yPos += _itemSize;
            }

            CenterPoint = new Point(xPos - _itemSize, yPos - _itemSize); // TODO: if need
        }
    }
}