using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RealPokerTime.tests
{
    [TestFixture]
    public class HandTests
    {
        [Test]
        public void StraightFlushTest1()
        {
            PokerHand test1 = new PokerHand("AD KD QD JD TD 9D 3D");
            //their hand
            PokerHand test2 = new PokerHand("AS 2S 3S 4S 5S 6S 7D");
            PokerHand.HandReducer(test1);
            PokerHand.HandReducer(test2);
            char[] seq = new char[] { '6', '5', '4', '3', '2' };
            for (int u = 0; u < test2.straightArr.Length; u++)
            {

                Assert.AreEqual(test2.straightArr[u], seq.ToArray()[u]);
            }
            
        }
        [Test]
        public void TwoPairTest1()
        {
            PokerHand test1 = new PokerHand("AD AS QD QS TD TS JD");
            PokerHand.HandReducer(test1);
            char[] seq = new char[] { 'A', 'A', 'Q', 'Q', 'J' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
            }

        }
        [Test]
        public void TwoPairTest2()
        {
            PokerHand test1 = new PokerHand("AD AS QD QS JD JS TD");
            PokerHand.HandReducer(test1);
            char[] seq = new char[] { 'A', 'A', 'Q', 'Q', 'J' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
            }

        }
        [Test]
        public void TwoPairTest3()
        {
            PokerHand test1 = new PokerHand("AD AS QD QS JD 6S TD");
            PokerHand.HandReducer(test1);
            char[] seq = new char[] { 'A', 'A', 'Q', 'Q', 'J' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
            }

        }
    }
    
}
