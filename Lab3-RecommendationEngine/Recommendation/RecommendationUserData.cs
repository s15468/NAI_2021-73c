using Lab3_RecommendationEngine.Database;
using System.Collections.Generic;

namespace Lab3_RecommendationEngine.Recommendation
{
    public class RecommendationUserData
    {
        public string Name { get; }
        public List<Movie> EqualsMovies { get; }
        public List<Movie> DifferentMovies { get; }
        public double EuclideanScore { get; private set; }
        public double ManhattanScore { get; private set; }

        public RecommendationUserData(string name)
        {
            Name = name;
            EqualsMovies = new();
            DifferentMovies = new();
            EuclideanScore = 0;
        }

        public void SetEuclideanScore(double score)
        {
            EuclideanScore = score;
        }

        public void SetManhattanScore(double score)
        {
            ManhattanScore = score;
        }
    }
}
