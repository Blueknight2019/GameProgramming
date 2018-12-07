using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    public class Player:AnimatedSprite
    {
        public static List<Player> shuriken = new List<Player>();


        private bool shoot = true;
        public bool Shoot
        {
            get { return shoot; }
            set { shoot = value; }
        }

        private double targetX;
        public Double TargetX //properties
        {
            get { return targetX; }
            set { targetX = value; }
        }
        private double targetY;
        public Double TargetY //properties
        {
            get { return targetY; }
            set { targetY = value; }
        }

        public Player(Image spriteSheet, int x, int y, int width, int height) : base(spriteSheet, x, y, width, height)
        {
            targetX = X;
            targetY = Y;
        }

        double vx = 0;
        public Double VX
        {
            get { return vx; }
            set { vx = value; }
        }
        double vy = 0;
        public Double VY
        {
            get { return vy; }
            set { vy = value; }
        }
        double ay = 0;
        public Double AY
        {
            get { return ay; }
            set { ay = value; }
        }
        double ax = 0;
        public Double AX
        {
            get { return ax; }
            set { ax = value; }
        }

        public void Throw()
        {
            /*
            Player star = new Player(Image.FromFile("ninja.png"), 5, (int)this.Width / 2, 88, 88);
            star.TargetX += 99;
            Animation shot = new Animation();
            shot.IList = new int[] { 0, 0, 0, 0, 0 };
            shot.JList = new int[] { 4, 4, 4, 4, 4 };
            shot.TimeList = new int[] { 1, 1, 1, 1, 1 };
            star.Animations.Add("shot", shot);
            star.State = "shot";
            shuriken.Add(star);
            this.Children.Add(star);
            shoot = false;
            */
        }

        public override void Update()
        {
            if (Math.Sqrt((X - targetX) * (X - targetX) + (Y - targetY) * (Y - targetY)) < .1)
            {
                X = TargetX;
                Y = TargetY;
            }
            else
            {
                vx += ax;
                vy += ay;
                X += vx;
                Y += vy;
                
            }
            State = "move";
            base.Update();
        }
    }
}
