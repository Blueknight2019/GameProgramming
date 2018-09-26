using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invaders
{
    public partial class Engine : Form
    {
        //variables for later.
        static int[] alienSize = { 48, 32 };
        static int[] blockSize = { 30, 20 };
        static int[] playerSize = { 60, 32 };
        static int[] bulletSize = { 6, 17 };
        static String one = "1";
        static String two = "2";
        static long counter = 0;
        List<ActiveSprite> aliens = new List<ActiveSprite>();
        ActiveSprite player = null;
        List<ActiveSprite> bullets = new List<ActiveSprite>();
        List<ActiveSprite> alienBullets = new List<ActiveSprite>();
        List<Sprite> blocks = new List<Sprite>();
        Random rand = new Random();
        Timer t = new Timer();
        bool paused = false;
        bool lost = false;
        bool won = false;
        Sprite quitBar;

        //start your engines...
        public Engine()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Aliens();
            Player();
            Blocks();

            
            t.Interval = 33;
            t.Tick += T_Tick;
            t.Start();
        }

        public void Engine_Pause(object sender, KeyEventArgs e)
        {
            paused = !paused;
            if (!paused)
            {
                t.Start();
            }
        }
        //create some cover for us
        private void Blocks()
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            for (int i = 0; i < 3; i++)
            {
                Sprite block = new Sprite(width / 10 + (width / 4) * i, height - height / 5, Image.FromFile("images\\fullblock.png"), 60, 20);
                blocks.Add(block);
            }
        }

        public void Engine_PauseClicks(object sender, MouseEventArgs e)
        {
            if (paused && quitBar.Contains(e.X, e.Y))
            {
                Console.WriteLine("Yeet");
                Close();
            }
        }

        private void T_Tick(object sender, EventArgs e)
        {
            counter++;
            this.Refresh();
            if (aliens.Count == 0) won = true;
            for (int i = 0; i < bullets.Count; i++)
            {
                //remove off screen bullets
                if (bullets.ElementAt(i).Y + bullets.ElementAt(i).Height < 0)
                {
                    bullets.Remove(bullets.ElementAt(i));
                    continue;
                }
                //remove shot aliens
                for (int j = 0; j < aliens.Count; j++)
                {
                    if (aliens.ElementAt(j).Contains(bullets.ElementAt(i).X, bullets.ElementAt(i).Y))
                    {
                        aliens.Remove(aliens.ElementAt(j));
                        bullets.ElementAt(i).Y = -bullets.ElementAt(i).Height;
                        continue;
                    }
                }
            }
            for (int i = 0; i < aliens.Count; i++)
            {
                //let the aliens shoot back
                if (rand.NextDouble() > 0.999)
                {
                    ActiveSprite alienBullet = new ActiveSprite(aliens.ElementAt(i).X + 21, aliens.ElementAt(i).Y - aliens.ElementAt(i).Height - 17, Image.FromFile("images\\bullet.png"), 6, 17, 0, 15);
                    alienBullets.Add(alienBullet);
                }
            }
            //clear the alien shots
            for (int i = 0; i < alienBullets.Count; i++)
            {
                if (alienBullets.ElementAt(i).Y > ClientSize.Height)
                {
                    alienBullets.Remove(alienBullets.ElementAt(i));
                    continue;
                }
                if (player.Contains(alienBullets.ElementAt(i).X, alienBullets.ElementAt(i).Y))
                {
                    lost = true;
                }
            }
            //check the blocks for shots
            for (int i = 0; i < blocks.Count; i++)
            {
                for (int j = 0; j < bullets.Count(); j++)
                {
                    if (blocks.ElementAt(i).Contains(bullets.ElementAt(j).X, bullets.ElementAt(j).X))
                    {
                        bullets.ElementAt(i).Y = -bullets.ElementAt(i).Height;
                        continue;
                    }
                }
                for (int j = 0; j < alienBullets.Count; j++)
                {
                    if (blocks.ElementAt(i).Contains(alienBullets.ElementAt(j).X , alienBullets.ElementAt(j).Y))
                    {
                        alienBullets.Remove(alienBullets.ElementAt(j));
                        continue;
                    }
                }
            }
            //change alien skins
            if (counter % 33 == 0)
            {
                for (int i = 0; i < aliens.Count; i++)
                {
                    if (counter % 2 == 1 && i < 55)
                    {
                        aliens.ElementAt(i).Image = Image.FromFile("images\\InvaderA" + two + ".png");
                    }
                    if (counter % 2 == 0 && i < 55)
                    {
                        aliens.ElementAt(i).Image = Image.FromFile("images\\InvaderA" + one + ".png");
                    }
                    if (counter % 2 == 1 && i < 33)
                    {
                        aliens.ElementAt(i).Image = Image.FromFile("images\\InvaderB" + two + ".png");
                    }
                    if (counter % 2 == 0 && i < 33)
                    {
                        aliens.ElementAt(i).Image = Image.FromFile("images\\InvaderB" + one + ".png");
                    }
                    if (counter % 2 == 1 && i < 11)
                    {
                        aliens.ElementAt(i).Image = Image.FromFile("images\\InvaderC" + two + ".png");
                    }
                    if (counter % 2 == 0 && i < 11)
                    {
                        aliens.ElementAt(i).Image = Image.FromFile("images\\InvaderC" + one + ".png");
                    }
                }
            }
        }

        //spawn the aliens
        private void Aliens()
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            int numAliens = 0;
            int row = 1;
            float alienX = width / 192;
            float alienY = height / 108;
            ActiveSprite alien;
            while (numAliens < 11 && row <= 5)
            {

                if (row == 1)
                {
                    alien = new ActiveSprite(alienX, alienY, Image.FromFile("images\\InvaderC" + one + ".png"), 48, 32, 3, 0);
                }
                else if (row == 2 || row == 3)
                {
                    alien = new ActiveSprite(alienX, alienY, Image.FromFile("images\\InvaderB" + one + ".png"), 48, 32, 3, 0);
                }
                else if (row == 4 || row == 5)
                {
                    alien = new ActiveSprite(alienX, alienY, Image.FromFile("images\\InvaderA" + one + ".png"), 48, 32, 3, 0);
                } else alien = new ActiveSprite(alienX, alienY, Image.FromFile("images\\InvaderC1.png"), 48, 32, 0, 0);

                aliens.Add(alien);

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

        //make the player
        private void Player()
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            player = new ActiveSprite(width / 2 - 30, height - height / 96, Image.FromFile("images\\Ship.png"), 60, 32, 0, 0);
        }
        //move said player
        private void Engine_PlayerMove(object sender, MouseEventArgs e)
        {
            player.X = e.X -30;
            if (player.X > 960)
            {
                player.X = 900;
            }
            if (player.X < 0)
            {
                player.X = 0;
            }
        }
        //let the player fight
        private void Engine_PlayerShoot(object sender, MouseEventArgs e)
        {
            ActiveSprite bullet = new ActiveSprite(player.X + 27, player.Y - 17, Image.FromFile("images\\bullet.png"), 6, 17, 0, -15);
            bullets.Add(bullet);
        }
        //paint all this stuff.
        protected override void OnPaint(PaintEventArgs e)
        {
            if (lost)
            {
                int width = this.ClientSize.Width;
                int height = this.ClientSize.Height;
                t.Stop();
                String lossText = "Sleeping with the asteroids it seems...";
                SizeF textSize = e.Graphics.MeasureString(lossText, new Font("Times New Roman", 72));
                e.Graphics.DrawString(lossText, new Font("Times New Roman", 72), Brushes.Orange, width / 2 - (textSize.Width / 2), height / 2 - (textSize.Height / 2));
            }
            if (won)
            {
                int width = this.ClientSize.Width;
                int height = this.ClientSize.Height;
                t.Stop();
                String winText = "The aliens are gone!";
                SizeF textSize = e.Graphics.MeasureString(winText, new Font("Times New Roman", 72));
                e.Graphics.DrawString(winText, new Font("Times New Roman", 72), Brushes.Orange, width / 2 - (textSize.Width / 2), height / 2 - (textSize.Height / 2));
            }
            if (paused)
            {
                int width = this.ClientSize.Width;
                int height = this.ClientSize.Height;
                Sprite quitBox = new Sprite(width / 2 - 2*width/5, height - height / 5- 3*height/10, Image.FromFile("images\\OKBlock.png"), 4 * width / 5, 3 * height / 5);
                quitBox.Paint(e.Graphics);
                quitBar = new Sprite(width / 2 -  width / 5, height - height / 5 - 1 * height / 10, Image.FromFile("images\\weakblock.png"), 2 * width / 5, 1 * height / 5);
                quitBar.Paint(e.Graphics);
                String quitText = "Quit?";
                SizeF yeetSize = e.Graphics.MeasureString(quitText, new Font("Times New Roman", 36));
                e.Graphics.DrawString(quitText, new Font("Times New Roman", 36), Brushes.Orange, width / 2 - (yeetSize.Width / 2), height - height / 5 - (yeetSize.Height / 2));
                String pauseText = "Paused.";
                SizeF textSize = e.Graphics.MeasureString(pauseText, new Font("Times New Roman", 72));
                e.Graphics.DrawString(pauseText, new Font("Times New Roman", 72), Brushes.Orange, width / 2 - (textSize.Width / 2), height / 2 - (textSize.Height / 2));
                t.Stop();
            }
            player.Paint(e.Graphics);
            //loop through the aliens and set their new positions
            foreach (ActiveSprite alien in aliens)
            {
                if (alien.X + alien.Width > 960 || alien.X < 0)
                {
                        alien.VelocityX = -alien.VelocityX;
                }
                alien.Paint(e.Graphics);
            }
            //set the new bullet positions
            foreach (ActiveSprite bullet in bullets)
            {
                bullet.Paint(e.Graphics);
            }
            foreach (Sprite block in blocks)
            {
                block.Paint(e.Graphics);
            }
            foreach (ActiveSprite alienBullet in alienBullets)
            {
                alienBullet.Paint(e.Graphics);
            }
        }
    }
}
