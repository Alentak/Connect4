using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4
{
    class sc
    {
        public static void wl(string text)
        {
            Console.WriteLine(text);
        }
        public static void w(string text)
        {
            Console.Write(text);
        }
        public static string rl()
        {
            return Console.ReadLine();
        }
        public static int toint()
        {
            return Convert.ToInt32(rl());
        }
        public static void clr()
        {
            Console.Clear();
        }
        public static void bg(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }
    }
}
