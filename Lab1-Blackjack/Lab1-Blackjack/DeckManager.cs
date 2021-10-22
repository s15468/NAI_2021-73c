using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1_Blackjack
{
    /// <summary>
    /// Class implementing method which allow to manage game card deck.
    /// </summary>
    public static class DeckManager
    {
        /// <summary>
        /// Card collection which is available in game.
        /// </summary>
        public static List<Card> Deck { get; private set; }

        /// <summary>
        /// Default constructor which initialize property value
        /// </summary>
        static DeckManager()
        {
            Deck = new List<Card>();
        }

        /// <summary>
        /// Method which generating whole 52 deck cards.
        /// </summary>
        public static void Generate()
        {
            foreach (var suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
            {
                foreach (var figure in Enum.GetValues(typeof(Figure)).Cast<Figure>())
                {
                    Deck.Add(new Card(suit, figure, (int)figure));
                }
            }
        }

        /// <summary>
        /// Method which shuffle deck with random seed to be more unpredicted.
        /// </summary>
        public static void Shuffle()
        {
            var tempDeck = new List<Card>();
            var currentDeck = Deck;

            do
            {
                var random = new Random().Next(0, currentDeck.Count);
                tempDeck.Add(currentDeck[random]);
                currentDeck.RemoveAt(random);
            }
            while (currentDeck.Count != 0);

            Deck = tempDeck;
        }

        /// <summary>
        /// Method which drawing first card from deck and returning it.
        /// </summary>
        /// <returns>Card as card drawn from deck.</returns>
        public static Card DrawCard()
        {
            var drawnCard = Deck.First();
            Deck.RemoveAt(0);
            return drawnCard;
        }
    }
}
