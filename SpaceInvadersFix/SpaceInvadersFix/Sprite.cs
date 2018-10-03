using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersFix
{
    class Sprite
    {
        static bool visible = true;
        public virtual bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }
        private float x;

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        private float y;

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        private Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }


        public virtual void Paint(Graphics g)
        {
            if (visible)
            {
                g.DrawImage(Image, X, Y);
            }
        }

        public virtual Bitmap Scale(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public bool Contains(float x, float y)
        {
            if (x > X && x < X + Width && y > Y && y < Y + Height)
            {
                return true;
            }
            else return false;
        }

        private List<Sprite> children = new List<Sprite>();
        
        public List<Sprite> Children
        {
            get { return children; }
            set { children = value; }
        }

        public Sprite(float x, float y, Image image, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.image = Scale(image, width, height);
        }
    }
}
