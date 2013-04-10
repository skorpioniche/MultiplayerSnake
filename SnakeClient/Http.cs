using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeClient
{
    class Http
    {
        public static string SendGETRequest(string Url, string Data)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(Url + "/" + Data);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();

            return Out;
        }
    }
}
