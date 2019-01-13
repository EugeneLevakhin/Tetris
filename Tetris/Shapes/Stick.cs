using System;
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
            int xPos = ((int)canvas.Width / (int)_itemSize / 2) * (int)_itemSize;
            double yPos = -110;

            for (int i = 0; i < 4; i++)
            {
                Border item = new Border();
                item.Width = _itemSize;
                item.Height = _itemSize;
                item.Background = Brushes.Yellow;
                item.CornerRadius = new System.Windows.CornerRadius(2);

                canvas.Children.Add(item);
                Items.Add(item);

                Canvas.SetLeft(item, xPos);
                Canvas.SetTop(item, yPos);
                yPos += _itemSize;
            }
        }

        public override void Rotate()
        {
            // TODO
        }
    }
}