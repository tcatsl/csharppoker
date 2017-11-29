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
            PokerHand test2 = new PokerHand("AS 2S 3S 4S 5S 6D 7D");
            PokerHand.HandReducer(test2);
            char[] seq = new char[] { '5', '4', '3', '2', 'A' };
            for (int u = 0; u < test2.straightArr.Length; u++)
            {

                Assert.AreEqual(test2.straightArr[u], seq.ToArray()[u]);
            }
            
        }
        [Test]
        public void StraightFlushTest2()
        {
            PokerHand test1 = new PokerHand("AD KD QD JD TD 9D 3D");
            PokerHand.HandReducer(test1);
            char[] seq = new char[] { 'A', 'K', 'Q', 'J', 'T' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
            }

        }
        [Test]
        public void StraightFlushTest3()
        {
            PokerHand test2 = new PokerHand("AS 2S 3S 4S 5S 6S 7D");
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
            PokerHand test1 = new PokerHand("AD AS QD QS 6D JS TD");
            PokerHand.HandReducer(test1);
            char[] seq = new char[] { 'A', 'A', 'Q', 'Q', 'J' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
            }

        }
        [Test]
        public void PairTest1()
        {
            PokerHand test1 = new PokerHand("AD TS QD JS 6D 7S AS");
            PokerHand.HandReducer(test1);
            char[] seq = new char[] { 'A', 'A', 'Q', 'J', 'T' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
            }

        }
        [Test]
        public void PairTest2()
        {
            PokerHand test1 = new PokerHand("AD TS QD JS 6D 2H 2S");
            PokerHand.HandReducer(test1);
            char[] seq = new char[] { '2', '2', 'A', 'Q', 'J' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
            }

        }
        [Test]
        public void FullTest1()
        {
            PokerHand test1 = new PokerHand("AD AS AH 2S 2D JS JD");
            PokerHand.HandReducer(test1);
            char[] seq = new char[] { 'A', 'A', 'A', 'J', 'J' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
            }

        }
        [Test]
        public void FullTest2()
        {
            PokerHand test1 = new PokerHand("AD TS AH TD 2D 2S AS");
            PokerHand.HandReducer(test1);
            char[] seq = new char[] { 'A', 'A', 'A', 'T', 'T' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
            }

        }
        [Test]
        public void FullTest3()
        {
            PokerHand test1 = new PokerHand("TD TS 6D 2S 6D 2H 6S");
            PokerHand.HandReducer(test1);
            char[] seq = new char[] { '6', '6', '6', 'T', 'T' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test1.straightArr[u], seq.ToArray()[u]);
            }

        }
    }
    
}
