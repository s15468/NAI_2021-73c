using Lab3_RecommendationEngine.Database;
using Lab3_RecommendationEngine.Recommendation;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void RenderCustomMessage(string message)
            => Console.WriteLine(message);

        public void ClearConsole()
            => Console.Clear();

        public void SelectUserMessage()
        {
            Console.WriteLine("Select user by writing ID: ");
        }

        public void RenderScoreUsers(IEnumerable<RecommendationUserData> recommendationUsersData, AlgorithmType algorithm, RecommendationType recommendationType)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Top 5 {recommendationType} users");
            sb.AppendLine($"UserName\t\t{algorithm}");

            foreach (RecommendationUserData recommendationUserData in recommendationUsersData)
            {
                string nameTabulator = recommendationUserData.Name.Length < 17 ? "\t\t" : "\t";
                string score = algorithm == AlgorithmType.Manhattan
                    ? recommendationUserData.ManhattanScore.ToString("0.00")
                    : recommendationUserData.EuclideanScore.ToString("0.00");

                sb.AppendLine(recommendationUserData.Name + nameTabulator + score);
            }

            Console.WriteLine(sb);
        }

        public void SelectRecommendationAlgorithm(IEnumerable<AlgorithmType> algorithms)
        {
            int counter = default;

            foreach (AlgorithmType algorithm in algorithms)
            {
                Console.WriteLine($"{counter} {algorithm}");
                counter++;
            }

            Console.WriteLine("Select algorithm by writing ID or press 9 to exit: ");
        }

        public void SelectBestOrWorstRecommendation()
        {
            Console.WriteLine("Select recommendation by ID or press 8 to return previous menu or 9 to exit: ");
            Console.WriteLine("0 Best recommendation");
            Console.WriteLine("1 Worst recommendation");
        }

        public void SelectMovieToPrintDescription(IEnumerable<Movie> moviesList)
        {
            Console.Clear();

            int counter = default;

            foreach (Movie movie in moviesList)
            {
                Console.WriteLine($"{counter} {movie.Title}");
                counter++;
            }
            
            Console.WriteLine("Select movie by ID or press 8 to return previous menu or 9 to exit: ");
        }

        public void RenderMovieDescription(string movieDescription)
        {
            Console.Clear();
            Console.WriteLine(movieDescription);
            Console.WriteLine();
            Console.WriteLine("Press any key to back to previous menu");
        }
    }
}
