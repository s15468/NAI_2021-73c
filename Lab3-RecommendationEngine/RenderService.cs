using Lab3_RecommendationEngine.Database;
using Lab3_RecommendationEngine.Recommendation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3_RecommendationEngine
{
    /// <summary>
    /// Method to render application UI in console.
    /// </summary>
    public class RenderService
    {
        /// <summary>
        /// Method to render all users in collection.
        /// </summary>
        /// <param name="users">Collection of users.</param>
        public void RenderUsers(IEnumerable<User> users)
        {
            int counter = 0;

            foreach (User user in users)
            {
                Console.WriteLine($"{counter} - {user.Name}");
                counter++;
            }
        }

        /// <summary>
        /// Method to render custom message in console.
        /// </summary>
        /// <param name="message">Custom message to print.</param>
        public void RenderCustomMessage(string message)
            => Console.WriteLine(message);

        /// <summary>
        /// Method to clear console from output.
        /// </summary>
        public void ClearConsole()
            => Console.Clear();

        /// <summary>
        /// Method to write select user message.
        /// </summary>
        public void SelectUserMessage()
        {
            Console.WriteLine("Select user by writing ID: ");
        }

        /// <summary>
        /// Method to render best/worst users in console.
        /// </summary>
        /// <param name="recommendationUsersData">Collection of users to print with scores.</param>
        /// <param name="algorithm">Algorithm type which score should be printed.</param>
        /// <param name="recommendationType">Type of recommendation to print in header of output.</param>
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

        /// <summary>
        /// Method which rendering custom menu to select algorithm.
        /// </summary>
        /// <param name="algorithms">Collection of available algorithms.</param>
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

        /// <summary>
        /// Method which rendering custom menu to select recommendation type.
        /// </summary>
        public void SelectBestOrWorstRecommendation()
        {
            Console.WriteLine("Select recommendation by ID or press 8 to return previous menu or 9 to exit: ");
            Console.WriteLine("0 Best recommendation");
            Console.WriteLine("1 Worst recommendation");
        }

        /// <summary>
        /// Method to render custom menu to select movie which description user want to read.
        /// </summary>
        /// <param name="moviesList">List of best/worst movies.</param>
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

        /// <summary>
        /// Method to render selected movie description.
        /// </summary>
        /// <param name="movieDescription">Description of movie.</param>
        public void RenderMovieDescription(string movieDescription)
        {
            Console.Clear();
            Console.WriteLine(movieDescription);
            Console.WriteLine();
            Console.WriteLine("Press any key to back to previous menu");
        }
    }
}
