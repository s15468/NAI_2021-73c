
using Lab2_Pan.Cards;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan.Players.AI.Difficulty
{
    public sealed class PanEasyAI : IPanDifficulty
    {
        public IEnumerable<ICard> GetCardsToPut(IEnumerable<ICard> aiCards, IEnumerable<ICard> stackCards, IEnumerable<int> availablePutMoves, List<ICard> cardsToPut)
        {
            List<ICard> sortedCards = aiCards.Where(aiCard => (int)aiCard.Figure >= (int)stackCards.Last().Figure).OrderBy(x => x.Figure).ToList();

            for (int i = 0; i < availablePutMoves.First(); i++)
            {
                cardsToPut.Add(sortedCards.First());
                sortedCards.Remove(sortedCards.First());
            }

            return cardsToPut;
        }
    }
}
