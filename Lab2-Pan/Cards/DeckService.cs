using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan.Cards
{
    /// <summary>
    /// Public class using to invoke operation on deck like generate,shuffle etc.
    /// </summary>
    public class DeckService
    {
        /// <summary>
        /// Public property representing Deck as List of Cards.
        /// </summary>
        public List<ICard> Deck { get; set; }

        /// <summary>
        /// Default constructor with initializing class property values.
        /// </summary>
        public DeckService()
        {
            Deck = new List<ICard>();
        }

        /// <summary>
        /// Method to generate every card to game and add to Deck property.
        /// </summary>
        public void GenerateDeck()
        {
            foreach (var suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
            {
                foreach (var figure in Enum.GetValues(typeof(Figure)).Cast<Figure>())
                {
                    Deck.Add(new Card(suit, figure));
                }
            }
        }

        /// <summary>
        /// Method to create temporary deck and return it without using normal deck.
        /// </summary>
        /// <returns>Returning temporary generated List of cards representing default deck</returns>
        public List<ICard> GetTempDeck()
        {
            List<ICard> tempDeck = new();

            foreach (var suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
            {
                foreach (var figure in Enum.GetValues(typeof(Figure)).Cast<Figure>())
                {
                    tempDeck.Add(new Card(suit, figure));
                }
            }

            return tempDeck;
        }

        /// <summary>
        /// Method to shuffle list of cards from Deck property.
        /// </summary>
        public void ShuffleDeck()
        {
            var tempDeck = new List<ICard>();
            var currentDeck = Deck;

            do
            {
                int index = new Random().Next(0, currentDeck.Count);
                tempDeck.Add(currentDeck[index]);
                currentDeck.RemoveAt(index);
            }
            while (currentDeck.Count != 0);

            Deck = tempDeck;
        }
    }
}
