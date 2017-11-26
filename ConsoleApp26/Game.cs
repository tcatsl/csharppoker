using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    public static class Game
    {
        public static string version = "0.951";
        public static int fullamt = 0;
        public static List<string> playercards = new List<string>();
        public static List<Player> peeps = new List<Player> { new Player(false, "knuckles", 2000), new Player(false, "Dante", 2000), new Player(false, "Jeanne", 2000) };
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
            System.Threading.Thread.Sleep(500); Console.WriteLine("$$$$$$$$$$$$new game$$$$$$$$$$$");
            System.Threading.Thread.Sleep(500); Console.WriteLine("Whats your name?");
            namer = Console.ReadLine();
            peeps.Add(new Player(true, namer, 2000));
            foreach (Player qq in peeps)
            {
                pot = 0;
                ante = 50;
                rounds = 0;
                qq.folded = false;
            }
            RoundStart();

        }
        public static void RoundStart()
        {
            Game.fullamt = ante + 30;
            deck = new List<string>();
            foreach (char suite in suits)
            {
                foreach (char numb in PokerHand.customOrder)
                {
                    deck.Add(numb.ToString() + suite.ToString());
                }
            }
            ante = ante + 30;
            foreach (Player playa in peeps)
            {


                playa.inpot = 0;
                playa.curr = 0;
                System.Threading.Thread.Sleep(500); Console.WriteLine(playa.name + ": " + playa.credit);
                if (playa.credit >= ante)
                {
                    playa.folded = false;
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


                peeps.Where(po => po.folded == false).ToList()[peeps.Where(po => po.folded == false).Count() - 1].Ante();
                peeps.Where(po => po.folded == false).ToList()[peeps.Where(po => po.folded == false).Count() - 2].SmallAnte();

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

            System.Threading.Thread.Sleep(500); Console.WriteLine("board cards: " + string.Join(" ", board));
            for (var u = 0; u < 99; u++)
            {
                if ((peeps.Where(mp => mp.folded == true).Count() >= peeps.Count() - 1))
                    break;
                if ((peeps.Where(mp => mp.curr == 0).Count() == peeps.Count()) && turn > plin)
                    break;
                foreach (Player one2 in peeps.Where(per => per.folded == false).ToList())
                {
                    if ((peeps.Where(mp => mp.curr == 0).Count() >= peeps.Count()) && turn > plin)
                        break;
                    if ((peeps.Where(mp => mp.folded == true).Count() >= peeps.Count() - 1))
                        break;
                    if (one2.cards.Count() == 0)
                    {
                        one2.cards = new List<string>();
                        for (var d = 0; d < 2; d++)
                        {

                            int dex = ran2.Next(0, deck.Count());
                            one2.cards.Add(deck[dex < deck.Count() ? dex : 0]);
                            deck.RemoveAt(dex < deck.Count() ? dex : 0);
                        }
                    }


                    System.Threading.Thread.Sleep(500); Console.WriteLine("__________turn_" + turn + "_start_________");

                    if (one2.player == true)
                    {
                        System.Threading.Thread.Sleep(500); Console.WriteLine("your cards: " + string.Join(" ", one2.cards));
                        System.Threading.Thread.Sleep(500); Console.WriteLine("board cards: " + string.Join(" ", board));
                        System.Threading.Thread.Sleep(500); Console.WriteLine("your balance: " + one2.credit);
                        System.Threading.Thread.Sleep(500); Console.WriteLine("players still in: " + string.Join(", ", Game.peeps.Where(peep => peep.folded == false).Select(pl => pl.name).ToArray()));
                    }
                    turn++;

                    one2.Act();

                    System.Threading.Thread.Sleep(500); Console.WriteLine("ending balance: " + one2.credit);
                    System.Threading.Thread.Sleep(500); Console.WriteLine("pot: " + Game.pot);
                    System.Threading.Thread.Sleep(500); Console.WriteLine("___________turn_end____________");
                }
            }
            subround++;

            if (Game.peeps.Where(per => per.folded == false).Count() > 1 && subround < 4)
            {
                System.Threading.Thread.Sleep(500); Console.WriteLine("*****next round of betting*****");
                SubroundStart();
                return;
            }
            else
            {

                System.Threading.Thread.Sleep(500); Console.WriteLine("board cards: " + string.Join(" ", board));
                foreach (Player hy in peeps)
                {
                    if (peeps.Where(go => go.folded == false).Count() > 1 && hy.folded == false)
                    {
                        System.Threading.Thread.Sleep(1500);
                        System.Threading.Thread.Sleep(500); Console.WriteLine(hy.name + " reveals " + String.Join(" ", hy.cards));
                    }
                }
                bool split = false;

                if (Game.peeps.Where(ok => ok.folded == false && ok.inpot != fullamt).Count() > 0)
                {
                    split = true;
                }
                if (split == true)
                {
                    foreach (Player outof in peeps.Where(peep1 => peep1.folded == false && peep1.inpot < fullamt).OrderBy(pl => pl.inpot).ToList())
                    {
                        if (peeps.Where(ip => new PokerHand(string.Join(" ", outof.cards.Concat(Game.board))).CompareWith(new PokerHand(string.Join(" ", ip.cards.Concat(Game.board)))) == Result.Loss).Count() == 0)
                        {
                            outof.credit += peeps.Select(plo => plo.inpot <= outof.inpot ? plo.inpot : outof.inpot).Sum();
                            Game.pot -= peeps.Select(plo => plo.inpot <= outof.inpot ? plo.inpot : outof.inpot).Sum();
                            System.Threading.Thread.Sleep(500); Console.WriteLine(outof.name + " won " + peeps.Select(plo => plo.inpot <= outof.inpot ? plo.inpot : outof.inpot).Sum() + ".");


                            foreach (Player lk in peeps.Where(ml => ml.folded = false && ml != outof))
                            {
                                lk.inpot -= outof.inpot <= lk.inpot ? outof.inpot : lk.inpot;
                            }
                            fullamt -= outof.inpot;
                            outof.folded = true;
                            outof.inpot = 0;
                        }
                    }
                }
                foreach (Player man in peeps)
                {
                    if (man.folded == true)
                    {
                        continue;
                    }
                    Result[] cons = new Result[peeps.Where(ok => ok.folded == false && ok != man).Count()];
                    for (int q = 0; q < peeps.Where(ok => ok.folded == false && ok != man).Count(); q++)
                    {
                        Player opper = peeps.Where(ok => ok.folded == false && ok != man).ToList()[q];
                        cons[q] = new PokerHand(string.Join(" ", man.cards.Concat(Game.board))).CompareWith(new PokerHand(string.Join(" ", opper.cards.Concat(Game.board))));
                    }
                    foreach (Result tuy in cons)
                    {
                        if (tuy.ToString() == Result.Loss.ToString())
                            man.folded = true;
                    }

                }
                foreach (Player won in peeps)
                {
                    if (won.folded == false)
                    {
                        won.credit = won.credit + (Game.pot / peeps.Where(yoo => yoo.folded == false).Count());
                        System.Threading.Thread.Sleep(500); Console.WriteLine(won.name + " won: " + Game.pot / peeps.Where(yoo => yoo.folded == false).Count());
                    }
                }
                Game.pot = 0;

                subround = 0;

                System.Threading.Thread.Sleep(3000);
            }

            if (peeps.Where(lk => lk.credit >= ante).Count() >= 2)
            {
                Player shift = peeps[0];
                peeps.RemoveAt(0);
                peeps.Add(shift);
                System.Threading.Thread.Sleep(500); Console.WriteLine("+++++++++++new round+++++++++++");
                rounds++;
                RoundStart();
            }
            else if (peeps.Where(ok => ok.credit < ante && ok.player == true).Count() > 0)
            {
                System.Threading.Thread.Sleep(500); Console.WriteLine("you lost; new game");
                GameStart();


            }
            else
            {
                System.Threading.Thread.Sleep(500); Console.WriteLine("you won; new game");
                GameStart();
            }
        }
    }
}
