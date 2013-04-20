using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeClient
{
    public enum DirectionState
    {
        Left,
        Right,
        Up,
        Down
    }

    class Direction
    {
        public static void SetDirection(KeyEventArgs e,string url,string id)
        {
            if (e.KeyCode == Keys.Up)
            {
                string response = Http.SendGETRequest(url, id+"u");
                return;
            }
            if (e.KeyCode == Keys.Right)
            {
                string response = Http.SendGETRequest(url, id + "r");
                return;
            }
            if (e.KeyCode == Keys.Down) 
            {
                string response = Http.SendGETRequest(url, id + "d");
                return;
            }
            if (e.KeyCode == Keys.Left) 
            {
                string response = Http.SendGETRequest(url, id + "l");
                return;
            }
        }
    }
}
