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

        public void Movement(char key)
        {
            switch (key) {
                case 'w':
                    position[1]++;
                    break;
                case 's':
                    position[1]--;
                    break;
                case 'a':
                    position[0]--;
                    break;
                case 'd':
                    position[0]++;
                    break;
            }
            Console.WriteLine("New Player Position:\nX:"+position[0] + "\nY:"+ position[1]);
        }

        public int[] position = {};

        public string sprite = "■";

    }
}
