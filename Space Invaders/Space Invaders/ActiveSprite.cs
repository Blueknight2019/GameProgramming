using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    class ActiveSprite:Sprite
    {
        private Sprite sprite;

        public Sprite Sprite
        {
            get { return sprite; }
            set { sprite = value; }
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

        public override void Paint(Graphics g)
        {
            X += velocityX;
            Y += velocityY;
            g.DrawImage(Image, X, Y);
        }

        public ActiveSprite(float x, float y, Image image, int width, int height, float velocityX, float velocityY):base(x, y, image, width, height)
        {
            this.velocityX = velocityX;
            this.velocityY = velocityY;
        }
    }
}
