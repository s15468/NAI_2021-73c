namespace Lab2_Pan
{
    /// <summary>
    /// Enum representing available for cards Suits
    /// </summary>
    public enum Suit
    {
        Club,
        Diamond,
        Heart,
        Spade,
    }

    /// <summary>
    /// Enum representing available for cards Figures
    /// </summary>
    public enum Figure
    {
        n9 = 1,
        n10 = 2,
        Jack = 3,
        Queen = 4,
        King = 5,
        Ace = 6,
    }

    /// <summary>
    /// Enum representing available AIDifficulties
    /// </summary>
    public enum AIDifficulty
    {
        Easy = 0,
        Normal = 1,
        Hard = 2,
    }

    /// <summary>
    /// Enum representing available GameMoves
    /// </summary>
    public enum GameMove
    {
        DrawStack,
        PutCards,
    }
}
