using Lab3_RecommendationEngine.Database;
using System.Collections.Generic;

namespace Lab3_RecommendationEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseService databaseService = new DatabaseService();
            IEnumerable<User> users = databaseService.GetUsers();
        }
    }
}
