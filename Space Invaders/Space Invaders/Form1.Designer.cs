﻿namespace Space_Invaders
{
    partial class Engine
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Engine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Name = "Engine";
            this.Text = "Form1";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Engine_Pause);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Engine_PlayerShoot);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Engine_PauseClicks);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Engine_PlayerMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

