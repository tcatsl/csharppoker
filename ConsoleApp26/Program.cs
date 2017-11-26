using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    class Program
    {
        static void Main(string[] args)
        {
            //your hand
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            PokerHand test1 = new PokerHand("AD KD QD JD TD 3D AH");
            //their hand
            PokerHand test2 = new PokerHand("AS 2S 3H 4S 5S 2D TS");
            
            //you win. high card.System.Threading.Thread.Sleep(500); Console.WriteLine(test1.CompareWith(test2).ToString() + test1.flush + test2.flush + string.Join(",", test1.nums));
            Game.GameStart();
            
        }
    }

}
