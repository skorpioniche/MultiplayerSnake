using System;
using System.Drawing;

namespace Snake
{

    class Block
    {
        private Color _color;
        private int _size; 
        private Point _point;

        
        public Block(Color color, int size, Point p)
        {
            this._color = color;
            this._size = size;
            this._point = p;
        }

        public Point Point
        {
            get { return this._point; }
        }

        public virtual void Paint(Graphics g)
        {
            SolidBrush sb = new SolidBrush(_color);
            lock (g)
            {
                try
                {
                    g.FillRectangle(sb, this.Point.X * this._size, this.Point.Y * this._size, this._size - 1, this._size - 1);
                }
                catch 
                { }
            }
        }
    }
}
