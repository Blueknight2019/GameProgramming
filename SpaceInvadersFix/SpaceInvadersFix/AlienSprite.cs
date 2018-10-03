using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersFix
{
    class AlienSprite : Sprite
    {
        static int counter = 0;
        private float velocityX;
        public String[] names = { "C1", "C2", "B1", "B2", "A1", "A2" };

        public float VelocityX
        {
            get { return velocityX; }
            set { velocityX = value; }
        }

        private float velocityY;

        public float VelocityY
        {
            get { return velocityY; }
            set { velocityY = value; }
        }

        public void Act(float width)
        {
            counter++;
            X += velocityX;
            Y += velocityY;
            if (X + Width > width) velocityX = -velocityX;
            Image = Image.FromFile("images\\Invader" + names[(int)(((Y - (540/108))/(32 + (540/180)))/2) + ((counter/330)%2)] + ".png");
        }
        public override void Paint(Graphics g)
        {
            Act(g.ClipBounds.Width);
            if (Visible)
            {
                g.DrawImage(Image, X, Y);
            }
        }

        public AlienSprite(float x, float y, Image image, int width, int height, float velocityX, float velocityY) : base(x, y, image, width, height)
        {
            this.velocityX = velocityX;
            this.velocityY = velocityY;
        }
    }
}