using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1_Blackjack
{
    /// <summary>
    /// Public class implementing AI Player and his move decision logic
    /// </summary>
    public class AiPlayer : Player
    {
        /// <summary>
        /// Multiplier of AI difficulty. By this variable AI decide in percent
        /// much of positive cards need to draw next card.
        /// </summary>
        private const int DIFFICULTY_MULTIPLIER = 25;

        /// <summary>
        /// Describing AI difficulty where 3 is Easy 1 is Hard.
        /// </summary>
        private int difficulty = 0;

        /// <summary>
        /// Public custom constructor of class
        /// </summary>
        /// <param name="difficultyLevel">Parameter which setting AI difficulty when creating object</param>
        public AiPlayer(int difficultyLevel)
        {
            difficulty = difficultyLevel;
        }

        /// <summary>
        /// Method which contains logic of AI player to take decision and make move.
        /// </summary>
        /// <returns>Returning status true - Draw card; false - Finish round</returns>
        public override bool MakeMove()
        {
            var fakeDeck = generateFakeDeck();
            
            foreach (var handCard in HandCards)
            {
                fakeDeck.Remove(handCard);
            }

            var availableCard = fakeDeck.Select(x => x.Value).GroupBy(x => x + HandPoints <= 21);
            var availablePositiveCards = availableCard.Where(x => x.Key).FirstOrDefault();
            var percentPossitiveCards = 
                availablePositiveCards == null ? 0 : ((float)availablePositiveCards.Count() / fakeDeck.Count()) * 100;

            return percentPossitiveCards > difficulty * DIFFICULTY_MULTIPLIER;
        }

        /// <summary>
        /// Method which generating fake (temporary) deck without AI player cards.
        /// </summary>
        /// <returns>Collection of cards which does not contains AI player cards</returns>
        private List<Card> generateFakeDeck()
        {
            var fakeDeck = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
            {
                foreach (var figure in Enum.GetValues(typeof(Figure)).Cast<Figure>())
                {
                    fakeDeck.Add(new Card(suit, figure, (int)figure));
                }
            }

            return fakeDeck;
        }
    }
}