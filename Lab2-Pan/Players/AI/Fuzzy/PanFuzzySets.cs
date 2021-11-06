using Accord.Fuzzy;
using System.Collections.Generic;
using static Accord.Fuzzy.TrapezoidalFunction;

namespace Lab2_Pan.Players.AI
{
    /// <summary>
    /// Public class representing FuzzySets
    /// </summary>
    public class PanFuzzySets
    {
        #region Global Fuzzy Sets

        /// <summary>
        /// FuzzySet of AI difficulty
        /// </summary>
        /// <returns>List of created FuzzySets</returns>
        public List<FuzzySet> AIDiffuculty()
        {
            return new List<FuzzySet>
            {
                new CustomFuzzySet(AIDifficulty.Easy, new SingletonFunction(0)),
                new CustomFuzzySet(AIDifficulty.Normal, new SingletonFunction(1)),
                new CustomFuzzySet(AIDifficulty.Hard, new SingletonFunction(2))
            };
        }

        /// <summary>
        /// FuzzySet of number of cards in AI Hand
        /// </summary>
        /// <returns>List of created FuzzySets</returns>
        public List<FuzzySet> NumberOfCardsInHand(int deckSize)
        {
            return new List<FuzzySet>
            {
                new CustomFuzzySet(PanDistToWin.VeryClose, new TrapezoidalFunction(0, 0, 2, 3)),
                new CustomFuzzySet(PanDistToWin.Close, new TrapezoidalFunction(3, 4, 5)),
                new CustomFuzzySet(PanDistToWin.Normal, new TrapezoidalFunction(4, 6, 8)),
                new CustomFuzzySet(PanDistToWin.Far, new TrapezoidalFunction(6, (deckSize / 2) - 6, deckSize / 2)),
                new CustomFuzzySet(PanDistToWin.VeryFar, new TrapezoidalFunction(deckSize / 2, deckSize, EdgeType.Left))
            };
        }

        #endregion

        #region Main Decision Fuzzy Sets

        /// <summary>
        /// FuzzySet of is start card in hand
        /// </summary>
        /// <returns>List of created FuzzySets</returns>
        public List<FuzzySet> IsStartCardInHand()
        {
            return new List<FuzzySet>
            {
                new CustomFuzzySet(PanBoolean.False, new SingletonFunction(0)),
                new CustomFuzzySet(PanBoolean.True, new SingletonFunction(1))
            };
        }

        /// <summary>
        /// FuzzySet of is any card of second player should be already on stack
        /// </summary>
        /// <returns>List of created FuzzySets</returns>
        public List<FuzzySet> IsAnyFromRestCardShouldBeAlreadyOnStack()
        {
            return new List<FuzzySet>
            {
                new CustomFuzzySet(PanBoolean.False, new SingletonFunction(0)),
                new CustomFuzzySet(PanBoolean.True, new SingletonFunction(1))
            };
        }

        /// <summary>
        /// FuzzySet of is any card from hand should be on stack
        /// </summary>
        /// <returns>List of created FuzzySets</returns>
        public List<FuzzySet> IsAnyCardFromHandShouldBeAlreadyOnStack()
        {
            return new List<FuzzySet>
            {
                new CustomFuzzySet(PanBoolean.False, new SingletonFunction(0)),
                new CustomFuzzySet(PanBoolean.True, new SingletonFunction(1))
            };
        }

        /// <summary>
        /// FuzzySet of is put move available
        /// </summary>
        /// <returns>List of created FuzzySets</returns>
        public List<FuzzySet> AnyPutMoveIsAvailable()
        {
            return new List<FuzzySet>
            {
                new CustomFuzzySet(PanBoolean.False, new SingletonFunction(0)),
                new CustomFuzzySet(PanBoolean.True, new SingletonFunction(1))
            };
        }

        /// <summary>
        /// FuzzySet of is ace on stack
        /// </summary>
        /// <returns>List of created FuzzySets</returns>
        public List<FuzzySet> AnyAceOnStack()
        {
            return new List<FuzzySet>
            {
                new CustomFuzzySet(PanBoolean.False, new SingletonFunction(0)),
                new CustomFuzzySet(PanBoolean.True, new SingletonFunction(1))
            };
        }

        /// <summary>
        /// FuzzySet of is only ace in hand
        /// </summary>
        /// <returns>List of created FuzzySets</returns>
        public List<FuzzySet> IsOnlyAceLeftInHand()
        {
            return new List<FuzzySet>
            {
                new CustomFuzzySet(PanBoolean.False, new SingletonFunction(0)),
                new CustomFuzzySet(PanBoolean.True, new SingletonFunction(1))
            };
        }

        /// <summary>
        /// FuzzySet of decision making
        /// </summary>
        /// <returns>List of created FuzzySets</returns>
        public List<FuzzySet> Decisions()
        {
            return new List<FuzzySet>
            {
                new CustomFuzzySet(GameMove.DrawStack, new SingletonFunction(0)),
                new CustomFuzzySet(GameMove.PutCards, new SingletonFunction(1))
            };
        }

        #endregion

        #region Card Draw Fuzzy Sets

        /// <summary>
        /// FuzzySet of number of cards to draw or put
        /// </summary>
        /// <returns>List of created FuzzySets</returns>
        public List<FuzzySet> NumberOfCardsToDrawOrPut(List<int> numOfCardsToDraw)
        {
            var fuzzySets = new List<FuzzySet>();

            foreach (var pos in numOfCardsToDraw)
            {
                fuzzySets.Add(new FuzzySet($"{pos}", new SingletonFunction(pos)));
            }

            return fuzzySets;
        }

        #endregion
    }
}
