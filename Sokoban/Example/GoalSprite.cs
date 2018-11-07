using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class GoalSprite:ImageSprite
    {
        public GoalSprite(Image image, int x, int y, int i, int j, bool con):base(image, x , y , i, j, con)
        {
            Image = image;
            Width = image.Width;
            Height = image.Height;
            I = i;
            J = j;
        }
        public override void Connect()
        {
            Image = Image.FromFile("gold.jpg");
            Con = true;
        }
    }
}
