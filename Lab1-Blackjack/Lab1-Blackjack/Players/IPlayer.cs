using System.Collections.Generic;

namespace Lab1_Blackjack
{
    /// <summary>
    /// IPlayer interface with properties for child-classes for player implementation.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Boolean variable set to True when player
        /// decide to do not take next card
        /// </summary>
        bool EndRound { get; set; }

        /// <summary>
        /// Summary points of each card in hand
        /// </summary>
        int HandPoints { get; set; }

        /// <summary>
        /// List of cards in player hand
        /// </summary>
        public List<Card> HandCards { get; set; }

        /// <summary>
        /// Abstract method which is implemented by child classes to make player move.
        /// </summary>
        /// <returns>Returning status true - Draw card; false - Finish round</returns>
        bool MakeMove();

        /// <summary>
        /// Method which setting HandPoints property for current object.
        /// </summary>
        void SetHandPoints();
    }
}
