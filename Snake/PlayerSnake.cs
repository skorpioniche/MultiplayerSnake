using System;
using System.Collections;
using System.Drawing;
using System.Timers;

namespace Snake
{
    // ÷чї≠∞е£ђ”√”Џїж÷∆”ќѕЈ
    class PlayerSnake
    {
        private int _width = 20; 
        private int _height = 20;
        private Color _bgColor= Color.White; 
        private Graphics _gpPalette; 
        private ArrayList _blocks;
        private Color colorSnake;
        public ArrayList Blocks
        {
            get
            {
                return _blocks;
            }
            set
            {
            }
        }
        private DirectionState _direction;
        private bool _directAble; 

      //  private Block _food; 
        private int _size = 20; 
        private int _level = 0;
        private bool _isGameOver;
        public bool IsGameOver
        {
            get
            {
                return _isGameOver;
            }
            set
            {
                _isGameOver = value;
            }
        }

        private string namePlayer;
        public string NamePlayer
        {
            get
            {
                return namePlayer;
            }
            set
            {
            }
        }
 


        public PlayerSnake(int width, int height, int size, Color _colorSnake, Graphics g, int lvl, string name)
        {
            this._width = width;
            this._height = height;
            colorSnake = _colorSnake;
            this._gpPalette = g;
            this._size = size;
            this._level = lvl;
            this._blocks = new ArrayList();
            this._blocks.Insert(0, (new Block(Color.Red, this._size, new Point(width / 2, height / 2))));
            this._direction = DirectionState.Right;
            _isGameOver = false;
            namePlayer = name;
        }

        public DirectionState Direction
        {
            get { return this._direction; }
            set
            {
                if (this._directAble)
                {
                    this._direction = value;
                    this._directAble = false;
                }
            }
        }

 




        private Point NextPositionMove()
        {
            Point p;
            Block head = (Block)(this._blocks[0]);
            if (this._direction == DirectionState.Up)
                p = new Point(head.Point.X, head.Point.Y - 1);
            else if (this._direction == DirectionState.Down)
                p = new Point(head.Point.X, head.Point.Y + 1);
            else if (this._direction == DirectionState.Left)
                p = new Point(head.Point.X - 1, head.Point.Y);
            else
                p = new Point(head.Point.X + 1, head.Point.Y);
            return p;
        }

        public void Move()
        {
            Point p = NextPositionMove();
            Block b = new Block(colorSnake, this._size, p);

            Block foodEat=null;
            foreach (Block food in Foods.Food)
            {
                if (food.Point == p)
                  foodEat=food;                    
            }
            if (foodEat == null)
                this._blocks.RemoveAt(this._blocks.Count - 1); //удал€ем блок если там не было еды
            else
                Foods.Food.Remove(foodEat);
            this._blocks.Insert(0, b);
    
            this._directAble = true; 
        }




    }



}
