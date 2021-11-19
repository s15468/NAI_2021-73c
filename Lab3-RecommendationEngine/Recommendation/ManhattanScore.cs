using Lab3_RecommendationEngine.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_RecommendationEngine.Recommendation
{
    public class ManhattanScore : IComputeScore
    {
        public double CalculateScore(RecommendationUserData userData, User currentUser)
        {
            List<double> diffRatings = new();

            foreach (Movie equalMovie in userData.EqualsMovies)
            {
                int currentUserRating = currentUser.Movie.First(x => x.Title == equalMovie.Title).Rating;
                diffRatings.Add(Math.Abs(currentUserRating - equalMovie.Rating));
            }

            if (diffRatings.Count() == 0)
            {
                return 100;
            }

            return getAlgorithmScore(diffRatings);
        }

        private double getAlgorithmScore(List<double> diffRatings)
            => diffRatings.Sum();
    }
}
