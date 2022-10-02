using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{
    internal class Player
    {
        
        public string codeName;

        public void Movement(ConsoleKeyInfo key) // refine key input
        {
            switch (key.Key) {
                case ConsoleKey.W:
                    position[1]++;
                    break;
                case ConsoleKey.S:
                    position[1]--;
                    break;
                case ConsoleKey.A:
                    position[0]--;
                    break;
                case ConsoleKey.D:
                    position[0]++;
                    break;
            }
            Console.WriteLine("New Player Position:\nX:"+position[0] + "\nY:"+ position[1]);

        }

        public int[] position = {};

        public string sprite = "■";

    }
}
