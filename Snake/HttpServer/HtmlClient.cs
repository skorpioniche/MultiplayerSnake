using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;


namespace Snake.HttpServer
{
    class HtmlClient
    {
        private void SendError(TcpClient Client, int Code)
        {

            string CodeStr = Code.ToString() + " " + ((HttpStatusCode)Code).ToString();
            string Html = "<html><body><h1>" + CodeStr + "</h1></body></html>";
            string Str = "HTTP/1.1 " + CodeStr + "\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
            byte[] Buffer = Encoding.ASCII.GetBytes(Str);
            Client.GetStream().Write(Buffer, 0, Buffer.Length);
            Client.Close();
        }

        public HtmlClient(TcpClient Client)
        {
            string Request = "";
            byte[] Buffer = new byte[1024];
            int Count;
            // Читаем из потока клиента до тех пор, пока от него поступают данные
            while ((Count = Client.GetStream().Read(Buffer, 0, Buffer.Length)) > 0)
            {
                Request += Encoding.ASCII.GetString(Buffer, 0, Count);
                if (Request.IndexOf("\r\n\r\n") >= 0 || Request.Length > 4096)
                {
                    break;
                }
            }
            Match ReqMatch = Regex.Match(Request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");
            if (ReqMatch == Match.Empty)
            {
                SendError(Client, 400);
                return;
            }
            string RequestUri = ReqMatch.Groups[1].Value;
            RequestUri = Uri.UnescapeDataString(RequestUri);
            Console.WriteLine(RequestUri); //

            byte[] response = CreateResponse();
            Client.GetStream().Write(response, 0, response.Length);
            Client.Close();
        }

        private byte[] CreateResponse()
        {
                        XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("Root",
            new XElement("Child1", "пизда рулю"),
            new XElement("Child2", 2),
            new XElement("Child3", 3),
            new XElement("Child4", 4),
            new XElement("Child5", 5),
            new XElement("Info8", "info8")
            )
            );
            string Html = "<html><body><h1>It works!</h1></body></html>";
            string Str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
            byte[] Buffer = Encoding.ASCII.GetBytes(Str);
            return Buffer;
        }

    }
}
