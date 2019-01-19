﻿using System.Windows;
using System.Windows.Controls;
using Tetris.Shapes;

namespace Tetris.Entities.Shapes
{
    class ZShapeMirror : Shape
    {
        public ZShapeMirror(Canvas canvas)
        {
            double xPos = 4 * _itemSize;
            double yPos = -80;

            for (int i = 0; i < 2; i++)
            {
                base.LocateShapeItemOnCanvas(canvas, xPos, yPos);
                yPos += _itemSize;
                base.LocateShapeItemOnCanvas(canvas, xPos, yPos);
                xPos += _itemSize;
            }

            CenterPoint = new Point(xPos - _itemSize, yPos);
        }
    }
}