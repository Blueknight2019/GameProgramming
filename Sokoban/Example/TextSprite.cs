using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class TextSprite:Sprite
    {
        private String text;
        public String Text
        {
            get { return text; }
            set { text = value; }
        }
        public TextSprite(String text, int x, int y, int i, int j, bool con):base(x, y, i, j, con)
        {
            Text = text;
        }

        public override void Draw(Graphics g)
        {
            SizeF size = g.MeasureString(text, new Font("Times New Roman", 32));
            g.DrawString(text, new Font("Times New Roman", 32), new SolidBrush(Color.Cyan), -size.Width /2, -size.Height/2);
        }
    }
}
