using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public partial class FormMain : Form
    {
        

        public FormMain()
        {
            InitializeComponent();
        }

        Game G;
        private void FormMain_Load(object sender, EventArgs e)
        {

            int width, height, size;
            width = height = 30;
            size = 20;

            this.pictureBox1.Width = width * size;
            this.pictureBox1.Height = height * size;
            this.Width = pictureBox1.Width + 30;
            this.Height = pictureBox1.Height + 60;
            G = new Game(width, height, size, Graphics.FromHwnd(this.pictureBox1.Handle));

        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            Direction.SetDirection(e, G.Snakes);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

       //         if (G != null)
         //       {
           //         G.PaintPalette(e.Graphics);
             //   }
            
        }
    }
}