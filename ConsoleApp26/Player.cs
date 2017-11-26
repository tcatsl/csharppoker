using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    public class Player
    {
        public List<string> cards = new List<string>();
        public int credit;
        public int curr;
        public bool folded;
        public bool player;
        public int inpot;
        public string name;
        public double odds;
        public double tempwins;
        public double iter;
        public double wins;
        public double lossodds;
        public int temploss;
        public int iterloss;
        public int loss;
        Random ran;
        public Player(bool play, string init, int cred)
        {
            this.name = init;
            this.player = play;
            this.credit = cred;
            this.curr = 0;
            this.folded = false;
            this.loss = 0;
            this.temploss = 0;
            this.iterloss = 0;
            this.lossodds = 0;
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
            BetAgain:
            int temp = amt;
            if (temp >= credit)
            {
                temp = credit;
            }

            int maxValue = Game.peeps.Where(opp => opp != this && opp.folded == false && opp.credit > 0).Count() > 0 ? Game.peeps.Where(opp => opp != this && opp.folded == false).Select(unc => unc.credit).Max() : -1;
            int maxIndex = Game.peeps.Select(unc => unc.credit).ToList().IndexOf(maxValue);
            if (maxIndex != -1)
            {
                if (temp >= Game.peeps[maxIndex].credit && temp <= credit - curr)
                {
                    temp = Game.peeps[maxIndex].credit;
                    System.Threading.Thread.Sleep(500); Console.WriteLine(this.name + " puts everyone all in for " + (temp) + ".");
                }
            }
            if (temp >= credit - curr)
            {
                Game.pot += this.credit;
                if (maxIndex != -1)
                {
                    if (!(temp >= Game.peeps[maxIndex].credit && temp <= credit - curr))
                        System.Threading.Thread.Sleep(500); Console.WriteLine(this.name + " goes all in for " + this.credit + ".");
                } else
                {
                    if (!(temp >= Game.peeps[maxIndex].credit && temp <= credit - curr))
                        System.Threading.Thread.Sleep(500); Console.WriteLine(this.name + " goes all in for " + this.credit + ".");
                }
                this.inpot += this.credit;


                foreach (Player dude in Game.peeps)
                {
                    if (dude.folded == false && dude.credit > 0 && dude != this && (this.credit - this.curr) > 0)
                        dude.curr += this.credit - this.curr;
                }
                if (Game.peeps.Where(dude => dude.folded == false && dude.credit > 0 && dude != this).Count() > 0 && (this.credit - this.curr) > 0)
                {
                    Game.fullamt += this.credit - this.curr;

                }
                this.credit = 0;
                goto Donezo;
            }
            Game.pot += this.curr;
            this.inpot += this.curr;
            this.credit -= this.curr;
            int bettin = 0;
            if (this.player == false && Game.ante < (int)(this.credit / (3 - Game.rounds * ((double)(1 / 2)))))
            {
                temp = ran.Next(Game.ante, (int)(this.credit / (3 - Game.rounds * ((double)(1 / 2)))));
            }
            else if (this.player == false && Game.ante <= credit-curr-Game.ante)
            {
                temp = Game.ante;
            }
            else if (this.player == false)
            {
                temp = credit;
                goto BetAgain;
            }
            if (credit <= 0)
            {
                temp = 0;
                bettin = 0;
                curr = 0;
            }
            if (temp != 0)
                bettin = temp;
            if (maxIndex != -1)
            {
                if (temp < Game.peeps[maxIndex].credit && !(temp >= credit))
                {
                    System.Threading.Thread.Sleep(500); Console.WriteLine(this.name + (this.curr > 0 ? (" calls " + this.curr + " and") : "") + " raises " + bettin + ".");
                }
            }
            this.credit -= bettin;
            this.inpot += bettin;
            Game.pot += bettin;
            Game.fullamt += bettin;
            foreach (Player dude in Game.peeps)
            {
                if (dude != this && dude.folded == false && dude.credit > 0)
                    dude.curr += bettin;
            }


            this.curr = 0;
            Donezo:
            this.curr = 0;
        }

        public void Fold()
        {
            System.Threading.Thread.Sleep(500); Console.WriteLine("XXXXXX0XXXXXXXXXXXXXXXXX0XXXXXX");
            System.Threading.Thread.Sleep(500); Console.WriteLine(this.name + " folds.");
            this.folded = true;
            this.curr = 0;
        }
        public void Call()
        {
            System.Threading.Thread.Sleep(500); Console.WriteLine(this.name + " calls " + this.curr + ".");
            Game.pot += this.curr;
            this.credit -= this.curr;
            this.inpot += this.curr;
            this.curr = 0;
        }
        public void Check()
        {
            System.Threading.Thread.Sleep(500); Console.WriteLine(this.name + " checks.");
            this.curr = 0;
        }
        public void AllIn()
        {

            Bet(this.credit);

        }
        public void Ante()
        {

            int bettin = Game.ante;
            System.Threading.Thread.Sleep(500); Console.WriteLine(this.name + " is big blind and antes " + bettin + ".");
            this.credit -= bettin;
            this.inpot += bettin;
            Game.pot += bettin;
            foreach (Player dude in Game.peeps.Where(po=>po.credit >= Game.ante))
            {
                if (dude != this && dude.folded == false)
                    dude.curr = Game.ante - dude.inpot;
            }

            this.curr = 0;
            System.Threading.Thread.Sleep(500); Console.WriteLine("Pot: " + Game.pot);
        }
        public void SmallAnte()
        {

            int bettin = Game.ante / 2;
            System.Threading.Thread.Sleep(500); Console.WriteLine(this.name + " is small blind and antes " + bettin + ".");
            this.credit -= bettin;
            this.inpot += bettin;
            Game.pot += bettin;
            foreach (Player dude in Game.peeps)
            {
                if (dude != this && dude.folded == false)
                    dude.curr = Game.ante - dude.inpot;
            }

            this.curr = Game.ante / 2;
            System.Threading.Thread.Sleep(500); Console.WriteLine("Pot: " + Game.pot);
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
        public void ResetLoss()
        {
            this.loss = 0;
            this.temploss = 0;
            this.lossodds = 0;
            this.iterloss = 0;
        }
        public double Ponder(Result whelp)
        {
            System.Threading.Thread.Sleep(500); Console.WriteLine(this.name + " is thinking.");
            this.Reset();
            if (this.cards.Count() > 0)
            {

                return this.Speculate(whelp);
            }
            else
            {
                return (float)0.33;
            }
        }
        public double Speculate(Result hmm)
        {
            this.tempwins = this.wins;
            if (hmm == Result.Loss)
            {
                if (this.TryLoss())
                {
                   
                    this.wins++;
                }
                this.iter++;
            } else if (hmm == Result.Win)
            {
                if (this.TryWin())
                {
                    this.wins++;
                }
                this.iter++;
            } else
            {
                if (this.TryTie())
                {
                    this.wins++;
                }
                this.iter++;
            }

            return (double)(Math.Abs(((double)(wins / iter) - (double)(tempwins / (iter!= 0? iter -1:1) ))) <= 0.00002 && iter > 20000 ? (double)(wins / (iter)) : this.Speculate(hmm));
        }

        public bool TryTie()
        {
            int trywins = 0;
            List<string> tempcards = new List<string>();
            List<List<string>> oppcards = new List<List<string>>();

            List<string> tempdeck = new List<string>(Game.deck.Concat(Game.peeps.Where(peeper => peeper != this).Select(io => io.cards).SelectMany(i => i)));

            for (int u = 0; u < Game.peeps.Where(mip => mip != this).Count(); u++)
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
            for (int u = 0; u < Game.peeps.Count() - Game.peeps.Where(pl => pl == this || pl.folded == true).Count(); u++)

            {
                List<string> opp = oppcards[u];
                trywins += new PokerHand(string.Join(" ", this.cards.Concat(Game.board).Concat(tempcards))).CompareWith(new PokerHand(string.Join(" ", tempcards.Concat(Game.board).Concat(opp)))) == Result.Tie ? 1 : 0;
            }
            return trywins == Game.peeps.Where(mip => mip.folded == false && mip != this).Count();


        }
        public bool TryWin()
        {
            int trywins = 0;
            List<string> tempcards = new List<string>();
            List<List<string>> oppcards = new List<List<string>>();

            List<string> tempdeck = new List<string>(Game.deck.Concat(Game.peeps.Where(peeper => peeper != this).Select(io => io.cards).SelectMany(i => i)));

            for (int u = 0; u < Game.peeps.Where(mip => mip != this).Count(); u++)
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
            for (int u = 0; u < Game.peeps.Count() - Game.peeps.Where(pl => pl == this || pl.folded == true).Count(); u++)

            {
                List<string> opp = oppcards[u];
                trywins += new PokerHand(string.Join(" ", this.cards.Concat(Game.board).Concat(tempcards))).CompareWith(new PokerHand(string.Join(" ", tempcards.Concat(Game.board).Concat(opp)))) == Result.Win ? 1 : 0;
            }
            return trywins == Game.peeps.Where(mip => mip.folded == false && mip != this).Count();


        }
        public bool TryLoss()
        {
            int tryloss = 0;
            List<string> tempcards = new List<string>();
            List<List<string>> oppcards = new List<List<string>>();

            List<string> tempdeck = new List<string>(Game.deck.Concat(Game.peeps.Where(peeper => peeper != this).Select(io => io.cards).SelectMany(i => i)));

            for (int u = 0; u < Game.peeps.Where(mip => mip != this).Count(); u++)
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
            for (int u = 0; u < Game.peeps.Count() - Game.peeps.Where(pl => pl == this || pl.folded == true).Count(); u++)

            {
                List<string> opp = oppcards[u];
                tryloss += new PokerHand(string.Join(" ", this.cards.Concat(Game.board).Concat(tempcards))).CompareWith(new PokerHand(string.Join(" ", tempcards.Concat(Game.board).Concat(opp)))) == Result.Loss ? 1 : 0;
            }
            return tryloss > 0;


        }
        public void AI()
        {
            this.odds = this.Ponder(Result.Win);
            if (Game.peeps.Where(loc => loc.folded == false && loc != this && loc.credit > 0).Count() < 1 && this.curr == 0)
            {
                this.Check();
                return;
            }

            if (this.credit <= 0)
            {
                this.Check();
                return;
            }
            if (this.curr == 0)
            {
                if (this.ran.Next(1, 21) > 15 || odds >= 0.5109 && this.ran.Next(0, 22) > 10)
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

                int res = this.ran.Next(0, 21);
                if (res == 20 && this.odds > 0.27)
                {
                    this.AllIn();
                    return;
                }
                if (res > 15 || (this.odds >= 0.5509) && res > 8)
                {
                    this.Bet(0);
                    return;
                }
                if (res >= 8 || (this.odds >= 0.5 && res > 4))
                {
                    this.Call();
                    return;
                }
                this.Fold();
                return;
            }
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
        public void NotAI()
        {
            this.odds = this.Ponder(Result.Win);
            System.Threading.Thread.Sleep(500); Console.WriteLine((this.odds * 100) + "% chance of winning");

            //this.odds = this.Ponder(Result.Loss);
            //System.Threading.Thread.Sleep(500); Console.WriteLine( (this.odds * 100) + "% chance of losing");
            //this.odds = this.Ponder(Result.Tie);
            //System.Threading.Thread.Sleep(500); Console.WriteLine((this.odds * 100) + "% chance of tie");

            if (this.folded == true)
                return;
            if (this.credit <= 0 || Game.peeps.Where(ok => ok.credit > 0 && !ok.folded && ok != this).Count() < 1)
            {
                this.Check();
                return;
            }
            if (this.curr == 0)
            {
                System.Threading.Thread.Sleep(500); Console.WriteLine("pot: " + Game.pot);
                System.Threading.Thread.Sleep(500); Console.WriteLine("current bet: " + this.curr);
                System.Threading.Thread.Sleep(500); Console.WriteLine("bet, or check?");
                string act = Console.ReadLine();

                if (act == "bet")
                {
                    int c;

                    Rebet:
                    {
                        System.Threading.Thread.Sleep(500); Console.WriteLine("bet how much?");
                        string inp = Console.ReadLine();
                        int y = 0;
                        bool succ = Int32.TryParse(inp, out y);
                        if (succ)
                        {


                            c = Int32.Parse(inp);
                            if (c >= Game.ante && c <= this.credit)
                            {
                                this.Bet(c);
                                return;
                            }
                            else
                            {
                                System.Threading.Thread.Sleep(500); Console.WriteLine("minimum bet is " + Game.ante + ". maximum is " + (this.credit - this.curr) + ".");
                                this.NotAI();
                            }
                        }
                        else
                        {
                            goto Rebet;
                        }
                    }
                }
                else if (act == "check")
                    this.Check();
                else
                {
                    System.Threading.Thread.Sleep(500); Console.WriteLine("not a valid option.");
                    this.NotAI();
                }
            }
            else if (this.curr <= this.credit)
            {
                System.Threading.Thread.Sleep(500); Console.WriteLine("pot: " + Game.pot);
                System.Threading.Thread.Sleep(500); Console.WriteLine("current bet: " + this.curr);
                System.Threading.Thread.Sleep(500); Console.WriteLine("bet, fold, or call?");
                string act = Console.ReadLine();
                if (act == "bet")
                {
                    int c;

                    Rebet:
                    {
                        System.Threading.Thread.Sleep(500); Console.WriteLine("bet how much?");
                        string inp = Console.ReadLine();
                        int y = 0;
                        bool succ = Int32.TryParse(inp, out y);
                        if (succ)
                        {


                            c = Int32.Parse(inp);
                            if (c >= Game.ante && c <= this.credit)
                            {
                                this.Bet(c);
                                return;
                            }
                            else
                            {
                                System.Threading.Thread.Sleep(500); Console.WriteLine("minimum bet is " + Game.ante + ". maximum is " + (this.credit - this.curr) + ".");
                                this.NotAI();
                            }
                        }
                        else
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
                    System.Threading.Thread.Sleep(500); Console.WriteLine("not a valid option");
                    this.NotAI();
                }
            }
            else

            {
                System.Threading.Thread.Sleep(500); Console.WriteLine("pot: " + Game.pot);
                System.Threading.Thread.Sleep(500); Console.WriteLine("current bet: " + this.curr);
                System.Threading.Thread.Sleep(500); Console.WriteLine("fold or all in?");
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
                    System.Threading.Thread.Sleep(500); Console.WriteLine("not a valid option.");
                    this.NotAI();
                }
            }
        }
    }
}
