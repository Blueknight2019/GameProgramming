using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvadersFix
{
    public partial class Engine : Form
    {
        static Game space;
        public Engine()
        {
            InitializeComponent();
            space = new Game(960, 540, 55, 3);

            DoubleBuffered = true;
            Timer t = new Timer();
            t.Interval = (1000 / 30);
            t.Tick += T_Tick;
            t.Start();

        }

        private void T_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            space.Update(e);
        }

        private void Engine_MouseMove(object sender, MouseEventArgs e)
        {
            space.PlayerMove(e.X);
        }

        private void Engine_MouseDown(object sender, MouseEventArgs e)
        {
            space.PlayerShoot();
        }

        private void Engine_KeyDown(object sender, KeyEventArgs e)
        {
            space.Pause();
        }

        private void Engine_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            space.Quit(e.X, e.Y);
        }
    }
}
