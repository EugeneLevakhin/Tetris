using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Tetris.Shapes
{
    class LShape : Shape
    {
        public LShape(Canvas canvas)
        {
            double yPos = -50;
            double xPos = 3 * _itemSize; // TODO : 3 or 4 sizes

            base.LocateShapeItemOnCanvas(canvas, xPos, yPos);
            yPos += _itemSize;

            for (int i = 0; i < 3; i++)
            {
                base.LocateShapeItemOnCanvas(canvas, xPos, yPos);

                xPos += _itemSize;
            }
        }
    }
}