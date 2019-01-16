﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris.Shapes
{
    class Stick : Shape
    {
        public Stick(Canvas canvas)
        {
            double xPos = 3 * _itemSize;
            double yPos = -20;

            for (int i = 0; i < 4; i++)
            {
                base.LocateShapeItemOnCanvas(canvas, xPos, yPos);

                xPos += _itemSize;
            }
        }

        public override void Rotate()
        {
            // TODO
        }
    }
}