using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace RealPokerTime
{
    class Program
    {
        static void Main(string[] args)
        {
            //your hand
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            PokerHand test1 = new PokerHand("AD KD QD JD TD 9D 3D");
            //their hand
            PokerHand test2 = new PokerHand("AS 2S 3S 4D 5S 6D 7S");
            //Console.WriteLine(test1.CompareWith(test2));
            System.Threading.Thread.Sleep(1000);  Console.WriteLine("welcome to real poker time v"+ Game.version);
            System.Threading.Thread.Sleep(500); Console.WriteLine("contribute at:"); System.Threading.Thread.Sleep(500); Console.WriteLine("https://github.com/tcatsl/csharppoker/");
            System.Threading.Thread.Sleep(1000);
            //you win. high card.System.Threading.Thread.Sleep(500); Console.WriteLine(test1.CompareWith(test2).ToString() + test1.flush + test2.flush + string.Join(",", test1.nums));
            Game.GameStart();
        }
    }


}
