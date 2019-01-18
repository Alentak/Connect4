using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class Game
    {
        public Player currentPlayer { get; set; }
        public Player p1 { get; set; }
        public Player p2 { get; set; }
        public ConsoleColor[][] grid { get; set; }
        public ConsoleColor[] menuColors = new ConsoleColor[7];
        int cursorMenu = 3;
        bool won = false;

        public Game()
        {
            //Init grid
            grid = new ConsoleColor[7][];

            for (int x = 0; x < grid.Length; x++)
                grid[x] = new ConsoleColor[6];

            for (int x = 0; x < 7; x++)
                for (int y = 0; y < 6; y++)
                    grid[x][y] = ConsoleColor.Blue;

            //Init players
            p1 = new Player(ConsoleColor.Yellow);
            p2 = new Player(ConsoleColor.Red);

            sc.wl("First player, what's your name ?");
            p1.name = sc.rl();
            sc.wl("Second player, what's your name ?");
            p2.name = sc.rl();

            //Init first player
            currentPlayer = p1;

            //Init menu colors
            for (int i = 0; i < 7; i++)
                menuColors[i] = ConsoleColor.Black;
            menuColors[cursorMenu] = currentPlayer.color;

            sc.clr();
        }

        public void Show()
        {
            Console.WriteLine();

            //Grid
            for (int y = 0; y < 6; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    sc.w(" ");
                    sc.bg(grid[x][y]);
                    sc.w(" ");
                    sc.bg(ConsoleColor.Black);
                    sc.w(" ");
                }
                sc.wl("\n");
            }
            sc.w(" ");
            for (int i = 0; i < 7; i++)
            {
                sc.bg(menuColors[i]);
                if (menuColors[i] == ConsoleColor.Black)
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.Black;
                sc.w((i + 1) + "");
                sc.bg(ConsoleColor.Black);
                sc.w("  ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            sc.wl("");
            sc.bg(currentPlayer.color);
            sc.w("\n ");
            sc.bg(ConsoleColor.Black);
            sc.w(" " + currentPlayer.name + "'s turn... ");
            sc.bg(currentPlayer.color);
            sc.w(" ");
            sc.bg(ConsoleColor.Black);
        }

        public void Start()
        {
            Turn();
            if (won)
                sc.wl("\n\n" + currentPlayer.name + " won !");
            else
                sc.wl("\n\nNobody won :c");
        }

        public void Turn()
        {
            bool menu = true;
            cursorMenu = 3;
            menuColors[3] = currentPlayer.color;

            ConsoleKeyInfo entry;

            sc.clr();
            Show();

            //While nobody won and grid is not filled
            while (true)
            {
                while (menu)
                {
                    sc.clr();
                    Show();
                    entry = Console.ReadKey();

                    switch (entry.Key)
                    {
                        case ConsoleKey.Enter:
                            {
                                if (!Fill(cursorMenu))// -> arrays start at 0
                                    Console.WriteLine("Column already filled !");
                                else
                                {
                                    menu = false;
                                    sc.clr();
                                    Show();
                                }
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            {
                                menuColors[cursorMenu] = ConsoleColor.Black;

                                if (cursorMenu == 0)
                                    cursorMenu = 6;
                                else
                                    cursorMenu--;

                                menuColors[cursorMenu] = currentPlayer.color;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            {
                                menuColors[cursorMenu] = ConsoleColor.Black;

                                if (cursorMenu == 6)
                                    cursorMenu = 0;
                                else
                                    cursorMenu++;

                                menuColors[cursorMenu] = currentPlayer.color;
                            }
                            break;
                    }
                }

                //If someone won or if nobody won -> stop
                if (CheckIfWon())
                    break;
                else
                    if (CheckIsFilled())
                    break;

                //Change turn
                if (currentPlayer == p1)
                    currentPlayer = p2;
                else
                    currentPlayer = p1;

                Turn();
            }
        }

        private bool Fill(int column)
        {
            //If last slot is empty return true
            if (grid[column][0] == ConsoleColor.Blue)
            {
                for (int y = 5; y >= 0; y--)
                {
                    if (grid[column][y] == ConsoleColor.Blue)
                    {
                        grid[column][y] = currentPlayer.color;
                        break;
                    }
                }
                return true;
            }
            else
                return false;
        }

        private bool CheckIfWon()
        {
            //Horizontal Check
            for (int x = 0; x < 4; x++)//Check from left side
                for (int y = 0; y < 6; y++)
                    if (grid[x][y] == grid[x + 1][y] && grid[x][y] == grid[x + 2][y] && grid[x][y] == grid[x + 3][y] && grid[x][y] != ConsoleColor.Blue)
                        won = true;
            //Vertical Check
            for (int x = 0; x < 7; x++)
                for (int y = 0; y < 3; y++)//Check from top side
                    if (grid[x][y] == grid[x][y + 1] && grid[x][y] == grid[x][y + 2] && grid[x][y] == grid[x][y + 3] && grid[x][y] != ConsoleColor.Blue)
                        won = true;
            //Diagonal Check

            for (int x = 0; x < 4; x++)//Top left corner check
                for (int y = 0; y < 3; y++)
                    if (grid[x][y] == grid[x + 1][y + 1] && grid[x][y] == grid[x + 2][y + 2] && grid[x][y] == grid[x + 3][y + 3] && grid[x][y] != ConsoleColor.Blue)
                        won = true;

            for (int x = 3; x < 7; x++)//Top right corner check
                for (int y = 0; y < 3; y++)
                    if (grid[x][y] == grid[x - 1][y + 1] && grid[x][y] == grid[x - 2][y + 2] && grid[x][y] == grid[x - 3][y + 3] && grid[x][y] != ConsoleColor.Blue)
                        won = true;

            return won;
        }

        public bool CheckIsFilled()
        {
            bool check;
            int nbNotBlue = 42;

            foreach (ConsoleColor[] column in grid)
                foreach (ConsoleColor line in column)
                    if (line != ConsoleColor.Blue)
                        nbNotBlue--;

            //If filled = 0 blue slot
            if (nbNotBlue == 0)
                check = true;
            else
                check = false;

            return check;
        }
    }
}
