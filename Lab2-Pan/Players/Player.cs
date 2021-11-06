using Lab2_Pan.Cards;
using System.Collections.Generic;

namespace Lab2_Pan.Players
{
    /// <summary>
    /// Public abstract class representing implementation of Player
    /// </summary>
    public abstract class Player : IPlayer
    {
        /// <summary>
        /// Property representing cards in hand
        /// </summary>
        public List<ICard> Cards { get; protected set; }

        /// <summary>
        /// Protected constructor to initialize property value
        /// </summary>
        protected Player()
        {
            Cards = new List<ICard>();
        }

        /// <summary>
        /// Method to add for player hand new card
        /// </summary>
        /// <param name="card">Card to add</param>
        public void AddCard(ICard card) => Cards.Add(card);

        /// <summary>
        /// Method to remove from player hand card
        /// </summary>
        /// <param name="card">Card to remove</param>
        public void RemoveCard(ICard card) => Cards.Remove(card);

        /// <summary>
        /// Abstract method to implement operation to make move
        /// </summary>
        /// <param name="playerCards">List of card as player hand</param>
        /// <param name="stackCards">List of card as cards on stack</param>
        /// <param name="availableMoves">List of available moves</param>
        /// <returns></returns>
        public abstract PlayerMove InvokeMove(List<ICard> playerCards, List<ICard> stackCards, List<GameMove> availableMoves);
    }
}
