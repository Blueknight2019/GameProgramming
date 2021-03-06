﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Example
{
    public class ImageSprite:Sprite
    {
       
        

        private Image image;
        public Image Image //properties
        {
            get { return image; }
            set { image = value; }
        }

        //constructors
        public ImageSprite(Image image, int x, int y, int i, int j, bool con):base(x,y, i, j, con)
        {
            this.image = image;
            this.Width = image.Width;
            this.Height = image.Height;
            I = i;
            J = j;
        }

        //methods/functions
        public override void Draw(Graphics g)
        {
            g.DrawImage(image, 0, 0, (float)Width, (float)Height);
        }

        public override void Connect()
        {
        }
    }
}
