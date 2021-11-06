namespace Lab2_Pan.Cards
{
    /// <summary>
    /// Public class representing simple Card as object
    /// </summary>
    public sealed class Card : ICard
    {
        /// <summary>
        /// Card suit e.g. Heart
        /// </summary>
        public Suit Suit { get; private set; }
        /// <summary>
        /// Card figure e.g. Queen
        /// </summary>
        public Figure Figure { get; private set; }
        /// <summary>
        /// Card value for verification where higher card
        /// has bigger value e.g. Jack is 3 when Queen is 4
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Custom constructor to create object with specific parameters
        /// </summary>
        /// <param name="suit">Expected card suit</param>
        /// <param name="figure">Expected card figure</param>
        public Card(Suit suit, Figure figure)
        {
            Suit = suit;
            Figure = figure;
            Value = (int)figure;
        }
    }
}
