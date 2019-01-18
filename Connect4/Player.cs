using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Player
    {
        public string name { get; set; }
        public int score { get; set; }
        public ConsoleColor color { get; set; }
        public static List<Player> listOfPlayers = new List<Player>();

        public Player(ConsoleColor color)
        {
            score = 0;
            this.color = color;
            listOfPlayers.Add(this);
        }

        public List<Player> GetListOfPlayers()
        {
            return listOfPlayers;
        }
    }
}
