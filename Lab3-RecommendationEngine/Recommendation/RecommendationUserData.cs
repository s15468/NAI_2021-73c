using Lab3_RecommendationEngine.Database;
using System.Collections.Generic;

namespace Lab3_RecommendationEngine.Recommendation
{
    /// <summary>
    /// Class representing analyzed user from database.
    /// </summary>
    public class RecommendationUserData
    {
        /// <summary>
        /// Name of this user.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Collection of movies equals to movies selected from menu user.
        /// </summary>
        public List<Movie> EqualsMovies { get; }

        /// <summary>
        /// Collection of movies different to movies selected from menu user.
        /// </summary>
        public List<Movie> DifferentMovies { get; }

        /// <summary>
        /// Score counted by Euclidean algorithm.
        /// </summary>
        public double EuclideanScore { get; private set; }

        /// <summary>
        /// Score counted by Manhattan algorithm.
        /// </summary>
        public double ManhattanScore { get; private set; }

        /// <summary>
        /// Custom constructor with parameter to save name of user.
        /// </summary>
        /// <param name="name"></param>
        public RecommendationUserData(string name)
        {
            Name = name;
            EqualsMovies = new();
            DifferentMovies = new();
            EuclideanScore = 0;
        }


        /// <summary>
        /// Method to save euclidean score.
        /// </summary>
        /// <param name="score">Euclidean score.</param>
        public void SetEuclideanScore(double score)
        {
            EuclideanScore = score;
        }

        /// <summary>
        /// Method to save manhattan score.
        /// </summary>
        /// <param name="score">Manhattan score.</param>
        public void SetManhattanScore(double score)
        {
            ManhattanScore = score;
        }
    }
}
