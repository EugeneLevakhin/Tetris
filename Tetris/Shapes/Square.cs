﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tetris.Shapes
{
    class Square : Shape
    {
        public Square(Canvas canvas)
        {
            double yPos = -50;

            for (int i = 0; i < 2; i++)
            {
                double xPos = 4 * _itemSize;

                for (int j = 0; j < 2; j++)
                {
                    base.LocateShapeItemOnCanvas(canvas, xPos, yPos);

                    xPos += _itemSize;
                }
                yPos += _itemSize;
            }
        }
    }
}