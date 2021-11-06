using Lab2_Pan.Cards;
using System.Collections.Generic;

namespace Lab2_Pan.Players.AI.Difficulty
{
    public interface IPanDifficulty
    {
        IEnumerable<ICard> GetCardsToPut(IEnumerable<ICard> aiCards, IEnumerable<ICard> stackCards, IEnumerable<int> availablePutMoves, List<ICard> cardsToPut);
    }
}
