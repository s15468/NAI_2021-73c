using System;

namespace Lab1_Blackjack
{
    /// <summary>
    /// Public class implementing Human Player and his decision menu
    /// </summary>
    public class HumanPlayer : Player
    {
        /// <summary>
        /// Method which menu of Human player to make move.
        /// </summary>
        /// <returns>Returning status true - Draw card; false - Finish round</returns>
        public override bool MakeMove()
        {
            bool status;
            ConsoleKey selectedOption;

            do
            {
                renderPlayerMenu();
                selectedOption = getPlayerInput();
                status = selectedOption == ConsoleKey.D1 || selectedOption == ConsoleKey.D2;

                if (!status)
                {
                    renderWarning(selectedOption);
                }
            }
            while (!status);

            Console.WriteLine();

            return selectedOption == ConsoleKey.D1 ? true : false;
        }

        /// <summary>
        /// Method that rendering in console human player menu with available moves.
        /// </summary>
        private void renderPlayerMenu()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine($"Current points: {HandPoints}");
            Console.WriteLine();
            Console.WriteLine("Select option by pressing button:");
            Console.WriteLine("1 - Draw next card");
            Console.WriteLine("2 - End round");
        }

        /// <summary>
        /// Method which rendering warning option when player press invalid button.
        /// </summary>
        /// <param name="selectedOption">ConsoleKey as button pressed by player</param>
        private void renderWarning(ConsoleKey selectedOption)
        {
            Console.WriteLine($"Incorrect input. Clicked {selectedOption} but expected button 1 or 2");
        }

        /// <summary>
        /// Method to read human player input from keyboard as single button press.
        /// </summary>
        /// <returns>ConsoleKey representing pressed button</returns>
        private ConsoleKey getPlayerInput() => Console.ReadKey().Key;

    }
}
