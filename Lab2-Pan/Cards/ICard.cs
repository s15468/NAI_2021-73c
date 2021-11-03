namespace Lab2_Pan.Cards
{
    public interface ICard
    {
        Suit Suit { get;}
        Figure Figure { get; }
        int Value { get; }
    }
}
