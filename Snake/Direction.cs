using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Snake
{
    public enum DirectionState
    {
        Left,
        Right,
        Up,
        Down
    }
    static class Direction
    {
        public static void SetDirection(KeyEventArgs e, List<PlayerSnake> p)
        {
            #region client control
            if ((e.KeyCode == Keys.Up) && p[0].Direction != DirectionState.Down)
            {
                p[0].Direction = DirectionState.Up;
                return;
            }
            if ((e.KeyCode == Keys.Right) && p[0].Direction != DirectionState.Left)
            {
                p[0].Direction = DirectionState.Right;
                return;
            }
            if ((e.KeyCode == Keys.Down) && p[0].Direction != DirectionState.Up)
            {
                p[0].Direction = DirectionState.Down;
                return;
            }
            if ((e.KeyCode == Keys.Left) && p[0].Direction != DirectionState.Right)
            {
                p[0].Direction = DirectionState.Left;
                return;
            }
            #endregion


            if ((e.KeyCode == Keys.W) && p[1].Direction != DirectionState.Down)
            {
                p[1].Direction = DirectionState.Up;
                return;
            }
            if ((e.KeyCode == Keys.D) && p[1].Direction != DirectionState.Left)
            {
                p[1].Direction = DirectionState.Right;
                return;
            }
            if ((e.KeyCode == Keys.S) && p[1].Direction != DirectionState.Up)
            {
                p[1].Direction = DirectionState.Down;
                return;
            }
            if ((e.KeyCode == Keys.A) && p[1].Direction != DirectionState.Right)
            {
                p[1].Direction = DirectionState.Left;
                return;
            }
        }

    }

}
