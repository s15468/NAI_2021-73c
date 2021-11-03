using System;

namespace Lab2_Pan
{
    class Program
    {
        private static GameService gameService = new GameService();

        static void Main(string[] args)
        {
            Console.WriteLine("P A N");
            gameService.PrepareGame();
            gameService.StartGame();
        }
    }
}
