
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan.Cards
{
    public class DeckService
    {
        public List<ICard> Stack { get; set; }
        public List<ICard> Deck { get; set; }

        public DeckService()
        {
            Deck = new List<ICard>();
        }

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

        public void AddCardToStack(ICard card) => AddCardsToStack(new ICard[] { card });

        public void AddCardsToStack(IEnumerable<ICard> cards)
        {

        }


    }
}
