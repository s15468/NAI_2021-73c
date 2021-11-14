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
            List<double> SquareDiffRatings = new();

            foreach (Movie equalMovie in userData.EqualsMovies)
            {
                int currentUserRating = currentUser.Movie.First(x => x.Title == equalMovie.Title).Rating;
                SquareDiffRatings.Add(Math.Pow(currentUserRating - equalMovie.Rating, 2));
            }

            if (SquareDiffRatings.Count() == 0)
            {
                return 0;
            }



            return getAlgorithmScore(SquareDiffRatings);
        }

        private double getAlgorithmScore(List<double> squareDiffRatings)
        {
            return 1 / (1 + Math.Sqrt(squareDiffRatings.Sum()));
        }
    }
}
