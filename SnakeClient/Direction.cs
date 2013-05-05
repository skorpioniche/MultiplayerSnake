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
        public static DirectionState CurrentDirection=DirectionState.Right;
        public static void SetDirection(KeyEventArgs e,string url,string id)
        {
            if ((e.KeyCode == Keys.Up)&& (CurrentDirection!=DirectionState.Down))
            {
                string response = Http.SendGETRequest(url, id+"u/");
                CurrentDirection = DirectionState.Up;
                return;
            }
            if ((e.KeyCode == Keys.Right) && (CurrentDirection != DirectionState.Left))
            {
                string response = Http.SendGETRequest(url, id + "r/");
                CurrentDirection = DirectionState.Right;
                return;
            }
            if ((e.KeyCode == Keys.Down) && (CurrentDirection != DirectionState.Up))
            {
                string response = Http.SendGETRequest(url, id + "d/");
                CurrentDirection = DirectionState.Down;
                return;
            }
            if ((e.KeyCode == Keys.Left) && (CurrentDirection != DirectionState.Right))
            {
                string response = Http.SendGETRequest(url, id + "l/");
                CurrentDirection = DirectionState.Left;
                return;
            }
        }
    }
}
