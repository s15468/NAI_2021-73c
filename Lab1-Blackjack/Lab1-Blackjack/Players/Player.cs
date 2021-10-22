using System.Collections.Generic;

namespace Lab1_Blackjack
{
    /// <summary>
    /// Abstract class which implementing IPlayer interface with properties for child-classes.
    /// </summary>
    public abstract class Player : IPlayer
    {
        /// <summary>
        /// Summary points of each card in hand
        /// </summary>
        public int HandPoints { get; set; } = 0;

        /// <summary>
        /// Boolean variable set to True when player
        /// decide to do not take next card
        /// </summary>
        public bool EndRound { get; set; }

        /// <summary>
        /// List of cards in player hand
        /// </summary>
        public List<Card> HandCards { get; set; } = new List<Card>();

        /// <summary>
        /// Abstract method which is implemented by child classes to make player move.
        /// </summary>
        /// <returns>Returning status true - Draw card; false - Finish round</returns>
        public abstract bool MakeMove();

        /// <summary>
        /// Method which setting HandPoints property for current object.
        /// </summary>
        public void SetHandPoints()
        {
            HandPoints = 0;

            foreach (var card in HandCards)
            {
                HandPoints += (int)card.Figure;
            }
        }
    }
}
