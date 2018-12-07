using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platformer
{
    class PlatformerGame : Engine
    {
        public static Player ninja;
        public static int unitSize = 24;
        Random enemy = new Random();
        public static List<PhysicsSprite> enemies = new List<PhysicsSprite>();
        int count = 0;
        int speed = 10;
        int lastSpeed;
        ImageSprite mainMenu;
        ImageSprite next;
        bool paused = true;
        bool firstPause = true;
        TextSprite menuText;
        public static TextSprite lose;
        

        public PlatformerGame() : base()
        {
            GenerateInitial();
        }

        public void GenerateInitial()
        {
            firstPause = true;
            Canvas.Width = ClientRectangle.Width;
            Canvas.Height = ClientRectangle.Height;
            ImageSprite background = new ImageSprite(Image.FromFile("background.png"), 0, 0);
            background.Width = ClientRectangle.Width;
            background.Height = ClientRectangle.Height;
            Canvas.Children.Add(background);
            background = new ImageSprite(Image.FromFile("background.png"), ClientRectangle.Width, 0);
            background.Width = ClientRectangle.Width;
            background.Height = ClientRectangle.Height;
            Canvas.Children.Add(background);
            PhysicsSprite block;
            for (int i = 0; i <= 2 * ClientRectangle.Width / unitSize; i++)
            {
                block = new PhysicsSprite(Image.FromFile("castlewall.jpg"), i * unitSize, ClientRectangle.Height - unitSize);
                block.Height = unitSize;
                block.Width = unitSize;
                Canvas.Children.Add(block);
            }
            ninja = new Player(Image.FromFile("ninja.png"), 0, ClientRectangle.Height - 2 * unitSize, 88, 88);
            Animation move = new Animation();
            move.IList = new int[] { 0, 1, 2, 3, 4 };
            move.JList = new int[] { 0, 0, 0, 0, 0 };
            move.TimeList = new int[] { 1, 1, 1, 1, 1 };
            ninja.Animations.Add("move", move);
            Animation stand = new Animation();
            stand.IList = new int[] { 0, 0, 0, 0, 0 };
            stand.JList = new int[] { 0, 0, 0, 0, 0 };
            stand.TimeList = new int[] { 1, 1, 1, 1, 1 };
            ninja.Animations.Add("stand", stand);
            ninja.Width = unitSize;
            ninja.Height = unitSize;
            Canvas.Children.Add(ninja);
            lose = new TextSprite("You've been caught!", ClientRectangle.Width / 2, ClientRectangle.Height / 2);
            Canvas.Children.Add(lose);
            lose.Visible = false;
        }

        

        public override void Resized(EventArgs e)
        {
            if (ClientRectangle.Width > ClientRectangle.Height)
            {
                Canvas.Scale = ClientRectangle.Height / Canvas.Height;
            }
            else if (ClientRectangle.Height > ClientRectangle.Width)
            {
                Canvas.Scale = ClientRectangle.Width / Canvas.Width;
            }
            else
            {
                Canvas.Scale = ClientRectangle.Height / Canvas.Height;
            }
        }
        public override void Key_Down(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W && ninja.AY == 0)
            {
                ninja.Y -= 3 * unitSize;
                ninja.AY = .01 * (-speed + 16);
                //Console.WriteLine("yyee");
            }
            if (e.KeyCode == Keys.Space && ninja.Shoot)
            {
                ninja.Throw();
                Console.WriteLine("yyee");
            }
            if (e.KeyCode == Keys.Escape)
            {
                Pause();
            }
        }
        public void Pause()
        {
            if (firstPause)
            {
                firstPause = false;
                mainMenu = new ImageSprite(Image.FromFile("castlewall.jpg"), ClientRectangle.Width / 4, ClientRectangle.Height / 8);

                mainMenu.Width = Width / 2;
                mainMenu.Height = Height / 4;
                Canvas.Children.Add(mainMenu);

                menuText = new TextSprite("Close", ClientRectangle.Width / 4, ClientRectangle.Height / 8);
                mainMenu.Children.Add(menuText);
            }
            if (paused) next.Visible = false;
            if (paused) mainMenu.Visible = false;
            if (paused)
            {
                lastSpeed = speed;
                speed = 999999;
            }
            else
            {
                speed = lastSpeed;
            }
            paused = !paused;

            foreach (Sprite child in Canvas.Children)
            {
                if (child != lose)
                {
                    child.Visible = !child.Visible;
                }
            }
        }
        public override void Act()
        {
            if (Engine.FrameCount % speed == 0)
            {
                foreach (PhysicsSprite knight in enemies)
                {
                    knight.TargetX -= unitSize;
                }
            }
            if (Engine.FrameCount % (10 * speed) == 0 && !ninja.Shoot)
            {
                ninja.Shoot = true;
            }
            Canvas.X = -ninja.X * Canvas.Scale;
            Canvas.Y = -ninja.Y * Canvas.Scale + 4 * ClientRectangle.Height / 5;
            if (enemy.NextDouble() > .993)
            {
                PhysicsSprite knight = new PhysicsSprite(Image.FromFile("knight.jpg"), (int)ninja.X + ClientRectangle.Width * (count + 1), ClientRectangle.Height - 2 * unitSize);
                knight.Width = unitSize;
                knight.Height = unitSize;
                enemies.Add(knight);
                Canvas.Children.Add(knight);
                if (enemies.Count % 20 == 0 && speed > 1)
                {
                    speed--;
                }
            }
        }
    }
}
