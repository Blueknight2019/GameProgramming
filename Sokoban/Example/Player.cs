using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    class Player:AnimatedSprite
    {
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

        public Player(Image spriteSheet, int x, int y, int width, int height, int i, int j, bool con) : base(spriteSheet, x, y, width, height, i, j, con)
        {
            targetX = X;
            targetY = Y;
            I = i;
            J = j;
        }

        public void CheckCollision(int dir)
        {
            if (dir == 1)
            {
                I--;
                if (!SokobanGame.lines[J][I].Equals('X'))
                {
                    targetX -= SokobanGame.unitSize;
                    if (!SokobanGame.lines[J][I-1].Equals('X'))
                    {
                        foreach (Sprite child in SokobanGame.Canvas.Children)
                        {
                            if (I == child.I && J == child.J)
                            {
                                child.Collide(dir);
                            }
                        }
                    }
                }
                else I++;
            }
            else if (dir == 2)
            {
                I++;
                if (!SokobanGame.lines[J][I].Equals('X'))
                {
                    
                    targetX += SokobanGame.unitSize;
                    if (!SokobanGame.lines[J][I+1].Equals('X'))
                    {
                        foreach (Sprite child in SokobanGame.Canvas.Children)
                        {
                            if (I == child.I && J == child.J)
                            {
                                child.Collide(dir);
                            }
                        }
                    }
                }
                else I--;
            }
            else if (dir == 3)
            {
                J--;
                if (!SokobanGame.lines[J][I].Equals('X'))
                {
                    
                    targetY -= SokobanGame.unitSize;
                    if (!SokobanGame.lines[J-1][I].Equals('X'))
                    {
                        foreach (Sprite child in SokobanGame.Canvas.Children)
                        {
                            if (I == child.I && J == child.J)
                            {
                                child.Collide(dir);
                            }
                        }
                    }
                }
                else J++;
            }
            else if (dir == 4)
            {
                J++;
                if (!SokobanGame.lines[J][I].Equals('X'))
                {
                    targetY += SokobanGame.unitSize;
                    if (!SokobanGame.lines[J+1][I].Equals('X'))
                    {
                        foreach (Sprite child in SokobanGame.Canvas.Children)
                        {
                            if (I == child.I && J == child.J)
                            {
                                child.Collide(dir);
                            }
                        }
                    }
                }
                else J--;
            }
        }

        double vx = 0;
        double vy = 0;
        double k = 0.75;
        double velocityReduction = .33;

        //F=m*a
        //k*dist=a
        //k*dist=dv/dt
        //k*dist=dv

        public override void Update()
        {
            if (Math.Sqrt((X - targetX) * (X - targetX) + (Y - targetY) * (Y - targetY)) < .1)
            {
                X = TargetX;
                Y = TargetY;
                State = "stand";
            }
            else
            {
                State = "move";
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
