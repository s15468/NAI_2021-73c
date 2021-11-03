using Lab2_Pan.Cards;
using System.Collections.Generic;

namespace Lab2_Pan.Players
{
    public interface IPlayer
    {
        List<ICard> Cards { get; }

        int[] InvokeMove();
        void AddCard(ICard card);
    }
}
