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
            PokerHand test1 = new PokerHand("4C 4D 6D 2C JS 3D AH");
            //their hand
            PokerHand test2 = new PokerHand("AS 2S 3S 4S 5D 2D TH");
            //you win. high card.Console.WriteLine(test1.CompareWith(test2).ToString() + test1.flush + test2.flush + string.Join(",", test1.nums));
            Game.GameStart();

        }
    }
    public class Npc
    {
        public List<string> cards = new List<string>();
        public int credit;
        public int curr;
        public bool folded;
        public bool player;
        public int inpot;
        public string name;
        public float odds;
        public float tempwins;
        public float iter;
        public float wins;
        Random ran;
        public Npc(bool play, string init)
        {
            this.name = init;
            this.player = play;
            this.credit = 2000;
            this.curr = 0;
            this.folded = false;
            this.inpot = 0;
            this.wins = 0;
            this.iter = 0;
            this.odds = 0;
            this.tempwins = 0;
            this.odds = 0;
            ran = new Random();
            
        }
        public void Bet(int amt)
        {


            int temp = amt;
            if (temp > credit + curr)
            {
                temp = credit;
            }
            if (true == true)
            {
                int maxValue = Game.peeps.Where(opp => opp != this && opp.folded == false && opp.credit > 0).Count() > 0 ? Game.peeps.Where(opp => opp != this && opp.folded == false).Select(unc => unc.credit).Max() : -1;
                int maxIndex = Game.peeps.Where(opp => opp != this && opp.folded == false).Select(unc => unc.credit).ToList().IndexOf(maxValue);
                if (maxIndex != -1)
                {
                    if (temp >= Game.peeps[maxIndex].credit)
                    {
                        temp = Game.peeps[maxIndex].credit;
                        Console.WriteLine(this.name + " puts everyone all in for " + temp );
                    }
                }
                if (temp == credit)
                {
                    Game.pot += this.credit;

                    Console.WriteLine(this.name + " goes all in for " + this.credit);
                    this.inpot += this.credit;


                    foreach (Npc dude in Game.peeps)
                    {
                        if (dude.folded == false && dude.credit > 0 && dude != this && (this.credit - this.curr) > 0)
                            dude.curr += this.credit - this.curr;
                    }
                    this.credit = 0;
                    goto Donezo;
                }
                Game.pot += this.curr;
                this.inpot += this.curr;
                this.credit -= this.curr;
                int bettin = ran.Next(1, (int)  (this.credit/(3-Game.rounds * ((double)(1/2)))));
                if (credit <= 0)
                {
                    bettin = 0;
                    curr = 0;
                }
                if (temp != 0)
                    bettin = temp;
                if (temp < Game.peeps[maxIndex].credit)
                {
                    Console.WriteLine(this.name + " calls " + this.curr + " and raises " + bettin);
                }
                this.credit -= bettin;
                this.inpot += bettin;
                Game.pot += bettin;
                Game.fullamt += bettin;
                foreach (Npc dude in Game.peeps)
                {
                    if (dude != this && dude.folded == false && dude.credit > 0)
                        dude.curr += bettin;
                }

            }
            Donezo:
            this.curr = 0;
        }

        public void Fold()
        {
            Console.WriteLine("XXXXXX0XXXXXXXXXXXXXXXXX0XXXXXX");
            Console.WriteLine(this.name +" folds" );
            this.folded = true;
            this.curr = 0;
            Console.WriteLine("Pot: " + Game.pot);
        }
        public void Call()
        {
            Console.WriteLine(this.name + " calls " + this.curr);
            Game.pot += this.curr;
            this.credit -= this.curr;
            this.inpot += this.curr;
            this.curr = 0;
            Console.WriteLine("Pot: " + Game.pot);
        }
        public void Check()
        {
            Console.WriteLine(this.name + " checks");
            Console.WriteLine("Pot: " + Game.pot);
            this.curr = 0;
        }
        public void AllIn()
        {

            Bet(this.credit);

            Console.WriteLine("Pot: " + Game.pot);
        }
        public void Ante()
        {

            int bettin = Game.ante;
            Console.WriteLine(this.name + " is big blind and antes: " + bettin);
            this.credit -= bettin;
            this.inpot += bettin;
            Game.pot += bettin;
            foreach (Npc dude in Game.peeps)
            {
                if (dude != this && dude.folded == false)
                    dude.curr = Game.ante - dude.inpot;
            }

            this.curr = 0;
            Console.WriteLine("Pot: " + Game.pot);
        }
        public void SmallAnte()
        {

            int bettin = Game.ante / 2;
            Console.WriteLine(this.name + " is small blind and antes: " + bettin);
            this.credit -= bettin;
            this.inpot += bettin;
            Game.pot += bettin;
            foreach (Npc dude in Game.peeps)
            {
                if (dude != this && dude.folded == false)
                    dude.curr = Game.ante - dude.inpot;
            }

            this.curr = Game.ante / 2;
            Console.WriteLine("Pot: " + Game.pot);
        }
        public void Win()
        {
            this.credit += Game.pot;
            Game.pot = 0;
        }
        public void Act()
        {
            if (this.player == false)
            {
                this.AI();
            }
            else if (this.player == true)
            {
                this.NotAI();
            }
        }
        public void Reset()
        {
            this.wins = 0;
            this.tempwins = 0;
            this.odds = 0;
            this.iter = 0;
        }
        public float Ponder()
        {
            Console.WriteLine(this.name + " is thinking");
            this.Reset();
            if (this.cards.Count() > 0)
            {
              
                return this.Speculate();
            } else
            {
                return (float) 0.33;
            }
        }
        public float Speculate()
        {
            this.tempwins = this.wins;
            if (this.TryWin())
            {
                this.wins++;
            }
            this.iter++;

            return (float) (Math.Abs(((double)tempwins / (iter + 1) - (double)wins / iter)) < 0.00005 && iter > 100 ? (double) (wins / iter) : this.Speculate());
            }
        public bool TryWin()
        {
            int trywins = 0;
                List<string> tempcards = new List<string>();
                List<List<string>> oppcards = new List<List<string>>();

                List<string> tempdeck = new List<string>(Game.deck.Concat((Game.peeps.Where(pepe => pepe.cards.Count() > 0 && pepe == this).Count() > 1) ? Game.peeps.Where(pepe => pepe.cards.Count() > 0 && pepe == this).ToList()[0].cards.Concat(Game.peeps.Where(pepe => pepe.cards.Count() > 0 && pepe == this).ToList()[1].cards) : Game.peeps.Where(pepe => pepe.cards.Count() > 0 && pepe == this).ToList()[0].cards));

                for (int u = 0; u < Game.peeps.Where(mip => mip.folded == false && mip != this).Count(); u++)
                {
                    List<string> tempc = new List<string>();
                    for (int im = 0; im < 2; im++)
                    {
                        int dex2 = this.ran.Next(0, tempdeck.Count());
                        tempc.Add(tempdeck[dex2]);
                        tempdeck.RemoveAt(dex2);
                    }
                    oppcards.Add(tempc);
                }
                for (var f = 0; f < 5 - Game.board.Count(); f++)
                {
                    int dex = this.ran.Next(0, tempdeck.Count());
                    tempcards.Add(tempdeck[dex]);
                    tempdeck.RemoveAt(dex);
                }
                foreach(List<string> opp in oppcards)
            {
                trywins += new PokerHand(string.Join(" ", this.cards.Concat(Game.board).Concat(tempcards))).CompareWith(new PokerHand(string.Join(" ", tempcards.Concat(Game.board).Concat(opp)))) == Result.Win ? 1 : 0;
            }
            return trywins == Game.peeps.Where(mip => mip.folded == false && mip != this).Count();


        }
        public void AI()
        {
            this.odds = this.Ponder();
            if (this.credit <= 0)
            {
                this.Check();
                return;
            }
            if (this.curr == 0)
            {
                if (this.ran.Next(1, 21) > 16 || odds >= 0.5109 && this.ran.Next(0, 22) > 10)
                {
                    this.Bet(0);
                    return;
                }
                else
                {
                    this.Check();
                    return;
                }
            }
            if (this.curr < this.credit)
            {
                
                int res = this.ran.Next(1, 20);
                if (res > 17 || (this.odds >= 0.5509) && res > 8)
                {
                    this.Bet(0);
                    return;
                }
                if (res >= 12 || (this.odds >= 0.5 && res > 5))
                {
                    this.Call();
                    return;
                }
                    this.Fold();
                    return;
            }
            else
            {
                
                if (this.ran.Next(1, 20) > 15 || this.odds >= 0.6 || this.credit < Game.ante + 30)
                {
                    this.AllIn();
                    return;
                }
                else
                {
                    this.Fold();
                    return;
                }
            }
        }
        public void NotAI()
        {
            this.odds = this.Ponder();
            Console.WriteLine((this.cards.Count() > 0 ? (int)(this.odds  * 100) : 33) + "% chance of winning");
            if (this.folded == true || Game.peeps.Where(ui=> ui.folded == true).Count() > Game.peeps.Count() - 2)
                return;
            Console.WriteLine("Balance: " + this.credit);
            if (Game.peeps.Where(peepa=>peepa.credit > 0 && peepa.folded == false && peepa.player == false).Count() < 1 || this.credit <= 0)
            {
                this.Check();
                return;
            }
            if (this.curr == 0)
            {
                Console.WriteLine("Pot: " + Game.pot);
                Console.WriteLine("Current Bet: " + this.curr);
                Console.WriteLine("bet, or check?");
                string act = Console.ReadLine();

                if (act == "bet")
                {
                    int c;
                    Console.WriteLine("Bet how much?");
                    c = Int32.Parse(Console.ReadLine());
                    if (c > 0 && c <= this.credit)
                    {
                        this.Bet(c);
                        return;
                    }else
                    {
                        Console.WriteLine("not enough funds");
                        this.NotAI();

                    }
                }
                else if (act == "check")
                    this.Check();
                else
                {
                    Console.WriteLine("not a valid option");
                    this.NotAI();
                }
            }
            else if (this.curr <= this.credit)
            {
                Console.WriteLine("Pot: " + Game.pot);
                Console.WriteLine("Current Bet: " + this.curr);
                Console.WriteLine("bet, fold, or call?");
                string act = Console.ReadLine();
                if (act == "bet")
                {
                    int c;

                    Rebet:
                    {
                        Console.WriteLine("Bet how much?");
                        string inp = Console.ReadLine();
                        int y = 0;
                        bool succ = Int32.TryParse(inp, out y);
                        if (succ)
                        {


                            c = Int32.Parse(inp);
                            if (c > 0 && c <= this.credit)
                            {
                                this.Bet(c);
                                return;
                            } else
                            {
                                Console.WriteLine("not a enough funds");
                                this.NotAI();
                            }
                        } else
                        {
                            goto Rebet;
                        }
                    }
                }
                else if (act == "call")
                    this.Call();
                else if (act == "fold")
                    this.Fold();
                else
                {
                    Console.WriteLine("not a valid option");
                    this.NotAI();
                }
            }
            else

            {
                Console.WriteLine("Pot: " + Game.pot);
                Console.WriteLine("Current Bet: " + this.curr);
                Console.WriteLine("fold or all in?");
                string act = Console.ReadLine();
                if (act == "fold")
                {
                    this.Fold();
                }
                else if (act == "all in")
                {
                    this.AllIn();
                }
                else
                {
                    Console.WriteLine("not a valid option");
                    this.NotAI();
                }
            }
        }
    }
    public enum Result
    {
        Win,
        Loss,
        Tie
    }
    public static class Game
    {
        public static int fullamt = 0;
        public static List<string> playercards = new List<string>();
        public static List<Npc> peeps = new List<Npc> { new Npc(false, "knuckles"), new Npc(false, "Dante"), new Npc(false, "Jeanne") };
        public static Random ran2 = new Random();
        public static int pot = 0;
        public static int ante = 50;
        public static int rounds = 0;
        public static int turn;
        public static int subround = 0;
        public static List<string> board = new List<string>();
        public static char[] suits = new char[] { '\u2660', '\u2665', '\u2666', '\u2663' };
        public static List<string> deck = new List<string>();
        public static string namer;
        public static void GameStart()
        {
            Console.WriteLine("$$$$$$$$$$$$new game$$$$$$$$$$$");
            Console.WriteLine("Whats your name?");
            namer = Console.ReadLine();
            peeps.Add(new Npc(true, namer));
            foreach (Npc qq in peeps)
            {
                qq.credit = 2000;
                pot = 0;
                ante = 50;
                rounds = 0;
                qq.folded = false;
            }
            RoundStart();

        }
        public static void RoundStart()
        {
            deck = new List<string>();
            foreach (char suite in suits)
            {
                foreach (char numb in PokerHand.customOrder)
                {
                    deck.Add(numb.ToString() + suite.ToString());
                }
            }
            ante = ante + 30;
            foreach (Npc playa in peeps)
            {

                Console.WriteLine(playa.name + ": " + playa.credit);
                if (playa.credit >= ante)
                {
                    playa.curr = 0;
                    playa.folded = false;
                    playa.inpot = 0;
                    playa.cards = new List<string>();
                }
            }
            board = new List<string>();
            subround = 0;
            SubroundStart();

        }
        public static void SubroundStart()
        {
            int plin = Game.peeps.Where(per => per.folded == false).Count();
            turn = 1;
            if (subround == 0)
            {
                
                
                    peeps[peeps.Count()-1].Ante();
                    peeps[peeps.Count() -2 ].SmallAnte();

            }
            if (subround == 1)
            {

                for (int u = 0; u < 3; u++)
                {
                    int dex = ran2.Next(0, deck.Count());
                    board.Add(deck[dex < deck.Count() ? dex : 0]);
                    deck.RemoveAt(dex < deck.Count() ? dex : 0);
                }


            }
            if (subround == 2)
            {
                int dex = ran2.Next(0, deck.Count());
                board.Add(deck[dex < deck.Count() ? dex : 0]);
                deck.RemoveAt(dex < deck.Count() ? dex : 0);

            }
            if (subround == 3)
            {
                int dex = ran2.Next(0, deck.Count());
                board.Add(deck[dex < deck.Count() ? dex : 0]);
                deck.RemoveAt(dex < deck.Count() ? dex : 0);
            }

            for (var u = 0; u < 99; u++)
            {
                
                if ((peeps.Where(mp => mp.curr == 0).Count() == peeps.Count()) && turn > plin)
                    break;
                foreach (Npc one2 in peeps.Where(per => per.folded == false).ToList())
                {
                    if ((peeps.Where(mp => mp.curr == 0).Count() >= peeps.Count()) && turn > plin)
                        break;
                    
                    if (one2.cards.Count() == 0)
                    {
                        one2.cards = new List<string>();
                        for (var d = 0; d < 2; d++)
                        {

                            int dex = ran2.Next(0, deck.Count());
                            one2.cards.Add(deck[dex < deck.Count()  ? dex : 0]);
                            deck.RemoveAt(dex < deck.Count() ? dex : 0);
                        }
                    }
                    

                    Console.WriteLine("__________turn_"+ turn +"_start_________");
                    if (one2.player == true)
                    {
                        Console.WriteLine("your cards: " + string.Join(" ", one2.cards));

                        Console.WriteLine("board cards: " + string.Join(" ", board));
                    }
                    turn++;
                    one2.Act();

                    Console.WriteLine("___________turn_end____________");
                }
            }
            subround++;

            if (Game.peeps.Where(per => per.folded == false).Count() > 1 && subround < 4)
            {
                Console.WriteLine("*****next round of betting*****");
                SubroundStart();
                return;
            }
            else
            {

                Console.WriteLine("board cards: " + string.Join(" ", board));
                foreach (Npc hy in peeps)
                {
                    if (peeps.Where(go => go.folded == false).Count() > 1 && hy.folded == false)
                    {
                        System.Threading.Thread.Sleep(1500);
                        Console.WriteLine(hy.name + " reveals " + String.Join(" ", hy.cards));
                    }
                }
                bool split = false;
                if (peeps.Where(opp => opp.folded == false).Select(unc => unc.inpot).Max() != peeps.Where(opp => opp.folded == false).Select(unc => unc.inpot).Min())
                {
                    split = true;
                }
                if (split == true)
                { foreach (Npc outof in peeps.Where(peep1 => peep1.folded == false && peep1.inpot != fullamt).ToList())
                    {
                        if (peeps.Where(ip => new PokerHand(string.Join(" ", outof.cards.Concat(Game.board))).CompareWith(new PokerHand(string.Join(" ", ip.cards.Concat(Game.board)))) == Result.Win).Count() > 1)
                        {
                            outof.credit += outof.inpot * peeps.Where(peep1 => peep1.folded == false && peep1.inpot != fullamt).Count();
                            Game.pot -= outof.inpot * 3;
                            outof.folded = true;
                        }
                    }
                }
                    foreach (Npc man in peeps)
                    {
                        if (man.folded == true || peeps.Where(poo => poo.folded == true).Count() > peeps.Count() -2)
                        {
                            continue;
                        }
                        Result[] cons = new Result[peeps.Where(ok=>ok.folded == false && ok != man).Count()];
                    for (int q = 0; q < peeps.Where(ok => ok.folded == false && ok != man).Count(); q++)
                    {
                        Npc opper = peeps.Where(ok => ok.folded == false && ok != man).ToList()[q];
                        cons[q] = new PokerHand(string.Join(" ", man.cards.Concat(Game.board))).CompareWith(new PokerHand(string.Join(" ", opper.cards.Concat(Game.board))));
                    }
                        foreach (Result tuy in cons)
                        {
                            if (tuy.ToString() == Result.Loss.ToString())
                                man.folded = true;
                        }

                    }
                System.Threading.Thread.Sleep(3000);
                    foreach (Npc won in peeps)
                    {
                        if (won.folded == false)
                        {
                            won.credit = won.credit + (Game.pot / peeps.Where(yoo => yoo.folded == false).Count());
                            Console.WriteLine(won.name + " won: " + Game.pot / peeps.Where(yoo => yoo.folded == false).Count());
                        }
                    }
                Game.pot = 0;

                subround = 0;
                
            }

            if (peeps.Where(lk => lk.credit < ante).Count() <= 1)
            {
                Npc shift = peeps[0];
                peeps.RemoveAt(0);
                peeps.Add(shift);
                Console.WriteLine("+++++++++++new round+++++++++++");
                rounds++;
                RoundStart();
            }
            else if (peeps.Where(ok => ok.credit > ante).ToList()[0].player == true)
            {


                Console.WriteLine("you won; new game");
                GameStart();
            }
            else
            {
                Console.WriteLine("you lost; new game");
                GameStart();
            }
        }
    }
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
            pok.nums = pok.nums.OrderBy(groupx => Array.IndexOf(customOrder, groupx)).ToArray();

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
                    int ahead = customOrder.ToList().IndexOf(pok.nums.Distinct().ToList()[o+1]);
                    
                    if (start + 1 != ahead)
                    {
                        strikes++;
                        streak = 1;
                        continue;
                    }
                    else
                    {
                    pok.straightArr[streak-1] = pok.nums.Distinct().ToList()[o];
                        pok.straightArr[streak] = pok.nums.Distinct().ToList()[o+1];

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
                    pok.nums = pok.nums.ToList().GroupBy(n => n)
                    .Select(group => new
                    {
                        Number = group.Key,
                        Count = group.Count()
                    })
                    .OrderByDescending(group => group.Count)
                    .ThenBy(group => Array.IndexOf(customOrder, group.Number)).SelectMany(group => Enumerable.Repeat(group.Number, group.Count)).ToArray();
                
            //generate binary hand reduction for easy scoring
            //Console.WriteLine(pok.straight);
                pok.binwin = new int[] { pok.straight && pok.flush ? 1 : 0, pok.four ? 1 : 0, pok.full ? 1 : 0, pok.flush ? 1 : 0, pok.straight ? 1 : 0, pok.trip ? 1 : 0, pok.twopair ? 1 : 0, pok.pair ? 1 : 0, 1 }; //high card is always true
        }

            //compare another hand to your hand. returns the result for the owner of the hand it was called from
            public Result CompareWith(PokerHand hand)
            {
            HandReducer(this);
            HandReducer(hand);
                //iterate across binary hand reduction
                for (int g =0; g < 9; g++)
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
                    else if (this.binwin[g] == 1 && hand.binwin[g] == 1 && this.straight == false)
                    {
                        //high card determination
                        for (int z = 0; z < 5; z++)
                        {
                            if (customOrder.ToList().IndexOf(this.nums[z]) < customOrder.ToList().IndexOf(hand.nums[z]))
                            {
                                return Result.Win;
                            }
                            else if (customOrder.ToList().IndexOf(this.nums[z]) > customOrder.ToList().IndexOf(hand.nums[z]))
                            {
                                return Result.Loss;
                            }
                        }
                        return Result.Tie;
                    }
                    else if (this.binwin[g] == 1 && hand.binwin[g] == 1 && this.straight == true)
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
    }
