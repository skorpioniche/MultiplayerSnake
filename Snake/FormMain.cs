using Snake.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Snake.HttpServer;
using System.Threading;

namespace Snake
{
    public partial class FormMain : Form
    {
        

        public FormMain()
        {
            InitializeComponent();
        }
        public static Random rand = new Random();
        Game G;
        private delegate void UpdateStatusCallback(string strMessage);
        private delegate void HttpServerDelegate(int port);
        private static IPAddress ipAddr = IPAddress.Parse("0.0.0.0");
        private ChatServer mainServer= new ChatServer(ipAddr);
        private void FormMain_Load(object sender, EventArgs e)
        {

            int width, height, size;
            width = height = 30;
            size = 20;

            this.pictureBox1.Width = width * size;
            this.pictureBox1.Height = height * size;
          //  this.Width = pictureBox1.Width + 30;
           // this.Height = pictureBox1.Height + 60;
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

        private void buttonStartGame(object sender, EventArgs e)
        {
            G.Start();
            Thread Thread = new Thread(new ParameterizedThreadStart(StartServer));
            Thread.Start(10050);               
            StartBot();
           

        }

        private void StartServer(object obj)
        {
            new HtmlGetServer((int)obj);
        }

        #region Bot
        private void StartBot()
        {
            Thread Thread = new Thread(new ParameterizedThreadStart(StartPlayer));
            Thread.Start(0);
        }

        private void StartPlayer(object obj)
        {
            int i = (int)obj;
            for (; ; )
            {
                foreach (PlayerSnake pl in G.Snakes)
                {
                    try
                    {
                        Thread.Sleep(200);
                        pl.Direction = DirectionState.Up;
                    }
                    catch { }
                }
                foreach (PlayerSnake pl in G.Snakes)
                {
                    try
                    {
                        Thread.Sleep(200);
                        pl.Direction = DirectionState.Left;
                    }
                    catch { }
                }
                foreach (PlayerSnake pl in G.Snakes)
                {
                    try
                    {
                        Thread.Sleep(200);
                        pl.Direction = DirectionState.Down;
                    }
                    catch { }
                }
                foreach (PlayerSnake pl in G.Snakes)
                {
                    try
                    {
                        Thread.Sleep(200);
                        pl.Direction = DirectionState.Right;
                    }
                    catch { }
                }
            }

        }

        #endregion Bot

        private void buttonStartChat_Click(object sender, EventArgs e)
        {
            if (mainServer.Work)
            {

            }
            else
            {
                ChatServer.StatusChanged += new StatusChangedEventHandler(mainServer_StatusChanged);
                mainServer.StartListening();
                txtLog.AppendText("Monitoring for connections...\r\n");
            }
        }

        public void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            this.Invoke(new UpdateStatusCallback(this.UpdateStatus), new object[] { e.EventMessage });
        }

        private void UpdateStatus(string strMessage)
        {
            txtLog.AppendText(strMessage + "\r\n");
        }

    }
}