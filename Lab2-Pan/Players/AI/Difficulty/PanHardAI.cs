using Lab2_Pan.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan.Players.AI.Difficulty
{
    class PanHardAI : IPanDifficulty
    {
        public IEnumerable<ICard> GetCardsToPut(IEnumerable<ICard> aiCards, IEnumerable<ICard> stackCards, IEnumerable<int> availablePutMoves, List<ICard> cardsToPut)
        {
            List<ICard> sortedCards = new();
            List<ICard> tempHand = new();

            tempHand.AddRange(aiCards);
            cardsToPut.ForEach(card => tempHand.Remove(card));
            tempHand = tempHand.OrderBy(x => x.Figure).ToList();

            List<ICard> availableCardsToPut = new();

            if (cardsToPut.Count > 0)
            {
                availableCardsToPut = tempHand.Where(x => x.Figure == cardsToPut.First().Figure).ToList();
            }
            else
            {
                ICard lastStackCard = stackCards.Last();

                availableCardsToPut = tempHand.Where(card => (int)card.Figure >= (int)lastStackCard.Figure).ToList();

                if (lastStackCard.Figure != Figure.Ace && availableCardsToPut.Any(card => card.Figure != Figure.Ace))
                {
                    availableCardsToPut = tempHand.FindAll(card => card.Figure != Figure.Ace);
                }
            }

            if (availableCardsToPut != null)
            {
                ICard lastHandCardToPut = availableCardsToPut.Last();

                availableCardsToPut = availableCardsToPut.FindAll(card => card.Figure == lastHandCardToPut.Figure);
            }

            if (!availableCardsToPut.Any(card => card.Figure != Figure.Ace) && aiCards.Any(card => card.Figure != Figure.Ace))
            {
                int minCardsToPut = availablePutMoves.Min();

                availableCardsToPut.RemoveRange(minCardsToPut, availableCardsToPut.Count() - minCardsToPut);
            }

            foreach (int move in availablePutMoves)
            {
                if (cardsToPut.Count + availableCardsToPut.Count >= move && cardsToPut.Count < move)
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
            List<ICard> tempHand = new();
            tempHand.AddRange(aiCards);
            tempHand.RemoveAll(x => x.Figure == Figure.Ace);

            ICard highestCard = tempHand.OrderBy(x => x.Figure).Last();
            int counter = 0;

            if (stackCards.Any(stackCard => stackCard.Figure == Figure.Ace))
            {
                counter = stackCards.Where(stackCard => stackCard.Figure == Figure.Ace).Count();
            }
            else
            {
                foreach (var stackCard in stackCards)
                {
                    if ((int)stackCard.Figure > (int)highestCard.Figure)
                    {
                        counter++;
                    }
                }
            }

            int result = availableDrawCardsMoves.OrderByDescending(x => x).FirstOrDefault(x => x <= counter);

            return result == 0 ? availableDrawCardsMoves.Min() : result;
        }
    }
}
