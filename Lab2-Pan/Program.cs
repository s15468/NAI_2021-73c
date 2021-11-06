using System;

namespace Lab2_Pan
{
    /*
     * Aplikacja napisana przez Juliana Chodorowskiego.
     * Do odpalenia aplikacji należy skopiować folder bin/Debug/net5.0 i odpalić Lab2-Pan.exe na systemie Windows.
     * Możliwe także jest odpalenie przy użyciu Visual Studio programu poprzez wybudowanie całego repozytorium i odpalenie debug runu.
     * 
     * Zasady:
     * Można położyć 1,3 lub 4 karty o takiej samej figurze.
     * Pierwsza karta musi mieć tą samą lub wyższą figurę niż ostatnia na stosie.
     * Kiedy gracz nie może położyć żadnej karty albo nie chce to pobiera 3 karty.
     */
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
