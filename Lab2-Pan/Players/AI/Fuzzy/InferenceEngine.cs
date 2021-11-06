using Accord.Fuzzy;
using System.Collections.Generic;

namespace Lab2_Pan.Players.AI.Fuzzy
{
    /// <summary>
    /// Public class representing InferenceEnginec
    /// </summary>
    public class InferenceEngine
    {
        /// <summary>
        /// Method to start Fuzzy Engine and calculate decision
        /// </summary>
        /// <param name="rulesList">List of Rules to verify</param>
        /// <param name="MinStrength">Optional min strength when rule will passed</param>
        /// <returns></returns>
        public string StartFuzzyEngine(IEnumerable<Rule> rulesList, float MinStrength = 0f)
        {
            float lastPassedStrength = 0f;
            string result = string.Empty;

            foreach (var rule in rulesList)
            {
                float currentStrength = rule.EvaluateFiringStrength();

                if (currentStrength > MinStrength && currentStrength > lastPassedStrength)
                {
                    lastPassedStrength = currentStrength;
                    result = rule.Output.Label.Name;
                }

                if (currentStrength == 1)
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Method to change list of LinguisticVariable to Database
        /// </summary>
        /// <param name="lingVarsList">List of LinguisticVariables</param>
        /// <returns></returns>
        public Database CreateDatabase(List<LinguisticVariable> lingVarsList)
        {
            Database db = new();

            lingVarsList.ForEach(lingVar => db.AddVariable(lingVar));

            return db;
        }
    }
}
