using Accord.Fuzzy;
using System;

namespace Lab2_Pan.Players.AI
{
    /// <summary>
    /// Public class that extends a LinguisticVariable class
    /// </summary>
    public class CustomLinguisticVariable : LinguisticVariable
    {
        /// <summary>
        /// Public custom constructor of class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public CustomLinguisticVariable(Enum name, float start, float end) : base(name.ToString(), start, end) { }
    }

    /// <summary>
    /// Public class that extends a FuzzySet class
    /// </summary>
    public class CustomFuzzySet : FuzzySet
    {
        /// <summary>
        /// Public custom constructor of class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="function"></param>
        public CustomFuzzySet(Enum name, IMembershipFunction function) : base(name.ToString(), function) { }
    }
}
