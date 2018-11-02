using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Example
{
    public class Sprite
    {
        private double width=0;
        public double Width //properties
        {
            get { return width; }
            set { width = value; }
        }

        private double height=0;
        public double Height //properties
        {
            get { return height; }
            set { height = value; }
        }

        private bool visible = true;
        public bool Visible
        {
            get { return visible;}
            set { visible = value; }
        }

        private Matrix m = new Matrix();
        public Matrix M
        {
            get { return m; }
            set { m=value; }
        }

        private List<Sprite> children = new List<Sprite>();
        public List<Sprite> Children
        {
            get { return children; }
        }

        private double scale = 1;
        public double Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        private double angle = 0;
        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        //instance variable
        private double x;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        private double y;

        public double Y //properties
        {
            get { return y; }
            set { y = value; }
        }
        private int i;
        public int I //properties
        {
            get { return i; }
            set { i = value; }
        }
        private int j;
        public int J //properties
        {
            get { return j; }
            set { j = value; }
        }
        private bool con;
        public bool Con //properties
        {
            get { return con; }
            set { con = value; }
        }

        //width, height


        //constructors
        public Sprite(int x, int y, int i, int j, bool con)
        {
            this.x = x;
            this.y = y;
            I = i;
            J = j;
            Con = con;
        }

        public virtual void Draw(Graphics g)
        {

        }


        //methods/functions
        public void Paint(Graphics g)
        {
            if (!Visible) return;
            M=g.Transform.Clone();
            
            
            g.TranslateTransform((float)X, (float)Y);
            g.ScaleTransform((float)scale, (float)scale);
            g.RotateTransform((float)angle);
            Draw(g);
            //do nothing for painting myself
            foreach (Sprite child in children)
            {
                child.Paint(g);
            }
            g.Transform = M.Clone();
        }
        public virtual void Collide(int dir)
        {
        }

        public virtual void Connect()
        {

        }
        public virtual void Act()
        {

        }

        public virtual void Update()
        {
            if (!Visible) return;
            Act();
            foreach (Sprite child in children)
            {
                child.Update();
            }
        }

        public bool Contains(int screenX, int screenY)
        {
            PointF point = ScreenToCanvasTransform(screenX, screenY);
            float x = point.X;
            float y = point.Y;
            return x >= X && x <= X + Width && y >= Y && y <= Y + Height;
        }

        public PointF ScreenToCanvasTransform(float x, float y)
        {
           
            PointF[] points = new PointF[] { new PointF(x, y) };
            Matrix q = M.Clone();
            q.Invert();
            q.TransformPoints(points);
            return points[0];
        }

        public virtual void MouseDown(MouseEventArgs e)
        {

        }

        public void ProcessMouseDown(MouseEventArgs e)
        {

            if (Contains(e.X, e.Y))
            {
                MouseDown(e);
            }
            foreach (Sprite child in children) child.ProcessMouseDown(e);
        }

        public virtual void MouseUp(MouseEventArgs e)
        {

        }

        public void ProcessMouseUp(MouseEventArgs e)
        {
            if (Contains(e.X, e.Y))
            {
                MouseUp(e);
            }
            foreach (Sprite child in children) child.ProcessMouseUp(e);
 
        }



    }
}
