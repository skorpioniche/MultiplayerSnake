using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Snake
{
    static class Foods
    {
        public static List<Block> Food = new List<Block>();

        public static Block GetFood( ArrayList allBlocks)
        {
            Block food = null;
            Random r = new Random();
            bool redo = false;
            while (true)
            {
                redo = false;
                int x = r.Next(PropertiesBlock.width);
                int y = r.Next(PropertiesBlock.height);
                foreach (Block block in allBlocks)
                {
                    if (block.Point.X == x && block.Point.Y == y)
                    {
                        redo = true;
                    }
                }
                if (redo == false)
                {
                    food = new Block(Color.Black, PropertiesBlock.size, new Point(x, y));
                    break;
                }
            }
            return food;
        }
    }
}
