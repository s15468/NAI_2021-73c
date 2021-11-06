using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan.Players
{
    /// <summary>
    /// Method implementing Collection of players
    /// </summary>
    public class PlayersCollection : ICollection<IPlayer>
    {
        private List<IPlayer> Players;

        /// <summary>
        /// Default constructor with initializing variables.
        /// </summary>
        public PlayersCollection()
        {
            Players = new List<IPlayer>();
        }

        /// <summary>
        /// Property to get numer of players
        /// </summary>
        public int Count => Players.Count();

        /// <summary>
        /// Property to get if collection is readonly
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Method which adding new player to collection
        /// </summary>
        /// <param name="item">Instance of player</param>
        public void Add(IPlayer item) => Players.Add(item);

        /// <summary>
        /// Method which clearing list of players
        /// </summary>
        public void Clear() => Players.Clear();

        /// <summary>
        /// Method which checking if expected player is already added
        /// </summary>
        /// <param name="item">Player to check if already on list</param>
        /// <returns>boolean as status if player is added</returns>
        public bool Contains(IPlayer item) => Players.Contains(item);

        /// <summary>
        /// Method to copy array of players to collection
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(IPlayer[] array, int arrayIndex) => throw new System.NotImplementedException();

        /// <summary>
        /// Method to remove player from collection
        /// </summary>
        /// <param name="item">Instance of player to remove</param>
        /// <returns>boolean as true if remove successfully</returns>
        public bool Remove(IPlayer item) => Players.Remove(item);

        /// <summary>
        /// Method to get colleciton enumerator
        /// </summary>
        /// <returns>IEnumerator Of collection</returns>
        public IEnumerator<IPlayer> GetEnumerator() => Players.GetEnumerator();

        /// <summary>
        /// Method to get colleciton enumerator
        /// </summary>
        /// <returns>IEnumerator Of collection</returns>
        IEnumerator IEnumerable.GetEnumerator() => Players.GetEnumerator();

        /// <summary>
        /// Method to get current element from enumerator
        /// </summary>
        /// <returns>Current player</returns>
        public IPlayer GetCurrentPlayer() => Players.GetEnumerator().Current;
    }
}
