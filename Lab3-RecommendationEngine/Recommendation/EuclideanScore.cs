using Lab3_RecommendationEngine.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_RecommendationEngine.Recommendation
{
    /// <summary>
    /// Class implementing Euclidean algorithm to compute users scores
    /// </summary>
    public class EuclideanScore : IComputeScore
    {
        /// <summary>
        /// Method to calculate score for selected user and single user from database.
        /// </summary>
        /// <param name="userData">Single user from database other than selected.</param>
        /// <param name="currentUser">Selected user from database.</param>
        /// <returns>Euclidean score of single user from database.</returns>
        public double CalculateScore(RecommendationUserData userData, User currentUser)
        {
            List<double> squareDiffRatings = new();

            foreach (Movie equalMovie in userData.EqualsMovies)
            {
                int currentUserRating = currentUser.Movie.First(x => x.Title.Equals(equalMovie.Title, StringComparison.OrdinalIgnoreCase)).Rating;
                squareDiffRatings.Add(Math.Pow(currentUserRating - equalMovie.Rating, 2));
            }

            if (squareDiffRatings.Count() == 0)
            {
                return 0;
            }

            return getAlgorithmScore(squareDiffRatings);
        }

        /// <summary>
        /// Method to calculate summary user score.
        /// </summary>
        /// <param name="squareDiffRatings">List of score for equal movies.</param>
        /// <returns>Euclidean score from all equal movies scores.</returns>
        private double getAlgorithmScore(List<double> squareDiffRatings)
            => 1 / (1 + Math.Sqrt(squareDiffRatings.Sum()));
    }
}
