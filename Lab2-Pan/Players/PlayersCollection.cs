using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan.Players
{
    public class PlayersCollection : ICollection<IPlayer>
    {
        private List<IPlayer> Players;

        public PlayersCollection()
        {
            Players = new List<IPlayer>();
        }

        public int Count => Players.Count();

        public bool IsReadOnly => true;

        public void Add(IPlayer item) => Players.Add(item);

        public void Clear() => Players.Clear();

        public bool Contains(IPlayer item) => Players.Contains(item);

        public void CopyTo(IPlayer[] array, int arrayIndex) => throw new System.NotImplementedException();

        public bool Remove(IPlayer item) => Players.Remove(item);

        public IEnumerator<IPlayer> GetEnumerator() => Players.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Players.GetEnumerator();

        public IPlayer GetCurrentPlayer() => Players.GetEnumerator().Current;

        public IPlayer MoveToNextPlayer()
        {
            IEnumerator<IPlayer> enumerator = GetEnumerator(); 

            if (!enumerator.MoveNext())
            {
                enumerator.Reset();
                enumerator.MoveNext();
            }

            return enumerator.Current;
        }
    }
}
