namespace Lab2_Pan.Players.AI
{
    /// <summary>
    /// Public enum representing every LinguisticVariables analyze by FuzzyEngine
    /// </summary>
    public enum PanLingVar
    {
        AIDifficulty,
        IsStackSort,
        IsStartCardInHand,
        IsAnyCardFromHandShouldBeAlreadyOnStack,
        IsAnyFromRestCardShouldBeAlreadyOnStack,
        NumberOfCardsInHand,
        NumberOfCardsToDraw,
        NumberOfCardsToPut,
        AnyPutMoveIsAvailable,
        AnyAceOnStack,
        IsOnlyAceLeftInHand,
        Decision
    }

    /// <summary>
    /// Public enum representing Boolean variable as Enum
    /// </summary>
    public enum PanBoolean
    {
        True,
        False,
    }

    /// <summary>
    /// Public enum representing how close AI is to win game
    /// </summary>
    public enum PanDistToWin
    {
        VeryClose,
        Close,
        Normal,
        Far,
        VeryFar,
    }
}
