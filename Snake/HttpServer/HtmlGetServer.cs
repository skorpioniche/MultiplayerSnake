using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Snake.HttpServer
{
    class HtmlGetServer
    {
        private TcpListener Listener;

        public HtmlGetServer(int Port, TcpListener listener)
        {
            Listener = listener;
            if (Listener==null)
                Listener = new TcpListener(IPAddress.Any, Port); 
            Listener.Start();

            while (true)
            {
                // Принимаем новых клиентов. После того, как клиент был принят, он передается в новый поток (ClientThread)
                // с использованием пула потоков.
                ThreadPool.QueueUserWorkItem(new WaitCallback(ClientThread), Listener.AcceptTcpClient());

                /*
                // Принимаем нового клиента
                TcpClient Client = Listener.AcceptTcpClient();
                // Создаем поток
                Thread Thread = new Thread(new ParameterizedThreadStart(ClientThread));
                // И запускаем этот поток, передавая ему принятого клиента
                Thread.Start(Client);
                */
            }
        }


        static void ClientThread(Object StateInfo)
        {
            new HtmlClient((TcpClient)StateInfo);
        }

        ~HtmlGetServer()
        {
            if (Listener != null)
            {
                Listener.Stop();
            }
        }
    }
}
