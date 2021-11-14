using Lab3_RecommendationEngine.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_RecommendationEngine
{
    public class MenuService
    {
        private readonly RenderService _renderService;

        public MenuService()
        {
            _renderService = new RenderService();
        }

        public User SelectUserToRecommendMovies(IEnumerable<User> users)
        {
            do
            {
                _renderService.RenderUsers(users);
                _renderService.SelectUserMessage();

                var input = Console.ReadLine();

                if (int.TryParse(input, out int index) && (index >= 0 && index <= users.Count() - 1))
                {
                    return users.ElementAt(index);
                }

                Console.WriteLine("Write incorrect index of user. Press anything to start again.");
                Console.ReadKey();
                Console.Clear();
            } while (true);
        }
    }
}
