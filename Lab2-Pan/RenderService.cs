using Lab2_Pan.Cards;
using System;
using System.Collections.Generic;

namespace Lab2_Pan
{
    /// <summary>
    /// Public class to render message for human player
    /// </summary>
    public class RenderService
    {
        /// <summary>
        /// Method to clear console
        /// </summary>
        public void ClearConsole() => Console.Clear();

        /// <summary>
        /// Method to Render cards on stack
        /// </summary>
        /// <param name="stackCards">Cards on stack</param>
        public void RenderStackCards(IEnumerable<ICard> stackCards)
        {
            Console.WriteLine("Cards from oldest to newest:");
            renderCards(stackCards);
        }

        /// <summary>
        /// Method to Render cards in player hand
        /// </summary>
        /// <param name="stackCards">Cards on hand</param>
        public void RenderPlayerCards(IEnumerable<ICard> playerCards)
        {
            Console.WriteLine("Player cards in hand:");
            renderCards(playerCards);
        }

        /// <summary>
        /// Method to render for player all available moves
        /// </summary>
        /// <param name="availableMoves"></param>
        public void RenderAvailableMoves(IEnumerable<GameMove> availableMoves)
        {
            int counter = default;

            Console.WriteLine("Click button with move ID to select:");
            Console.WriteLine("Available moves:");

            foreach (GameMove move in availableMoves)
            {
                Console.WriteLine($"ID {counter} - {move}");
                counter++;
            }
        }
        
        /// <summary>
        /// Method to render all cards from list
        /// </summary>
        /// <param name="cards">List of cards</param>
        private void renderCards(IEnumerable<ICard> cards)
        {
            int counter = default;

            foreach (ICard card in cards)
            {
                Console.WriteLine($"ID {counter} - {card.Figure} - {card.Suit}");
                counter++;
            }
        }
    }
}
