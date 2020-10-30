using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace Dz
{
    public class Init
    {
        Random rand = new Random();
        Config config = new Config();
        Draw draw = new Draw();
        public char[,] generator(out int persX, out int persY, float sharp_freq)
        {
            char[,] field = new char [config.height,config.width];
            {
                for (int i = 0; i < config.height; i++)
                {
                    for (int o = 0; o < config.width; o++)
                    {
                        field[i, o] = randomizer(sharp_freq);
                    }
                }
            }
            checkPerson(ref field, out persX, out persY);
            checkEnd(ref field);
            draw.drw(field);
            return field;
        }
        public char randomizer(float sharp_freq)
        {
            float chance1 = rand.Next(0, 10000)/100;
            float chance2 = rand.Next(0, 10000) / 100;
            char m = (chance1<sharp_freq? '#' : ' ');
            if(m == ' ')
            m = (chance2 < ((config.bomb_freq / 100) / (sharp_freq / 100)) ? 'o' : ' ');
            return m;
        }
        public (int,int) GeneratePerson()
        {
            int persX, persY;
            persX = rand.Next(config.width - 1);
            persY = rand.Next(config.height - 1);
            return (persX,persY);
        }
        public void checkPerson(ref char[,] field, out int persX, out int persY)
        {
            (persX, persY) = GeneratePerson();
            while (field[persY, persX] != ' ')
            {
                (persX, persY) = GeneratePerson();
            }
            field[persY, persX] = '0';
        }
        public (int, int) GenerateEnd()
        {
            int endX, endY;
            endX = rand.Next(config.width - 1);
            endY = rand.Next(config.height - 1);
            return (endX, endY);
        }
        public void checkEnd(ref char[,] field)
        {
            int endX, endY;
            (endX, endY) = GenerateEnd();
            while (field[endY, endX] != ' ')
            {
                (endX, endY) = GenerateEnd();
            }
            field[endY, endX] = 'x';
        }

    }
}
