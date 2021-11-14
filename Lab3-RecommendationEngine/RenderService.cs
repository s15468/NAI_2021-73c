using Lab3_RecommendationEngine.Database;
using System;
using System.Collections.Generic;

namespace Lab3_RecommendationEngine
{
    public class RenderService
    {
        public void RenderUsers(IEnumerable<User> users)
        {
            int counter = 0;

            foreach (User user in users)
            {
                Console.WriteLine($"{counter} - {user.Name}");
                counter++;
            }
        }

        public void SelectUserMessage()
        {
            Console.WriteLine("Select user by writing ID: ");
        }
    }
}
