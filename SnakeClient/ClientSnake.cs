using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SnakeClient
{
    class ClientSnake
    {
        private ArrayList allBlocks = new ArrayList();
        private int width = 30;
        private int height = 30;
        private int sizeBlock=20;
        private Graphics graphics;
        private string url;

        public ClientSnake( Graphics gp,string  url)
        {
            
            graphics=gp;
            this.url = url;
        }

        public void Update()
        {
            string response= Http.SendGETRequest(url, "");
            string codingBlockInString = ParseXmlResponse(response);
            GenereteBlockFromRequest(codingBlockInString);
            PaintPalette(graphics);
        }

        private void GenereteBlockFromRequest(string codingBlockInString)
        {
            allBlocks.Clear();
            for (int i = 0; i < codingBlockInString.Length; i++)
            {
                switch (codingBlockInString[i])
                {
                    case '9':                   
                        break;
                    case '0':
                        allBlocks.Add(new Block(Color.Red,sizeBlock,CalcPoint(i)));
                        break;
                    case '1':
                        allBlocks.Add(new Block(Color.Blue, sizeBlock, CalcPoint(i)));
                        break;
                    case '2':
                        allBlocks.Add(new Block(Color.Green, sizeBlock, CalcPoint(i)));
                        break;
                    case '8':
                        allBlocks.Add(new Block(Color.Black, sizeBlock, CalcPoint(i)));
                        break;
                    default:
                        break;
                }
                
            }
        }

        private Point CalcPoint(int i)
        {
            return new Point(i % height, i / width);
        }

        private string ParseXmlResponse(string response)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(response);
            XmlNodeList items = xml.GetElementsByTagName("Block");
            return items.Item(0).InnerText; 
        }
        public void PaintPalette(Graphics gp)
        {
            gp.Clear(Color.White);
            foreach (Block b in allBlocks)
                b.Paint(gp);
        }
    }
}
