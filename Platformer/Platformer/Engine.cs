using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platformer
{


    public partial class Engine : Form
    {
        static Sprite canvas = new Sprite(0, 0);
        public static Sprite Canvas
        {
            get { return canvas; }
        }

        private static int frameCount = 0;
        public static int FrameCount
        {
            get { return frameCount; }
        }

        public static Timer t;

        public Engine()
        {
            InitializeComponent();
            DoubleBuffered = true;


            t = new Timer();
            t.Interval = 33;
            t.Tick += T_Tick;
            t.Start();

        }

        private void T_Tick(object sender, EventArgs e)
        {
            frameCount += 1;
            canvas.Update();
            Act();
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            canvas.Paint(e.Graphics);

        }

        public virtual void Resized(EventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Resized(e);
            this.Refresh();
        }


        public virtual void Mouse_Down(MouseEventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse_Down(e);
            canvas.ProcessMouseDown(e);

        }

        public virtual void Mouse_Move(MouseEventArgs e)
        {

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Mouse_Move(e);
        }


        public virtual void Mouse_Wheel(MouseEventArgs e)
        {

        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            Mouse_Wheel(e);
            Refresh();
        }

        public virtual void Mouse_Up(MouseEventArgs e)
        {

        }

        private void Engine_MouseUp(object sender, MouseEventArgs e)
        {
            Mouse_Up(e);
            canvas.ProcessMouseUp(e);
        }

        public virtual void Key_Down(KeyEventArgs e)
        {

        }
        private void Engine_KeyDown(object sender, KeyEventArgs e)
        {
            Key_Down(e);
        }

        public virtual void Act()
        {

        }
    }
}