namespace Lab2_Pan.Cards
{
    /// <summary>
    /// Public interface representing Card properties
    /// </summary>
    public interface ICard
    {
        /// <summary>
        /// Property representing card suit
        /// </summary>
        Suit Suit { get;}

        /// <summary>
        /// Property representing card figure
        /// </summary>
        Figure Figure { get; }

        /// <summary>
        /// Property representing card value
        /// </summary>
        int Value { get; }
    }
}
