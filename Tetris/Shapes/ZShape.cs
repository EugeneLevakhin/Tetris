using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tetris.Shapes
{
    class ZShape : Shape
    {
        public ZShape(Canvas canvas)
        {
            double xPos = 3 * _itemSize; // TODO : 3 or 4 sizes
            double yPos = -50;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    base.LocateShapeItemOnCanvas(canvas, xPos, yPos);

                    xPos += _itemSize;
                }
                xPos -= _itemSize;
                yPos += _itemSize;
            }
        }
    }
}