namespace Lab1_Blackjack
{
    public class Card
    {
        /// <summary>
        /// Property representing card suit
        /// </summary>
        public Suit Suit { get; private set; }

        /// <summary>
        /// Property representing card figure
        /// </summary>
        public Figure Figure { get; private set; }

        /// <summary>
        /// Property representing card value in game
        /// </summary>
        public int Value { get; private set; }


        /// <summary>
        /// Custom class constructor
        /// </summary>
        /// <param name="suit">Expected card suit</param>
        /// <param name="figure">Expected card figure</param>
        /// <param name="value">Expected card value</param>
        public Card(Suit suit, Figure figure, int value)
        {
            Suit = suit;
            Figure = figure;
            Value = value;
        }
    }
}
