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


        public SokobanGame():base()
        {
            
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
            lines = System.IO.File.ReadAllLines("Level" + levelNum + ".txt");
            FindForm().Size = new Size(lines[0].Length * unitSize + 100,lines.Length * unitSize + 100);
            for (int j=0; j<lines.Length;j++)
            {
                for (int i = 0; i < lines[j].Length; i++) 
                {
                    char letter = lines[j][i];
                    if(letter=='X')
                    {
                        ImageSprite a = new ImageSprite(Image.FromFile("wall.png"), i*unitSize, j*unitSize, i, j, true);
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
                        ImageSprite a = new PhysicsSprite(Image.FromFile("crate.jpg"), i * unitSize, j * unitSize, i, j, true);
                        a.Width = unitSize;
                        a.Height = unitSize;
                        Canvas.Children.Add(a);
                    }
                }
            }
            Canvas.Children.Add(ninja);
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
                paused = false;
                Console.WriteLine("Win");
            }
        }
        public void Pause()
        {
            mainMenu = new ImageSprite(Image.FromFile("green.png"), Width / 4, Height / 8, -255, -255, true);
            if(paused) mainMenu.Visible = false;
            mainMenu.Width = Width /2;
            mainMenu.Height = Height / 4;
            Canvas.Children.Add(mainMenu);


            next = new ImageSprite(Image.FromFile("FullBlock.png"), Width / 4, Height /8 *5, -269, -255, true);
            if (paused) next.Visible = false;
            next.Width = Width / 2;
            next.Height = Height / 4;
            Canvas.Children.Add(next); 
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

        public override void Mouse_Down(MouseEventArgs e)
        {
            if (!paused && mainMenu.Contains(e.X, e.Y))
            {
                this.Close();
            }
            if (!paused && next.Contains(e.X, e.Y))
            {
                levelNum++;
                Canvas = new Sprite(0, 0, -255, -255, true);
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
                lines = System.IO.File.ReadAllLines("Level" + levelNum + ".txt");
                FindForm().Size = new Size(lines[0].Length * unitSize + 100, lines.Length * unitSize + 100);
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
                            ImageSprite a = new PhysicsSprite(Image.FromFile("crate.jpg"), i * unitSize, j * unitSize, i, j, true);
                            a.Width = unitSize;
                            a.Height = unitSize;
                            Canvas.Children.Add(a);
                        }
                    }
                }
                Canvas.Children.Add(ninja);
                paused = !paused;
            }
        }


    }
}
