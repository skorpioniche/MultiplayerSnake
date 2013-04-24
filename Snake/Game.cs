using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Timers;

namespace Snake
{
    class Game
    {
        private List<PlayerSnake> snakes = new List<PlayerSnake>();
        public static string CodingBlockInString;
        public List<PlayerSnake> Snakes
        {
            get
            {
                return snakes;
            }
            set { }
        }

        private ArrayList allBlocks = new ArrayList(); //хранит все блоки игроков


        private Timer timerBlock;
        private int respFood;
        private int timeRespFood = 10;
        private Graphics _gpPalette;
        private Color _bgColor = Color.White; 
        private int width = 30;
        private int height = 30;
        private int size = 30;
        private int _level=5;
       // private int[] _speed = new int[] { 500, 450, 400, 350, 300, 250, 200, 150, 100, 50 };
        private int speed = 400;

        public Game(int width, int height, int size, Graphics g )
        {
            this.width = width;
            this.height = height;
            this.size = size;
            this._gpPalette = g;
        }

        public virtual void Dispose()
        {
            timerBlock.Stop();
            snakes.Clear();
            allBlocks.Clear();
            Foods.Food.Clear();
            _gpPalette.Clear(this._bgColor);
        }


        public void Start()
        {
            snakes.Add(new PlayerSnake(width, height, size, Color.Red, _gpPalette, 5, "player1"));
            snakes.Add(new PlayerSnake(width, height, size, Color.Blue, _gpPalette, 5, "player2"));
            snakes.Add(new PlayerSnake(width, height, size, Color.Green, _gpPalette, 5, "player3"));

            Foods.Food.Add(Foods.GetFood(allBlocks));
            timerBlock = new System.Timers.Timer(speed);//_speed[this._level]);
            timerBlock.Elapsed += new System.Timers.ElapsedEventHandler(OnBlockTimedEvent);
            timerBlock.AutoReset = true;
            timerBlock.Start();
        }


        private void OnBlockTimedEvent(object source, ElapsedEventArgs e)
        {
            foreach (PlayerSnake snake in snakes)
            {
                snake.Move();
                snake.IsGameOver=CheckDead((Block)(snake.Blocks[0]));
            }
            UpdateAllBlock();
            PaintPalette(this._gpPalette);
            CheckEndGame();
            CheckRespFood();
            UpdateStringAllBlock();
        }

        private void CheckRespFood()
        {
            respFood++;
            if (respFood > timeRespFood)
            {
                Foods.Food.Add(Foods.GetFood(allBlocks));
                respFood = 0;
            }
        }

        private void CheckEndGame()
        {
            List<String> SnakeLive = new List<String>();
            foreach (PlayerSnake snake in snakes)
            {
                if (!snake.IsGameOver)
                    SnakeLive.Add(snake.NamePlayer);
            }

            if (SnakeLive.Count < 2)
            {
                this.timerBlock.Stop();
                this.timerBlock.Dispose();
                if (SnakeLive.Count == 1)
                    System.Windows.Forms.MessageBox.Show(SnakeLive[0] + "  win!");
                if (SnakeLive.Count == 0)
                    System.Windows.Forms.MessageBox.Show("AllPlayer crash!");
                return;
            }
            else
            {
                int level = allBlocks.Count / 10 % 10;
                if (this._level != level)
                {
                    this._level = level;
                   // this.timerBlock.Interval = this._speed[_level];
                }
            }
        }

        private bool CheckDead(Block head)
        {
            if (head.Point.X < 0 || head.Point.Y < 0 || head.Point.X >= width || head.Point.Y >= height)
                return true;
            foreach (Block block in allBlocks)
            {
                if (head.Point.X == block.Point.X && head.Point.Y == block.Point.Y)
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateAllBlock()
        {
            allBlocks.Clear();
            foreach (PlayerSnake snake in snakes)
            {
                foreach (Block block in snake.Blocks)
                    allBlocks.Add(block);
            }
        }



        public void PaintPalette(Graphics gp)
        {
            gp.Clear(this._bgColor);
            foreach (Block food in Foods.Food)
            {
                food.Paint(gp);
            }
            foreach (Block b in allBlocks)
                b.Paint(gp);
        }

        //кодируем блоки как 9 -пусто
        //8-еда
        //7-0 номера индексов игроков
        public void UpdateStringAllBlock()
        {
            Char[] CodingBlock = new Char[PropertiesBlock.width * PropertiesBlock.height];
            for (int i=0; i<CodingBlock.Length;i++)
                CodingBlock[i] = '9';
            foreach (Block food in Foods.Food)
            {
                if ((food.Point.X>=0 && food.Point.X<PropertiesBlock.width)&&(food.Point.Y>=0 && food.Point.Y<PropertiesBlock.height))
                    CodingBlock[food.Point.X + food.Point.Y * PropertiesBlock.width] = '8';
            }
            foreach (Block b in allBlocks)
            {
                if ((b.Point.X >= 0 && b.Point.X < PropertiesBlock.width) && (b.Point.Y >= 0 && b.Point.Y < PropertiesBlock.height))
                    CodingBlock[b.Point.X + b.Point.Y * PropertiesBlock.width] = Convert.ToChar(GetIdByColor(b.ColorBlock).ToString());
            }
            CodingBlockInString = new string(CodingBlock);           
        }

        //определяет номер инекса игрока по цвету
        public int GetIdByColor(Color colorSnake)
        {
            int i=0;
            foreach (PlayerSnake sn in snakes)
            {
                if (sn.ColorSnake == colorSnake)
                    return i;
                i++;
            }
            return 9;
        }



    }
}
