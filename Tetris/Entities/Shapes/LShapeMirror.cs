﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tetris.Shapes;

namespace Tetris.Entities.Shapes
{
    class LShapeMirror : Shape
    {
        public LShapeMirror(Canvas canvas)
        {
            double xPos = 5 * _itemSize;
            double yPos = -80;

            if (canvas.Name == "previewCanvas")
            {
                xPos = _itemSize * 2;
                yPos = 0;
            }

            for (int i = 0; i < 3; i++)
            {
                base.LocateShapeItemOnCanvas(canvas, xPos, yPos, Brushes.Yellow);
                yPos += _itemSize;
            }
            yPos -= _itemSize;

            CenterPoint = new Point(xPos, yPos);

            base.LocateShapeItemOnCanvas(canvas, xPos - _itemSize, yPos, Brushes.Yellow);
        }
    }
}