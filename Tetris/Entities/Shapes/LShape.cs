using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Tetris.Shapes;

namespace Tetris.Entities.Shapes
{
    class LShape : Shape
    {
        public LShape(Canvas canvas)
        {
            double xPos = 4 * _itemSize;
            double yPos = -80;

            if (canvas.Name == "previewCanvas")
            {
                xPos = _itemSize;
                yPos = 0;
            }

            for (int i = 0; i < 3; i++)
            {
                base.LocateShapeItemOnCanvas(canvas, xPos, yPos, Brushes.Orange);
                yPos += _itemSize;
            }
            yPos -= _itemSize;
            xPos += _itemSize;

            CenterPoint = new Point(xPos, yPos);

            base.LocateShapeItemOnCanvas(canvas, xPos, yPos, Brushes.Orange);
        }
    }
}