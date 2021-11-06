using Lab2_Pan.Cards;
using System.Collections.Generic;

namespace Lab2_Pan.Players
{
    /// <summary>
    /// Public interface of Player
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Property representing cards in hand
        /// </summary>
        List<ICard> Cards { get; }

        /// <summary>
        /// Method representing move 
        /// </summary>
        /// <param name="aiCards">List of player cards</param>
        /// <param name="stackCards">List of stack cards</param>
        /// <param name="availableMoves">List of available moves</param>
        /// <returns></returns>
        PlayerMove InvokeMove(List<ICard> aiCards, List<ICard> stackCards, List<GameMove> availableMoves);

        /// <summary>
        /// Method to add card for player cards
        /// </summary>
        /// <param name="card">Card to add</param>
        void AddCard(ICard card);

        /// <summary>
        /// Method to remove card for player cards
        /// </summary>
        /// <param name="card">Card to add</param>
        void RemoveCard(ICard card);

    }
}
