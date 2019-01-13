using System;
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
                int xPos = ((int)canvas.Width / (int)_itemSize / 2) * (int)_itemSize - (int)_itemSize;

                for (int j = 0; j < 2; j++)
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
                    xPos += (int)_itemSize;
                }
                yPos += 30;
            }
        }
    }
}