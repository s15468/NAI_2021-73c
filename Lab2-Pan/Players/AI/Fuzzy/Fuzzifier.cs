using Accord.Fuzzy;
using System.Collections.Generic;

namespace Lab2_Pan.Players.AI
{
    /// <summary>
    /// Public class implementing Fuzzifier
    /// </summary>
    public class Fuzzifier
    {
        /// <summary>
        /// Method to add every FuzzySet to LinguisticVariable object.
        /// </summary>
        /// <param name="lingvar">LinguisticVariable where want to add FuzzySets</param>
        /// <param name="fuzzySetsList">List of FuzzySets</param>
        /// <returns></returns>
        protected LinguisticVariable addFuzzySetsAsLinguisticVariableLabels(LinguisticVariable lingvar, List<FuzzySet> fuzzySetsList)
        {
            fuzzySetsList.ForEach(fuzzySet => lingvar.AddLabel(fuzzySet));

            return lingvar;
        }
    }
}
