
namespace Lab2_Pan.Cards
{
    public sealed class Card : ICard
    {
        public Suit Suit { get; private set; }
        public Figure Figure { get; private set; }
        public int Value { get; private set; }

        public Card(Suit suit, Figure figure)
        {
            Suit = suit;
            Figure = figure;
            Value = (int)figure;
        }
    }
}
