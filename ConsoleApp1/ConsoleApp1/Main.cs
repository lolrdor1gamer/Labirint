using System;
using System.Timers;

namespace Dz
{
    class main
    {
        static void Main()
        {
            bool fals = false;
            Logic logic = new Logic();
            logic.NewGame(ref fals);
        }

    }
    class Logic
    {
        Init init = new Init();
        Draw draw = new Draw();
        Input input = new Input();
        main m = new main();
        Person person = new Person();
        Config config = new Config();
        public int rounds = 0;
        public float sharps;
        public char[,] field;
        public void Go(ref int persX, ref int persY, char[,] field, ref int bombs, ref bool won)
        {
            char key;
            input.TryInput(out key);
            int dx=0, dy=0;
            bool bomb = false;
            switch (key)
            {
                case 'W':
                    dy = -1;
                    break;
                case 'S':
                    dy = 1;
                    break;
                case 'A':
                    dx = -1;
                    break;
                case 'D':
                    dx = 1;
                    break;
                case 'B':
                    bomb = true;
                    break;
                default:
                    break;
            }
            if(TryToGo(dx, dy,  persX,  persY, field, ref won, ref bombs))
            {

                field[persY, persX] = ' ';
                persY += dy;
                persX += dx;
                field[persY, persX] = '0';
                if (bomb&& bombs >0)
                {
                    if(field[persY - 1, persX - 1] != 'x') field[persY - 1, persX - 1] = ' ';
                    if (field[persY - 1, persX + 1] != 'x') field[persY - 1, persX + 1] = ' ';
                    if (field[persY + 1, persX - 1] != 'x') field[persY + 1, persX - 1] = ' ';
                    if (field[persY + 1, persX + 1] != 'x') field[persY + 1, persX + 1] = ' ';
                    if (field[persY, persX - 1] != 'x') field[persY, persX - 1] = ' ';
                    if (field[persY, persX + 1] != 'x') field[persY, persX + 1] = ' ';
                    if (field[persY + 1, persX] != 'x') field[persY + 1, persX] = ' ';
                    if (field[persY - 1, persX] != 'x') field[persY - 1, persX] = ' ';
                    bombs -= 1;
                }
                draw.drw(field);
                Console.WriteLine("You have "+bombs+" bombs");
            }
        }
        void Game()
        {
            while (!person.won)
            {
                Go(ref person.persX, ref person.persY, field, ref person.bombs, ref person.won);
            }
            if (person.won)
            {
                Won();
            }
        }
        void Won()
        {
            Random random = new Random();
            char end;
            Console.Clear();
            Console.WriteLine("YOU WON");
            Console.WriteLine("ONE MORE TIME?");
            Console.WriteLine("                Y - Yes               N - No");
            input.TryInput(out end);
            if (end == 'Y')
            {
                rounds++;
                NewGame(ref person.won);
            }
            else if (end == 'N')
            {
                rounds++;
                Console.Clear();
                Console.WriteLine("You have passed " + rounds + " round");
                Environment.Exit(0);
            }
            else
            {
                input.TryInput(out end);
                Won();
            }
        }
        public void NewGame(ref bool won)
        {
            Random random = new Random();
            sharps = config.sharp_freq + (random.Next(500) / 100) * rounds;
            won = false;
            field = init.generator(out person.persX, out person.persY, sharps);
            Game();
        }
        bool TryToGo(int dx, int dy,  int persX,  int persY, char[,] field, ref bool won, ref int bombs)
        {
            if (persY + dy < 0 || persX + dx < 0 || persY + dy == config.height || persX + dx == config.width)
                return false;
            if (field[persY+dy, persX+dx] == '#')
            {
                return false;
            }
            else
            if(field[persY + dy, persX + dx] == 'o')
            {
                bombs += 1;
                return true;
            }
            else
            if(field[persY + dy, persX + dx] == ' ')
            {
                return true;
            }
            else
            if(field[persY + dy, persX + dx] == 'x')
            {
                won = true;
                return true;
            }
            else
            return true;
        }

    }

    class Person
    {
        public int persX, persY, bombs = 3;
        public bool won = false;
    }
    class Input
    {
        public char key; 
        public void TryInput(out char key)
        {
            ConsoleKeyInfo cki = Console.ReadKey();
            key = cki.Key.ToString().ToUpper().ToCharArray()[0];
           // Console.WriteLine(key);
        }
            
        
    }
}
