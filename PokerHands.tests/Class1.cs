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
            char[] seq = new char[] {'6','5','4','3','2' };
            for (int u = 0; u < test1.straightArr.Length; u++)
            {

                Assert.AreEqual(test2.straightArr[u], seq.ToArray()[u]);
            }
        }
    }
}
