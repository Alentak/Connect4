using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            string play = "y";

            while (play == "y")
            {
                Game game = new Game();
                game.Start();

                Console.WriteLine("\n\nContinue ? y/n");
                play = Console.ReadLine();
            }
        }
    }
}
