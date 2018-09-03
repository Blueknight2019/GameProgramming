using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_Game
{
    public partial class Form1 : Form
    {
        static int[,] board = new int[3, 3];
        static int turn = 0;
        static bool win = false;
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            e.Graphics.FillRectangle(Brushes.White, 0, 0, width, height);
            e.Graphics.FillRectangle(Brushes.Black, width / 3 - 10, 0, 10, height);
            e.Graphics.FillRectangle(Brushes.Black, 2 * width / 3 - 10, 0, 10, height);
            e.Graphics.FillRectangle(Brushes.Black, 10, height / 3 - 10, width, 10);
            e.Graphics.FillRectangle(Brushes.Black, 0, 2 * height / 3 - 10, width, 10);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 1)
                    {
                        DrawX(i, j, e);
                    }
                    else if (board[i, j] == 2)
                    {
                        DrawO(i, j, e);
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != 0)
                {
                    Win(turn, 1, i, e);
                }
            }
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == board[1, j] && board[1, j] == board[2, j] && board[0, j] != 0)
                {
                    Win(turn, 2, j, e);
                }
            }
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != 0)
            {
                Win(turn, 3, 1, e);
            }
            if (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2] && board[2, 0] != 0)
            {
                Win(turn, 3, 2, e);
            }
        }
        private void Win(int turn, int method, int position, PaintEventArgs e)
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            String winner;
            win = true;
            if (method == 2)
            {
                e.Graphics.DrawLine(new Pen(Color.Green, height / 10), 0, position * (height / 3) + height / 6, width, position * (height / 3) + height / 6);
            }
            else if (method == 1)
            {
                e.Graphics.DrawLine(new Pen(Color.Green, width / 10), position * (width / 3) + width / 6, 0, position * (width / 3) + width / 6, height);
            }
            else if (method == 3 && position == 1)
            {
                e.Graphics.DrawLine(new Pen(Color.Green, width / 10), 0, 0, width, height);
            }
            else if (method == 3 && position == 2)
            {
                e.Graphics.DrawLine(new Pen(Color.Green, width / 10), width, 0, 0, height);
            }
            if ((turn-1) % 2 == 0)
            {
                winner = "X";
            }
            else winner = "O";
            String winText = winner + "'s has won!";
            SizeF textSize = e.Graphics.MeasureString(winText, new Font("Times New Roman", 72));
            e.Graphics.DrawString(winText, new Font("Times New Roman", 72), Brushes.Orange, width / 2 - (textSize.Width / 2), height / 2 - (textSize.Height / 2));
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!win)
            {
                int hChunk = this.ClientSize.Height / 3;
                int wChunk = this.ClientSize.Width / 3;
                int i = e.X / wChunk;
                int j = e.Y / hChunk;
                if (turn % 2 == 0 && board[i, j] == 0)
                {
                    board[i, j] = 1;
                    turn++;
                }
                else if (turn % 2 == 1 && board[i, j] == 0)
                {
                    board[i, j] = 2;
                    turn++;
                }
                this.Refresh();
            }
        }
        private void DrawX(int i, int j, PaintEventArgs e)
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            e.Graphics.DrawLine(new Pen(Color.Red, width / 30), i * (width / 3) + width / 6 - width / 10, j * (height / 3) + height / 6 - height / 10, i * (width / 3) + width / 6 + width / 10, j * (height / 3) + height / 6 + height / 10);
            e.Graphics.DrawLine(new Pen(Color.Red, width / 30), i * (width / 3) + width / 6 - width / 10, j * (height / 3) + height / 6 + height / 10, i * (width / 3) + width / 6 + width / 10, j * (height / 3) + height / 6 - height / 10);
        }
        private void DrawO(int i, int j, PaintEventArgs e)
        {
            int width = this.ClientSize.Width;
            int height = this.ClientSize.Height;
            e.Graphics.DrawEllipse(new Pen(Color.Blue, width / 15), new Rectangle(i*(width/3)+width/6-width/50, j*(height/3)+height/6-height/50, width/25, height/25));
        }
    }
}
