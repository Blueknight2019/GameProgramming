using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example
{
    public class SokobanGame:Engine
    {

        Player ninja;
        public static int unitSize = 88;
        public static String[] lines;
        public static bool paused = true;
        ImageSprite mainMenu;
        ImageSprite next;
        int levelNum = 1;
        bool firstPause = true;
        TextSprite winText;
        TextSprite menuText;
        TextSprite nextText;


        public SokobanGame():base()
        {
            LoadLevel(1);
        }

        public void CheckWin()
        {
            bool win = true;
            foreach (Sprite child in Canvas.Children)
            {
                if (!child.Con)
                {
                    win = false;
                }
            }
            if (win)
            {
                if (levelNum == 1)
                {
                    levelNum = 2;
                    LoadLevel(2);
                }
                else
                {
                    winText = new TextSprite("All Levels Complete!", Width / 2, Height / 2, 345, 345, true);
                    Canvas.Children.Add(winText);
                }
            }
        }
        public void Pause()
        {
            if (firstPause)
            {
                firstPause = false;
                mainMenu = new ImageSprite(Image.FromFile("green.png"), Width / 4, Height / 8, -255, -255, true);
                
                mainMenu.Width = Width / 2;
                mainMenu.Height = Height / 4;
                Canvas.Children.Add(mainMenu);

                menuText = new TextSprite("Reset", Width / 4, Height / 8, -255, -255, true);
                mainMenu.Children.Add(menuText);


                next = new ImageSprite(Image.FromFile("FullBlock.png"), Width / 4, Height / 8 * 5, -269, -255, true);
                
                next.Width = Width / 2;
                next.Height = Height / 4;
                Canvas.Children.Add(next);

                nextText = new TextSprite("Next Level", Width / 4, Height / 8, -255, -255, true);
                next.Children.Add(nextText);
            }
            if (paused) next.Visible = false;
            if (paused) mainMenu.Visible = false;
            paused = !paused;
            
            foreach (Sprite child in Canvas.Children)
            {
                child.Visible = !child.Visible;
            }
        }
        public override void Key_Down(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Pause();
            }
            if (e.KeyCode == Keys.A)
            {
                ninja.CheckCollision(1);
            }
            if (e.KeyCode == Keys.D)
            {
                ninja.CheckCollision(2);
            }
            if (e.KeyCode == Keys.W)
            {
                ninja.CheckCollision(3);
            }
            if (e.KeyCode == Keys.S)
            {
                ninja.CheckCollision(4);
            }
            CheckWin();
        }

        public void LoadLevel(int lvl)
        {
            paused = true;
            lines = System.IO.File.ReadAllLines("Level" + lvl + ".txt");
            Canvas = new Sprite(0, 0, -255, -255, true);
            Canvas.Width = lines[0].Length * unitSize;
            Canvas.Height = lines.Length * unitSize;
            firstPause = true;
            ninja = new Player(Image.FromFile("ninja.png"), unitSize, unitSize, unitSize, unitSize, 1, 1, true);
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
            for (int j = 0; j < lines.Length; j++)
            {
                for (int i = 0; i < lines[j].Length; i++)
                {
                    char letter = lines[j][i];
                    if (letter == 'X')
                    {
                        ImageSprite a = new ImageSprite(Image.FromFile("wall.png"), i * unitSize, j * unitSize, i, j, true);
                        a.Width = unitSize;
                        a.Height = unitSize;
                        Canvas.Children.Add(a);
                    }
                    else if (letter == 'G')
                    {
                        ImageSprite a = new GoalSprite(Image.FromFile("green.png"), i * unitSize, j * unitSize, i, j, false);
                        a.Width = unitSize;
                        a.Height = unitSize;
                        Canvas.Children.Add(a);
                    }
                    else if (letter == 'M')
                    {
                        ImageSprite a = new PhysicsSprite(Image.FromFile("crate.jpg"), i * unitSize, j * unitSize, i, j, false);
                        a.Width = unitSize;
                        a.Height = unitSize;
                        Canvas.Children.Add(a);
                    }
                }
            }
            Canvas.Children.Add(ninja);
            Width = lines[0].Length * unitSize;
            Height = lines.Length * unitSize;
            Canvas.XScale = (float)Width / (float)Canvas.Width;
            Canvas.YScale = (float)Height / (float)Canvas.Height;
        }

        public override void Resized(EventArgs e)
        {
            Canvas.XScale = (float)Width / (float)Canvas.Width;
            Canvas.YScale = (float)Height / (float)Canvas.Height;
        }

        public override void Mouse_Down(MouseEventArgs e)
        {
            if (!paused && mainMenu.Contains(e.X, e.Y))
            {
                LoadLevel(levelNum);
            }
            if (!paused && next.Contains(e.X, e.Y))
            {
                levelNum++;
                try
                {
                    LoadLevel(levelNum);
                }
                catch (Exception yeet)
                {
                    this.Close();
                }
            }
        }


    }
}
