using System;
using System.Linq;
using System.Collections.Generic;

namespace Lab1_Blackjack
{
    /// <summary>
    /// Rules:
    /// Player which get close 21 points win game
    /// (number of points cant be higher than 21 else player losing game)
    /// 
    /// How-To open game:
    /// 1. Via Debugger in Visual Studio (require .NET 5.0 Framework)
    /// 2. Copy Lab1-Blackjack/bin/Debug/net5.0 folder and start Lab1-Blackjack.exe
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method of game.
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("Blackjack");

            var players = new List<IPlayer>()
            {
                new HumanPlayer(),
                new AiPlayer(new Random().Next(1,3)),
            };

            DeckManager.Generate();
            DeckManager.Shuffle();
            giveFirstCards(players);

            do
            {
                foreach (var player in players)
                {
                    if (player.EndRound)
                    {
                        continue;
                    }

                    var decision = player.MakeMove();

                    if (decision)
                    {
                        drawCardForPlayer(player);
                    }
                    else
                    {
                        player.EndRound = true;
                    }
                }
            }
            while (players.All(x => x.HandPoints <= 21 && !x.EndRound) || !players.Any(x => x.HandPoints > 21));

            Console.WriteLine("Points:");
            Console.WriteLine($"Player: {players[0].HandPoints}");
            Console.WriteLine($"AI: {players[1].HandPoints}");

            if (players.All(x => x.HandPoints <= 21))
            {
                if (players[0].HandPoints > players[1].HandPoints)
                {
                    Console.WriteLine("Player WIN");
                }
                else if (players[0].HandPoints < players[1].HandPoints)
                {
                    Console.WriteLine("AI WIN");
                }
                else
                {
                    Console.WriteLine("DRAW");
                }
            }
            else if (players.All(x => x.HandPoints > 21))
            {
                Console.WriteLine("Both players LOSE");
            }
            else if (players.Where(x => x.HandPoints > 21).Count() == 1)
            {
                var losePlayer = players.Where(x => x.HandPoints > 21).First();
                var type = losePlayer.GetType();

                Console.WriteLine($"{( type == typeof(HumanPlayer) ? "Player" : "AI")} LOSE");
            }

            Console.WriteLine("Press anything to end");
            Console.ReadKey();
        }


        /// <summary>
        /// Method which giving first 2 card for all playing players
        /// </summary>
        /// <param name="players">List of IPlayers which representing players in game</param>
        private static void giveFirstCards(List<IPlayer> players)
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (var player in players)
                {
                    player.HandCards.Add(DeckManager.DrawCard());
                    player.SetHandPoints();
                }
            }
        }

        /// <summary>
        /// Method which draw one card for player and giving him it.
        /// </summary>
        /// <param name="currentPlayer">IPlayer which decide to draw a card.</param>
        private static void drawCardForPlayer(IPlayer currentPlayer)
        {
            currentPlayer.HandCards.Add(DeckManager.DrawCard());
            currentPlayer.SetHandPoints();
        }
    }
}
