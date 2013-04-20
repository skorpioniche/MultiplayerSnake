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
            byte[] response;
            if (RequestUri == "//")
            {
               response = CreateResponse();               
            }
            else
            {
                response = CreateResponseOK();
                CreateResponseDirection(RequestUri);
            }
            Client.GetStream().Write(response, 0, response.Length);
            Client.Close();
        }

        private void CreateResponseDirection(string RequestUri)
        {
            try
            {
                int PlayerId = Convert.ToInt32(RequestUri[2]) - Convert.ToInt32('0');
                DirectionState PlayerDirection = GetDirectionByRequest(RequestUri[3]);
                PropertiesBlock.snakes[PlayerId].Direction = PlayerDirection;
            }
            catch
            {
            }
        }

        private DirectionState GetDirectionByRequest(char p)
        {
            DirectionState NewDirect = DirectionState.Up;
            switch (p)
            {
                case 'u':
                    NewDirect = DirectionState.Up;
                    break;
                case 'd':
                    NewDirect = DirectionState.Down;
                    break;
                case 'l':
                    NewDirect = DirectionState.Left;
                    break;
                case 'r':
                    NewDirect = DirectionState.Right;
                    break;
            }
            return NewDirect;

        }

        private byte[] CreateResponse()
        {
            XDocument HtmlDocument = new XDocument(
            new XComment("This is a comment"),
            new XElement("html",
            new XElement("body",
            new XElement("Height", PropertiesBlock.height),
            new XElement("Width", PropertiesBlock.width),
             new XElement("Size", PropertiesBlock.size),
            new XElement("Block", Game.CodingBlockInString)
            )
            )
            );
            string Html = HtmlDocument.ToString();
            string Str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
            byte[] Buffer = Encoding.ASCII.GetBytes(Str);
            return Buffer;
        }

        private byte[] CreateResponseOK()
        {
            string Html = " ";
            string Str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
            byte[] Buffer = Encoding.ASCII.GetBytes(Str);
            return Buffer;
        }

    }
}
