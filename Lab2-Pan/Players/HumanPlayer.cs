using Lab2_Pan.Cards;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Pan.Players
{
    /// <summary>
    /// Class implementing HumanPlayer
    /// </summary>
    public sealed class HumanPlayer : Player
    {
        private RenderService _renderService;

        /// <summary>
        /// Default constructor with initializing variables;
        /// </summary>
        public HumanPlayer()
        {
            _renderService = new RenderService();
        }

        /// <summary>
        /// Method to invoke player move
        /// </summary>
        /// <param name="aiCards">cards of human player</param>
        /// <param name="stackCards">cards on stack</param>
        /// <param name="availableMoves">List of available moves</param>
        /// <returns>PlayerMove object representing player decision</returns>
        public override PlayerMove InvokeMove(List<ICard> playerCards, List<ICard> stackCards, List<GameMove> availableMoves)
        {
            _renderService.ClearConsole();
            _renderService.RenderStackCards(stackCards);
            Console.WriteLine();
            _renderService.RenderPlayerCards(playerCards);
            Console.WriteLine();

            GameMove selectedMove = selectMove(availableMoves);

            switch (selectedMove)
            {
                case GameMove.PutCards:
                    return new PlayerMove() { MoveType = GameMove.PutCards, Data = selectCardsToPut(playerCards) };
                case GameMove.DrawStack:
                    return new PlayerMove() { MoveType = GameMove.DrawStack, Data = null };
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Private method which allowing user to select cards to put on stack
        /// </summary>
        /// <param name="playerCards">List of player cards in hand</param>
        /// <returns>List of cards to put on stack</returns>
        private IEnumerable<ICard> selectCardsToPut(IEnumerable<ICard> playerCards)
        {
            int[] availableNumberOfCardsToPut = new int[] { 1, 3, 4 };
            List<ICard> cardsToPut = new();

            while(true)
            {
                cardsToPut = new();
                Console.WriteLine("Enter id of cards to put. Use space to separate ids");
                string cardsId = Console.ReadLine();

                string[] inputCardsids = cardsId.Split(" ", StringSplitOptions.TrimEntries);

                foreach (string cardId in inputCardsids)
                {
                    if (int.TryParse(cardId, out int index))
                    {
                        if (index < playerCards.Count() && index > -1)
                        {
                            cardsToPut.Add(playerCards.ElementAt(index));
                        }
                    }
                }

                if (inputCardsids.Length == cardsToPut.Count && availableNumberOfCardsToPut.Any(x => cardsToPut.Count == x))
                {
                    return cardsToPut;
                }
            }
        }

        /// <summary>
        /// Method which allow user to select move type
        /// </summary>
        /// <param name="availableMoves">List of available game moves</param>
        /// <returns>Selected GameMove</returns>
        private GameMove selectMove(List<GameMove> availableMoves)
        {
            while(true)
            {
                _renderService.RenderAvailableMoves(availableMoves);
                string pressedKey = Console.ReadKey().KeyChar.ToString();
                
                if (int.TryParse(pressedKey, out int index))
                {
                    if (index <= availableMoves.Count - 1 && index >= 0)
                    {
                        return availableMoves[index];
                    }
                }
            }
        }
    }
}
