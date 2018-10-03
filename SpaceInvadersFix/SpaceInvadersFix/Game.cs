using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvadersFix
{
    class Game
    {
        static Sprite quitBar;
        static int counter = 0;
        static Sprite canvas = new Sprite(0, 0, Image.FromFile("images\\InvaderC1.png"), 1, 1);
        static Sprite player;
        static bool paused = false;
        
        private int width = 960;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height = 540;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private int numAliens = 55;

        public int NumAliens
        {
            get { return numAliens; }
            set { numAliens = value; }
        }

        private int numBlocks = 3;

        public int NumBlocks
        {
            get { return numBlocks; }
            set { numBlocks = value; }
        }

        private void MakeAliens()
        {
            int numAliens = 0;
            int row = 1;
            float alienX = width / 192;
            float alienY = height / 108;
            AlienSprite alien;
            while (numAliens < 11 && row <= 5)
            {

                if (row == 1)
                {
                    alien = new AlienSprite(alienX, alienY, Image.FromFile("images\\InvaderC1.png"), 48, 32, 3, 0);
                }
                else if (row == 2 || row == 3)
                {
                    alien = new AlienSprite(alienX, alienY, Image.FromFile("images\\InvaderB1.png"), 48, 32, 3, 0);
                }
                else if (row == 4 || row == 5)
                {
                    alien = new AlienSprite(alienX, alienY, Image.FromFile("images\\InvaderA1.png"), 48, 32, 3, 0);
                }
                else alien = new AlienSprite(alienX, alienY, Image.FromFile("images\\InvaderC1.png"), 48, 32, 0, 0);

                canvas.Children.Add(alien);

                alienX += width / 192 + 48;
                numAliens++;

                if (numAliens >= 11)
                {
                    alienY += 32 + height / 108;
                    alienX = width / 192;
                    row++;
                    numAliens = 0;
                }
            }
        }

        private void Player()
        {
            player = new Sprite(width / 2 - 30, height - 100, Image.FromFile("images\\Ship.png"), 60, 32);
            canvas.Children.Add(player);
        }

        public void PlayerMove(float x)
        {
            player.X = x - 30;
            if (player.X + player.Width > width) player.X = (width - player.Width);
            else if (player.X < 0) player.X = 0;
        }

        public void PlayerShoot()
        {
            ActiveSprite bullet = new ActiveSprite(player.X + 27, player.Y - 17, Image.FromFile("images\\bullet.png"), 6, 17, 0, -15);
            canvas.Children.Add(bullet);
        }
        public void Pause()
        {
            paused = !paused;
        }
        public void Update(PaintEventArgs e)
        {
            if (paused)
            {
                Sprite quitBox = new Sprite(width / 2 - 2 * width / 5, height / 2 - 3 * height / 10, Image.FromFile("images\\OKBlock.png"), 4 * width / 5, 3 * height / 5);
                quitBox.Paint(e.Graphics);
                quitBar = new Sprite(width / 2 - width / 5, height / 2 + 1 * height / 20, Image.FromFile("images\\weakblock.png"), 2 * width / 5, 1 * height / 5);
                quitBar.Paint(e.Graphics);
                String quitText = "Quit?";
                SizeF yeetSize = e.Graphics.MeasureString(quitText, new Font("Times New Roman", 36));
                e.Graphics.DrawString(quitText, new Font("Times New Roman", 36), Brushes.Orange, width / 2 - (yeetSize.Width / 2), height / 2 + (yeetSize.Height / 2));
                String pauseText = "Paused.";
                SizeF textSize = e.Graphics.MeasureString(pauseText, new Font("Times New Roman", 72));
                e.Graphics.DrawString(pauseText, new Font("Times New Roman", 72), Brushes.Orange, width / 2 - (textSize.Width / 2), (height / 2) - (textSize.Height));
            }
            else
            {
                counter++;
                foreach (Sprite child in canvas.Children)
                {
                    child.Paint(e.Graphics);
                    // if (!child.Visible) canvas.Children.Remove(child);
                }
            }
        }

        public void Quit(float x, float y)
        {
            if (paused)
            {
                if (quitBar.Contains(x, y)) Engine.ActiveForm.Close();
            }
        }

        public Game(int width, int height, int numAliens, int numBlocks)
        {
            this.width = width;
            this.height = height;
            this.numAliens = numAliens;
            this.numBlocks = numBlocks;
            MakeAliens();
            Player();
        }
    }
}
