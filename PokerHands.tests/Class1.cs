using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ConsoleApp26.tests
{
    [TestFixture]
    public class HandTests
    {
        [Test]
        public void StraightFlushTests()
        {
            PokerHand test1 = new PokerHand("AD KD QD JD TD 9D 3D");
            //their hand
            PokerHand test2 = new PokerHand("AS 2S 3S 4S 5S 6S 7D");
            PokerHand.HandReducer(test1);
            PokerHand.HandReducer(test2);
            char[] seq = new char[] { '6', '5', '4', '3', '2' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test2.straightArr[u], seq.ToArray()[u]);
            }
            
        }
    }
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void AllInTest1()
        {
            Game.peeps = new List<Player>() { new Player(false, "Bob", 1000), new Player(false, "Jim", 1000) };
            foreach (Player to in Game.peeps)
            {
                to.folded = false;
            }
            Game.peeps[0].curr = 75;
            Game.peeps[1].curr = 50;
            Game.peeps[0].AllIn();
            Assert.AreEqual(975, Game.peeps[1].curr);
        }
        public void AllInTest2()
        {
            Game.peeps = new List<Player>() { new Player(false, "Bob", 1000), new Player(false, "Jim", 1000) };
            foreach (Player to in Game.peeps)
            {
                to.folded = false;
            }
            Game.peeps[0].curr = 50;
            Game.peeps[1].curr = 75;
            Game.peeps[0].AllIn();
            Assert.AreEqual(1025, Game.peeps[1].curr);
        }
        public void AllInTest3()
        {
            Game.peeps = new List<Player>() { new Player(false, "Bob", 1000), new Player(false, "Jim", 1000) };
            foreach (Player to in Game.peeps)
            {
                to.folded = false;
            }
            Game.peeps[0].curr = 50;
            Game.peeps[1].curr = 50;
            Game.peeps[0].AllIn();
            Assert.AreEqual(1000, Game.peeps[1].curr);
        }
        public void AllInTest4()
        {
            Game.peeps = new List<Player>() { new Player(false, "Bob", 1000), new Player(false, "Jim", 1000), new Player(false, "Sue", 0) };
            foreach (Player to in Game.peeps)
            {
                to.folded = false;
            }
            Game.peeps[0].curr = 50;
            Game.peeps[1].curr = 75;
            Game.peeps[0].AllIn();
            Assert.AreEqual(1025, Game.peeps[1].curr);
            Assert.AreEqual(0, Game.peeps[2].curr);
            Assert.AreEqual(1000, Game.peeps[0].inpot);
        }
    }
}
