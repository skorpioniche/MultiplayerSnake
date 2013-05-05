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
using System.Net.Sockets;

namespace Snake
{
    public partial class FormMain : Form
    {
        

        public FormMain()
        {
            InitializeComponent();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }
        public static Random rand = new Random();
        Game G;
        private delegate void UpdateStatusCallback(string strMessage);
        private delegate void HttpServerDelegate(int port);
        private static IPAddress ipAddr = IPAddress.Parse("0.0.0.0");
        private ChatServer mainServer= new ChatServer(ipAddr);

        private Thread ThreadServerGamer;
        private Thread BotThread;
        private HtmlGetServer ServerHttp;
        private TcpListener ServerListner;
        
        private void OnApplicationExit(object sender, EventArgs e)
        {
            buttonRestart_Click(sender, e);
        }
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
            
        }

        private void StartGame()
        {
            G.Start();
            ThreadServerGamer = new Thread(new ParameterizedThreadStart(StartServer));
            ThreadServerGamer.Start(10050);
            PropertiesBlock.snakes = G.Snakes;
            PropertiesBlock.gpPalette = Graphics.FromHwnd(this.pictureBox1.Handle);
            //  StartBot();
        }
        private void StartServer(object obj)
        {
            if (ServerListner==null)
            ServerListner = new TcpListener(IPAddress.Any, (int)obj);
            ServerHttp = new HtmlGetServer((int)obj, ServerListner);
        }

        #region Bot
        private void StartBot()
        {
             BotThread = new Thread(new ParameterizedThreadStart(StartPlayer));
             BotThread.Start(0);
        }

        private void StartPlayer(object obj)
        {
            int i = (int)obj;
            for (; ; )
            {
                int j = 0;
                foreach (PlayerSnake pl in G.Snakes)
                {
                    try
                    {
                        Thread.Sleep(200);
                        if (j>0)
                        pl.Direction = DirectionState.Up;
                        j++;
                    }
                    catch { }
                }
                j = 0;
                foreach (PlayerSnake pl in G.Snakes)
                {
                    try
                    {
                        Thread.Sleep(200);
                        if (j > 0)
                        pl.Direction = DirectionState.Left;
                        j++;
                    }
                    catch { }
                }
                j = 0;
                foreach (PlayerSnake pl in G.Snakes)
                {
                    try
                    {
                        Thread.Sleep(200);
                        if (j > 0)
                        pl.Direction = DirectionState.Down;
                        j++;
                    }
                    catch { }
                }
                j = 0;
                foreach (PlayerSnake pl in G.Snakes)
                {
                    try
                    {
                        Thread.Sleep(200);
                        if (j > 0)
                        pl.Direction = DirectionState.Right;
                        j++;
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
                StartGame();
                ChatServer.StatusChanged += new StatusChangedEventHandler(mainServer_StatusChanged);
                mainServer.StartListening();
                txtLog.AppendText("Monitoring for connections...\r\n");
               // ChatServer.htUsers.
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

        private void buttonRestart_Click(object sender, EventArgs e)
        {
            if (BotThread!=null)
            BotThread.Abort();

            if (ServerListner!=null)
            ServerListner.Stop();
            if (G!=null)
            G.Dispose();
            if (ThreadServerGamer!=null)
            ThreadServerGamer.Abort();

        }



    }
}