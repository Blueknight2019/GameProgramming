using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class PhysicsSprite : ImageSprite
    {
        private bool crate = true;
        public override bool Crate
        {
            get { return crate; }
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

        public override void Collide(int dir)
        {
            if (dir == 1)
            {
                targetX -= SokobanGame.unitSize;
                I--;
            }
            else if (dir == 2)
            {
                targetX += SokobanGame.unitSize;
                I++;
            }
            else if (dir == 3)
            {
                targetY -= SokobanGame.unitSize;
                J--;
            }
            else if (dir == 4)
            {
                targetY += SokobanGame.unitSize;
                J++;
            }
        }

        public override void Connect()
        {
            if (SokobanGame.lines[J][I].Equals('G'))
            {
                Image = Image.FromFile("gold.jpg");
                Con = true;
            }
        }

        public PhysicsSprite(Image image, int x, int y, int i, int j, bool con) : base(image, x, y, i, j, con)
        {
            targetX = X;
            targetY = Y;
        }

        double vx = 0;
        double vy = 0;
        double k = .75;
        double velocityReduction = .33;

        //F=m*a
        //k*dist=a
        //k*dist=dv/dt
        //k*dist=dv

        public override void Update()
        {
            foreach (Sprite child in SokobanGame.Canvas.Children)
            {
                if(I == child.I && J == child.J)
                {
                    child.Connect();
                }
            }
            if (Math.Sqrt((X - targetX) * (X - targetX) + (Y - targetY) * (Y - targetY) )< .1)
            {
                X = TargetX;
                Y = TargetY;
            }
            else
            {
                vx += k * (TargetX - X);
                vy += k * (TargetY - Y);
                vx *= velocityReduction;
                vy *= velocityReduction;
                X += vx;
                Y += vy;
            }


            base.Update();
        }

    }
}
