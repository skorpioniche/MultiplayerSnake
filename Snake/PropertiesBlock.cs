using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Snake
{
    static class PropertiesBlock
    {
        public static int width = 30;
        public static int height = 30;
        public static int size = 20;
        public static List<PlayerSnake> snakes;
        public static bool GameIsStarted=false;

        public static System.Drawing.Graphics gpPalette;

        public static Color GetColorByCountSnake(int snakesCount)
        {
            switch (snakesCount)
            {
                case 0:
                    return Color.Red;
                case 1:
                    return (Color.Blue);
                case 2:
                    return(Color.Green);
                case 3:
                    return(Color.Brown);
                case 4:
                    return(Color.Gold);
                case 5:
                   return(Color.Ivory);
                case 6:
                    return (Color.Magenta);
                case 7:
                    return(Color.Silver);
                default:
                    return (Color.Snow);
            }
        }
        public static int GetIdByName(String namePlayer)
        {
            int i = 0;
            foreach (PlayerSnake sn in snakes)
            {
                if (sn.NamePlayer == namePlayer)
                    return i;
                i++;
            }
            return 9;
        }
    }
}
