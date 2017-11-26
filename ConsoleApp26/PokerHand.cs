using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    public class PokerHand
    {
        //arrays
        public static char[] customOrder = new char[] { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
        public static char[] customOrder2 = new char[] { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'A' };//card rank
        public string orig; //original hand string fed into constructor
        public int[] binwin; //binary int array indicating what hand(s) you have in descending ranked order, 1 is have, 0 is don't have--see line 74
        public string[] arr; //orig split into a string array
        public char[] suits; // the suits of the cards in hand separate from the card ranks in a char array
        public char[] nums; //cards by rank in hand separated from suit in a char array
        public char[] straightArr;

        //hand booleans
        public bool straight;
        public bool trip;
        public bool pair;
        public bool twopair;
        public bool four;
        public bool full;
        public bool flush;

        //constructor
        public PokerHand(string hand)
        {
            this.orig = hand;
            this.arr = new string[7];
            this.nums = new char[7];
            this.suits = new char[7];
            this.straightArr = new char[5];
            this.straight = true;
        }

        //does what it says
        public static void HandReducer(PokerHand pok)
        {
            //populate arrays
            pok.arr = pok.orig.Split(' ');
            for (int c = 0; c < 7; c++)
            {
                pok.suits[c] = pok.arr[c][1]; //second char
                pok.nums[c] = pok.arr[c][0]; //first char
            }
            //put cards without suit in order by customOrder


            //single line booleans
            pok.flush = pok.suits.Where(suit1 => pok.suits.Where(suit2 => suit1 == suit2).Count() == 5).Count() == 5 || pok.suits.Where(suit3 => pok.suits.Where(suit4 => suit4 == suit3).Count() == 6).Count() == 6 || pok.suits.Where(suit5 => pok.suits.Where(suit6 => suit5 == suit6).Count() == 7).Count() == 7;
            pok.pair = pok.nums.Where(card1 => pok.nums.Where(card2 => card1 == card2).Count() == 2).Count() == 2; ; //single pair
            pok.trip = pok.nums.Where(card1 => pok.nums.Where(card2 => card1 == card2).Count() == 3).Count() == 3; //three of a kind
            pok.four = pok.nums.Where(card1 => pok.nums.Where(card2 => card1 == card2).Count() == 4).Count() == 4; //4 of a kind
            pok.twopair = pok.nums.Where(card1 => pok.nums.Where(card2 => card1 == card2).Count() == 2).Count() == 4 || pok.nums.Where(card1 => pok.nums.Where(card2 => card1 == card2).Count() == 2).Count() == 6; //two pairs
            pok.full = pok.nums.Where(card1 => pok.nums.Where(card2 => card1 == card2).Count() == 2).Count() + pok.nums.Where(card3 => pok.nums.Where(card4 => card3 == card4).Count() == 3).Count() == 5 || pok.nums.Where(card1 => pok.nums.Where(card2 => card1 == card2).Count() == 2).Count() + pok.nums.Where(card3 => pok.nums.Where(card4 => card3 == card4).Count() == 3).Count() == 7; //full house--three of a kind plus a pair

            //straight determination
            pok.nums = !pok.flush ? pok.nums.OrderBy(groupx => Array.IndexOf(customOrder, groupx)).ToArray() : pok.arr.Where(suit1 => pok.arr.Where(suit2 => suit1[1] == suit2[1]).Count() >= 5).Select(card => card[0]).OrderBy(group => Array.IndexOf(customOrder, group)).Take(5).ToArray();

            //card to start at for straight determination
            int strikes = 0;
            int streak = 1;

            int start = customOrder.ToList().IndexOf(pok.nums.Distinct().ToList()[0]);
            int count = pok.nums.Distinct().Count();
            for (int o = 0; o < pok.nums.Distinct().Count() - 1; o++)

            {

                start = customOrder.ToList().IndexOf(pok.nums.Distinct().ToList()[o]);
                if (streak >= 5 || pok.nums.Distinct().Count() < 5)
                    break;

                int ahead = customOrder.ToList().IndexOf(pok.nums.Distinct().ToList()[o + 1]);

                if (start + 1 != ahead)
                {
                    strikes++;
                    streak = 1;
                    continue;
                }
                else
                {
                    pok.straightArr[streak - 1] = pok.nums.Distinct().ToList()[o];
                    pok.straightArr[streak] = pok.nums.Distinct().ToList()[o + 1];

                    streak++;
                    continue;
                }
                //if the cards aren't consecutive
            }
            if (streak == 4 && pok.nums.Distinct().ToList()[0] == 'A' && pok.straightArr[streak - 1] == '2')

            {
                pok.straightArr[4] = 'A';

                streak++;
            }
            if (streak < 5)
            {
                pok.straight = false;

            }
            bool straightflush = pok.straight && pok.flush && pok.straightArr.ToList().OrderBy(group => Array.IndexOf(customOrder, group)).SequenceEqual(pok.arr.Where(suit1 => pok.arr.Where(suit2 => suit1[1] == suit2[1]).Count() >= 5).Select(card => card[0]).OrderBy(group => Array.IndexOf(customOrder, group)).Take(5).ToArray());
            //System.Threading.Thread.Sleep(500); Console.WriteLine(straightflush);
            if (pok.flush == true && pok.straight == false)
            {
                pok.straightArr = pok.arr.Where(suit1 => pok.arr.Where(suit2 => suit1[1] == suit2[1]).Count() >= 5).Select(card => card[0]).OrderBy(group => Array.IndexOf(customOrder, group)).Take(5).ToArray();
            }
            else if (pok.flush == false && pok.straight == false)
            {
                pok.straightArr = pok.nums.ToList().GroupBy(n => n)
                    .Select(group => new
                    {
                        Number = group.Key,
                        Count = group.Count()
                    })
                    .OrderByDescending(group => group.Count)
                    .ThenBy(group => Array.IndexOf(customOrder, group.Number)).SelectMany(group => Enumerable.Repeat(group.Number, group.Count)).Take(5).ToArray();
            }
            //generate binary hand reduction for easy scoring
            //System.Threading.Thread.Sleep(500); Console.WriteLine(pok.straight);
            pok.binwin = new int[] { straightflush ? 1 : 0, pok.four ? 1 : 0, pok.full ? 1 : 0, pok.flush ? 1 : 0, pok.straight ? 1 : 0, pok.trip ? 1 : 0, pok.twopair ? 1 : 0, pok.pair ? 1 : 0, 1 }; //high card is always true

        }

        //compare another hand to your hand. returns the result for the owner of the hand it was called from
        public Result CompareWith(PokerHand hand)
        {
            HandReducer(this);
            HandReducer(hand);
            //iterate across binary hand reduction
            for (int g = 0; g < 9; g++)
            {
                //if you have a hand that ranks higher than theirs
                if (this.binwin[g] > hand.binwin[g])
                {
                    return Result.Win;
                }

                //the opposite
                else if (this.binwin[g] < hand.binwin[g])
                {
                    return Result.Loss;
                }

                //if you have the same hand

                else if (this.binwin[g] == 1 && hand.binwin[g] == 1)
                {
                    for (int z = 0; z < 5; z++)
                    {
                        if (customOrder.ToList().IndexOf(this.straightArr[z]) < customOrder.ToList().IndexOf(hand.straightArr[z]))
                        {
                            return Result.Win;
                        }
                        else if (customOrder.ToList().IndexOf(this.straightArr[z]) > customOrder.ToList().IndexOf(hand.straightArr[z]))
                        {
                            return Result.Loss;
                        }
                    }
                    return Result.Tie;
                }
            }

            //default
            return Result.Tie;
        }
    }

    public enum Result
    {
        Win,
        Loss,
        Tie
    }
}
