using Lab3_RecommendationEngine.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_RecommendationEngine.Recommendation
{
    /// <summary>
    /// Class implementing Manhattan algorithm to compute users scores
    /// </summary>
    public class ManhattanScore : IComputeScore
    {
        /// <summary>
        /// Method to calculate score for selected user and single user from database.
        /// </summary>
        /// <param name="userData">Single user from database other than selected.</param>
        /// <param name="currentUser">Selected user from database.</param>
        /// <returns>Manhattan score of single user from database.</returns>
        public double CalculateScore(RecommendationUserData userData, User currentUser)
        {
            List<double> diffRatings = new();

            foreach (Movie equalMovie in userData.EqualsMovies)
            {
                int currentUserRating = currentUser.Movie.First(x => x.Title.Equals(equalMovie.Title, StringComparison.OrdinalIgnoreCase)).Rating;
                diffRatings.Add(Math.Abs(currentUserRating - equalMovie.Rating));
            }

            if (diffRatings.Count() == 0)
            {
                return 100;
            }

            return getAlgorithmScore(diffRatings);
        }

        /// <summary>
        /// Method to calculate summary user score.
        /// </summary>
        /// <param name="squareDiffRatings">List of score for equal movies.</param>
        /// <returns>Euclidean score from all equal movies scores.</returns>
        private double getAlgorithmScore(List<double> diffRatings)
            => diffRatings.Sum();
    }
}
