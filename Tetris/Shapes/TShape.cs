using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris.Shapes
{
    class TShape : Shape
    {
        public TShape(Canvas canvas)
        {
            double yPos = -50;
            double xPos = 3 * _itemSize; // TODO : 3 or 4 sizes

            for (int i = 0; i < 3; i++)
            {
                base.LocateShapeItemOnCanvas(canvas, xPos, yPos);

                xPos += _itemSize;
            }
            base.LocateShapeItemOnCanvas(canvas, xPos - (2 * _itemSize), yPos += _itemSize);
        }
    }
}