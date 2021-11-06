using Accord.Fuzzy;
using System.Collections.Generic;

namespace Lab2_Pan.Players.AI
{
    /// <summary>
    /// Public class of every AI Rules to analyze
    /// </summary>
    public sealed class PanRules
    {
        /// <summary>
        /// Method to generate rules for every AI
        /// </summary>
        /// <param name="db">Database with Linguistic Variables</param>
        /// <returns>List of generated rules</returns>
        public IEnumerable<Rule> GenerateRulesForMainDecision(Database db)
        {
            return new List<Rule>
            {
                //Easy AI
                new Rule(
                    db,
                    "Easy - Stack Draw",
                    $"IF {PanLingVar.AIDifficulty} IS {AIDifficulty.Easy} " +
                    $"AND {PanLingVar.IsAnyCardFromHandShouldBeAlreadyOnStack} IS {PanBoolean.True} " +
                    $"THEN {PanLingVar.Decision} IS {GameMove.DrawStack}"),
                new Rule(
                    db,
                    "Easy - Card Put",
                    $"IF {PanLingVar.AIDifficulty} IS {AIDifficulty.Easy} " +
                    $"AND {PanLingVar.IsAnyCardFromHandShouldBeAlreadyOnStack} IS {PanBoolean.False} " +
                    $"AND {PanLingVar.AnyPutMoveIsAvailable} IS {PanBoolean.True} " +
                    $"THEN {PanLingVar.Decision} IS {GameMove.PutCards}"),

                // Normal AI 
                new Rule(
                    db,
                    "Normal - Stack Draw - ANY AI card should be on Stack AND number of AI cards IS NOT Normal/Far/VeryFar",
                    $"IF {PanLingVar.AIDifficulty} IS {AIDifficulty.Normal} " +
                    $"AND {PanLingVar.IsAnyCardFromHandShouldBeAlreadyOnStack} IS {PanBoolean.True} " +
                    $"AND {PanLingVar.NumberOfCardsInHand} IS NOT {PanDistToWin.Normal} " +
                    $"AND {PanLingVar.NumberOfCardsInHand} IS NOT {PanDistToWin.Far} " +
                    $"AND {PanLingVar.NumberOfCardsInHand} IS NOT {PanDistToWin.VeryFar} " +
                    $"THEN {PanLingVar.Decision} IS {GameMove.DrawStack}"),
                new Rule(
                    db,
                    "Normal - Card Put - ANY AI/Enemy card should be on Stack AND Number of AI cards IS NOT VeryClose",
                    $"IF {PanLingVar.AIDifficulty} IS {AIDifficulty.Normal} " +
                    $"AND {PanLingVar.IsAnyCardFromHandShouldBeAlreadyOnStack} IS {PanBoolean.True} " +
                    $"AND {PanLingVar.IsAnyFromRestCardShouldBeAlreadyOnStack} IS {PanBoolean.True} " +
                    $"AND {PanLingVar.NumberOfCardsInHand} IS NOT {PanDistToWin.VeryClose} " +
                    $"AND {PanLingVar.AnyPutMoveIsAvailable} IS {PanBoolean.True} " +
                    $"THEN {PanLingVar.Decision} IS {GameMove.PutCards}"),
                new Rule(
                    db,
                    "Normal - Card Put - NOT ANY AI card should be on Stack",
                    $"IF {PanLingVar.AIDifficulty} IS {AIDifficulty.Normal} " +
                    $"AND {PanLingVar.IsAnyCardFromHandShouldBeAlreadyOnStack} IS {PanBoolean.False} " +
                    $"AND {PanLingVar.AnyPutMoveIsAvailable} IS {PanBoolean.True} " +
                    $"THEN {PanLingVar.Decision} IS {GameMove.PutCards}"),

                // Hard AI
                new Rule(
                    db,
                    "Hard - Stack Draw - ANY Ace card on Stack",
                    $"IF {PanLingVar.AIDifficulty} IS {AIDifficulty.Hard} " +
                    $"AND {PanLingVar.AnyAceOnStack} IS {PanBoolean.True} " +
                    $"AND {PanLingVar.IsOnlyAceLeftInHand} IS {PanBoolean.False} " +
                    $"THEN {PanLingVar.Decision} IS {GameMove.DrawStack}"),
                new Rule(
                    db,
                    "Hard - Stack Draw - ANY card in hand should be on stack and Number of AI cards is VeryClose",
                    $"IF {PanLingVar.AIDifficulty} IS {AIDifficulty.Hard} " +
                    $"AND {PanLingVar.IsAnyCardFromHandShouldBeAlreadyOnStack} IS {PanBoolean.True} " +
                    $"AND {PanLingVar.NumberOfCardsInHand} IS {PanDistToWin.VeryClose} " +
                    $"THEN {PanLingVar.Decision} IS {GameMove.DrawStack}"),
                new Rule(
                    db,
                    "Hard - Card Put - Ace only in Hands",
                    $"IF {PanLingVar.AIDifficulty} IS {AIDifficulty.Hard} " +
                    $"AND {PanLingVar.IsOnlyAceLeftInHand} IS {PanBoolean.True} " +
                    $"THEN {PanLingVar.Decision} IS {GameMove.PutCards}"),

                // Global
                new Rule(
                    db,
                    "All Difficulty - Place start card on board",
                    $"IF {PanLingVar.IsStartCardInHand} IS {PanBoolean.True} " +
                    $"OR {PanLingVar.AnyPutMoveIsAvailable} IS {PanBoolean.True} " +
                    $"THEN {PanLingVar.Decision} IS {GameMove.PutCards}"),
                new Rule(
                    db,
                    "Global Stack Draw",
                   $"IF {PanLingVar.AnyPutMoveIsAvailable} IS {PanBoolean.False} " +
                   $"THEN {PanLingVar.Decision} IS {GameMove.DrawStack}"),
            };
        }
    }
}
