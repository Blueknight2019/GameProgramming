using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Platformer
{
    public class AnimatedSprite:Sprite
    {

        private Dictionary<String, Animation> animationDict = new Dictionary<String, Animation>();
        public Dictionary<String, Animation> Animations
        {
            get { return animationDict; }
        }

        private String state="";
        public String State
        {
            get { return state; }
            set { state=value; }
        }



        private Image spriteSheet;
        public Image SpriteSheet //properties
        {
            get { return spriteSheet; }
            set { spriteSheet = value; }
        }
        private int srcWidth;
        public int SrcWidth //properties
        {
            get { return srcWidth; }
            set { srcWidth = value; }
        }
        private int srcHeight;
        public int SrcHeight //properties
        {
            get { return srcHeight; }
            set { srcHeight = value; }
        }

        //constructors
        public AnimatedSprite(Image spriteSheet, int x, int y, int width, int height):base(x,y)
        {
            this.spriteSheet = spriteSheet;
            srcWidth = width;
            srcHeight = height;
        }


        //methods/functions
        public override void Draw(Graphics g)
        {
            int i = 0;
            int j = 0;
            if(Animations.ContainsKey(state))
            {
                Animation a = Animations[state];
                int totalTime = a.TimeList.Sum();
                int partialTime =(int)(Engine.FrameCount % totalTime);
                int t = 0;
                int k = 0;
                for (; k < a.TimeList.Length && t<= partialTime; k++)
                    t += a.TimeList[k];
                k -= 1;
                i = a.IList[k];
                j = a.JList[k];
                
            }

            Rectangle destRect = new Rectangle((int)0, (int)0, (int)Width, (int)Height);

            Rectangle srcRect = new Rectangle((int)(i*srcWidth), (int)(j*srcHeight), (int)srcWidth, (int)srcHeight);
            g.DrawImage(spriteSheet, destRect, srcRect, GraphicsUnit.Pixel);
        }
    }
}
