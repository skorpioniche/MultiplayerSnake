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
        private int _width = 30;
        private int _height = 30;
        private int _level=5;
       // private int[] _speed = new int[] { 500, 450, 400, 350, 300, 250, 200, 150, 100, 50 };

        public Game(int width, int height, int size, Graphics g )
        {
            this._gpPalette = g;
            snakes.Add(new PlayerSnake(width, height, size, Color.Red, g, 5,"player1")); 
            snakes.Add(new PlayerSnake(width, height, size, Color.Blue, g, 5,"player2"));
            snakes.Add(new PlayerSnake(width, height, size, Color.Green, g, 5, "player3"));
            
        }


        public void Start()
        {
            Foods.Food.Add(Foods.GetFood(allBlocks));
            timerBlock = new System.Timers.Timer(200);//_speed[this._level]);
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
            List<PlayerSnake> newSnakes = new List<PlayerSnake>();
            foreach (PlayerSnake snake in snakes)
            {
                if (!snake.IsGameOver)
                 //   System.Windows.Forms.MessageBox.Show(snake.NamePlayer + "Game Over");
               // else
                    newSnakes.Add(snake);
            }

            if (newSnakes.Count < 2)
            {
                this.timerBlock.Stop();
                this.timerBlock.Dispose();
                if (newSnakes.Count==1)
                    System.Windows.Forms.MessageBox.Show(newSnakes[0].NamePlayer + "  win!");
                if (newSnakes.Count==0)
                    System.Windows.Forms.MessageBox.Show("AllPlayer crash!");
                return;
            }
            else
            {
                snakes = newSnakes;
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
            if (head.Point.X < 0 || head.Point.Y < 0 || head.Point.X >= _width || head.Point.Y >= _height)
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
