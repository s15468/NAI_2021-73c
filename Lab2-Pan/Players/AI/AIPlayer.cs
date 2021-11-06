using Lab2_Pan.Cards;
using Lab2_Pan.Players.AI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan.Players
{

    /// <summary>
    /// Class implementing AIPlayer
    /// </summary>
    public sealed class AIPlayer : Player
    {
        private AIAdvancedProcessMove _advancedProcessMove;

        /// <summary>
        /// Default constructor with initializing variables;
        /// </summary>
        public AIPlayer()
        {
            _advancedProcessMove = new AIAdvancedProcessMove(getAIDifficulty());
        }

        /// <summary>
        /// Method to invoke player move
        /// </summary>
        /// <param name="aiCards">cards of ai player</param>
        /// <param name="stackCards">cards on stack</param>
        /// <param name="availableMoves">List of available moves</param>
        /// <returns>PlayerMove object representing player decision</returns>
        public override PlayerMove InvokeMove(List<ICard> aiCards, List<ICard> stackCards, List<GameMove> availableMoves)
        {
            GameMove mainDecision;
            _advancedProcessMove.RefreshAIDeckAndStackCards(aiCards, stackCards);

            if (availableMoves.Count > 1)
            {
                mainDecision = _advancedProcessMove.AnalyzeAndSelectMainDecision();
            }
            else
            {
                mainDecision = availableMoves.First();
            }

            PlayerMove moveResult;

            switch (mainDecision)
            {
                case GameMove.DrawStack:
                    moveResult = new PlayerMove() { MoveType = GameMove.DrawStack, Data = null };
                    break;
                case GameMove.PutCards:
                    moveResult = new PlayerMove() { MoveType = GameMove.PutCards, Data = _advancedProcessMove.AnalyzeAndPutCards(new List<int> { 1, 3, 4 }) };
                    break;
                default:
                    throw new NotImplementedException();
            }

            return moveResult;
        }

        /// <summary>
        /// Method to get random AI difficulty
        /// </summary>
        /// <returns>Random AIDifficulty</returns>
        private AIDifficulty getAIDifficulty()
        {
            var diffucultyTypes = Enum.GetValues(typeof(AIDifficulty));

            return (AIDifficulty)diffucultyTypes.GetValue(new Random().Next(diffucultyTypes.Length - 1));
        }
    }
}
