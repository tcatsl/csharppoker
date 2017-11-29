﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RealPokerTime.tests
{
    
    [TestFixture]
    public class PlayerTests
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
        [Test]
        public void AllInTest2()
        {
            Game.peeps = new List<Player>() { new Player(false, "Bob", 1000), new Player(false, "Jim", 1025) };
            foreach (Player to in Game.peeps)
            {
                to.folded = false;
            }
            Game.peeps[0].curr = 50;
            Game.peeps[1].curr = 75;
            Game.peeps[0].AllIn();
            Assert.AreEqual(1025, Game.peeps[1].curr);
        }
        [Test]
        public void AllInTest3()
        {
            Game.peeps = new List<Player>() { new Player(false, "Bob", 1000), new Player(false, "Jim", 1000) };
            foreach (Player to in Game.peeps)
            {
                to.folded = false;
            }
            Game.peeps[0].curr = 50;
            Game.peeps[1].curr = 75;
            Game.peeps[0].AllIn();
            Assert.AreEqual(1000, Game.peeps[1].curr);
        }
        [Test]
        public void AllInTest4()
        {
            Game.peeps = new List<Player>() { new Player(false, "Bob", 1000), new Player(false, "Jim", 1000), new Player(false, "Sue",1000) };
            foreach (Player to in Game.peeps)
            {
                to.folded = false;
            }
            Game.peeps[0].curr = 0;
            Game.peeps[1].curr = 900;
            Game.peeps[0].Bet(1000);
            Assert.AreEqual(1000, Game.peeps[1].curr);
            Assert.AreEqual(1000, Game.peeps[2].curr);
            Assert.AreEqual(1000, Game.peeps[0].inpot);
        }
        [Test]
        public void BetTest1()
        {
            for (int f = 0; f < 50; f++)
            {

                Game.peeps = new List<Player>() { new Player(false, "Bob", 10000), new Player(false, "Jim", 3000), new Player(false, "Sue", 0) };
                foreach (Player to in Game.peeps)
                {
                    to.folded = false;
                }
                Game.subround = 4;
                Game.peeps[0].curr = 0;
                Game.peeps[1].curr = 900;
                Game.peeps[0].Bet(0);
                Assert.Greater(Game.peeps[1].curr, 900);
                Assert.AreEqual(0, Game.peeps[2].curr);
                Assert.Greater(Game.peeps[0].inpot, 0);
            }
        }
    }
}
