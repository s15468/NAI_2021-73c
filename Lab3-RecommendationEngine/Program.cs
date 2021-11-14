using Lab3_RecommendationEngine.Database;
using Lab3_RecommendationEngine.Recommendation;
using Lab3_RecommendationEngine.REST;
using System.Collections.Generic;

namespace Lab3_RecommendationEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuService menuService = new MenuService();
            TheMovieDBApiService TMDBApiService = new TheMovieDBApiService();
            
            IEnumerable<User> allUsers = new DatabaseService().GetUsers();
            User currentUser = menuService.SelectUserToRecommendMovies(allUsers);
            RecommendationService recommendationService = new RecommendationService(currentUser, allUsers);

            recommendationService.CalculateScore();
        }
    }
}
