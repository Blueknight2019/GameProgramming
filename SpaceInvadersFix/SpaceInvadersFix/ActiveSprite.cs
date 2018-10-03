using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersFix
{
    class ActiveSprite : Sprite
    {
        static bool visible = true;
        public override bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        private float velocityX;

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

        public void Act()
        {
            X += velocityX;
            Y += velocityY;
            /*if (Y < 0 || Y > 540)
             {
                 visible = false;
             }*/
        }

        public bool CheckCollision(Sprite obj, Sprite target)
        {
            return true;
        }

        public override void Paint(Graphics g)
        {
            Act();
            if (Visible)
            {
                g.DrawImage(Image, X, Y);
            }
        }

        public ActiveSprite(float x, float y, Image image, int width, int height, float velocityX, float velocityY) : base(x, y, image, width, height)
        {
            this.velocityX = velocityX;
            this.velocityY = velocityY;
        }
    }
}
