using Dz;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dz
{
    public class Draw
    {
        Config config = new Config();
       // Init init = new Init();

        public void drw(char[,] field)
        {
            Console.Clear();
            for (int i = 0; i < config.height; i++)
            {
                for (int o = 0; o < config.width; o++)
                {
                    switch (field[i, o])
                    {
                        case '#':
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.Write(' ');
                            break;
                        case ' ':
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(' ');
                            break;
                        case '0' :
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write(config.person);
                            break;
                        case 'o':
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(config.bomb);
                            break;
                        case 'x':
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write('x');
                            break;
                        default: 
                            Console.Write("Wtf");
                            break;
                    }
                }
                Console.WriteLine("");
            }
            Console.ResetColor();
        }
    }
}