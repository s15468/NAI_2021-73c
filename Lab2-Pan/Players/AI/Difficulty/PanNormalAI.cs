using Lab2_Pan.Cards;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan.Players.AI.Difficulty
{
    public class PanNormalAI : IPanDifficulty
    {
        public IEnumerable<ICard> GetCardsToPut(IEnumerable<ICard> aiCards, IEnumerable<ICard> stackCards, IEnumerable<int> availablePutMoves, List<ICard> cardsToPut)
        {
            List<ICard> sortedCards = new();
            List<ICard> tempHand = new();

            tempHand.AddRange(aiCards);
            cardsToPut.ForEach(card => tempHand.Remove(card));
            tempHand.OrderBy(x => x.Figure).ToList();

            List<ICard> availableCardsToPut = new();

            if (cardsToPut.Count > 0)
            {
                availableCardsToPut = tempHand.Where(x => x.Figure == aiCards.First().Figure).ToList();
            }
            else
            {
                ICard lastStackCard = stackCards.Last();

                if (tempHand.Any(card => card.Figure == lastStackCard.Figure))
                {
                    availableCardsToPut = tempHand.Where(x => x.Figure == lastStackCard.Figure).ToList();
                }
                else
                {
                    List<ICard> allAvailableCardToPut = tempHand.Where(x => (int)x.Figure > (int)lastStackCard.Figure).OrderBy(x => x.Figure).ToList();
                    availableCardsToPut = allAvailableCardToPut.Where(x => x.Figure == allAvailableCardToPut.First().Figure).ToList();
                }
            }

            foreach (int move in availablePutMoves)
            {
                if (cardsToPut.Count + availablePutMoves.Count() >= move && cardsToPut.Count < move)
                {
                    if (move == 1)
                    {
                        cardsToPut.Add(availableCardsToPut.First());
                    }
                    else
                    {
                        for (int i = 0; i <= move; i++)
                        {
                            if (cardsToPut.Count < move)
                            {
                                cardsToPut.Add(availableCardsToPut[i]);
                            }
                        }
                    }

                    break;
                }
            }

            return cardsToPut;
        }

        public int GetNumberOfCardsToDraw(IEnumerable<ICard> aiCards, IEnumerable<ICard> stackCards, List<int> availableDrawCardsMoves)
        {
            ICard lowerCard = aiCards.OrderBy(x => x.Figure).First();
            int counter = 0;

            foreach (ICard stackCard in stackCards)
            {
                if ((int)stackCard.Figure > (int)lowerCard.Figure)
                {
                    counter++;
                }
            }

            int result = availableDrawCardsMoves.OrderByDescending(x => x).FirstOrDefault(x => x <= counter);

            return result == 0 ? availableDrawCardsMoves.Min() : result;
        }
    }
}
