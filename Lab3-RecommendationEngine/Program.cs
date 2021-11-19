namespace Lab3_RecommendationEngine
{
    /*
     * Aplikacja napisana przez Juliana Chodorowskiego.
     * Do odpalenia aplikacji należy skopiować folder bin/Debug/net5.0 i odpalić Lab3-RecommendationEngine.exe na systemie Windows.
     * Możliwe także jest odpalenie przy użyciu Visual Studio programu poprzez wybudowanie całego repozytorium i odpalenie debug runu.
     */
    class Program
    {
        static void Main(string[] args)
        {
            new MenuService().MainMenu();
        }
    }
}
