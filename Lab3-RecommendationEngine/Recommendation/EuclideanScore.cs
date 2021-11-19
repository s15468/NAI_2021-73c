using Lab3_RecommendationEngine.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_RecommendationEngine.Recommendation
{
    public class EuclideanScore : IComputeScore
    {
        public double CalculateScore(RecommendationUserData userData, User currentUser)
        {
            List<double> squareDiffRatings = new();

            foreach (Movie equalMovie in userData.EqualsMovies)
            {
                int currentUserRating = currentUser.Movie.First(x => x.Title == equalMovie.Title).Rating;
                squareDiffRatings.Add(Math.Pow(currentUserRating - equalMovie.Rating, 2));
            }

            if (squareDiffRatings.Count() == 0)
            {
                return 0;
            }

            return getAlgorithmScore(squareDiffRatings);
        }

        private double getAlgorithmScore(List<double> squareDiffRatings)
            => 1 / (1 + Math.Sqrt(squareDiffRatings.Sum()));
    }
}
