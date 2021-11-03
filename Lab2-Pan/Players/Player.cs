using Lab2_Pan.Cards;
using System.Collections.Generic;

namespace Lab2_Pan.Players
{
    public abstract class Player : IPlayer
    {
        public List<ICard> Cards { get; protected set; }

        protected Player()
        {
            Cards = new List<ICard>();
        }

        public void AddCard(ICard card) => Cards.Add(card);

        public abstract int[] InvokeMove();
    }
}
