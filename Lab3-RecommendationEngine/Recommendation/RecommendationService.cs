using Lab3_RecommendationEngine.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_RecommendationEngine.Recommendation
{
    /// <summary>
    /// Class representing service to recommendate movies.
    /// </summary>
    public class RecommendationService
    {
        /// <summary>
        /// Field representing selected user.
        /// </summary>
        private User _currentUser;

        /// <summary>
        /// Field representing all other users from database expected selected user.
        /// </summary>
        private IEnumerable<User> _otherUsers;

        /// <summary>
        /// List of user from database which are already analyzed.
        /// </summary>
        private List<RecommendationUserData> _recommendationUserData;

        /// <summary>
        /// Custom constructor of class which initializing fields;
        /// </summary>
        /// <param name="currentUser">Selected user to recommend movies.</param>
        /// <param name="allUsers">Collection of all users in database.</param>
        public RecommendationService(User currentUser, IEnumerable<User> allUsers)
        {
            _recommendationUserData = new();
            _currentUser = currentUser;
            _otherUsers = getUsersWithoutSelected(currentUser, allUsers);
        }

        /// <summary>
        /// Method to calculate for every user in database score to selected user.
        /// </summary>
        public void CalculateScore()
        {
            foreach (User otherUser in _otherUsers)
            {
                RecommendationUserData userData = new(otherUser.Name);

                analyzeMovies(userData, otherUser);

                foreach (IComputeScore algorithm in getComputeScoreList())
                {
                    double score = algorithm.CalculateScore(userData, _currentUser);

                    switch (algorithm.GetType())
                    {
                        case var a when a == typeof(EuclideanScore):
                            userData.SetEuclideanScore(score);
                            break;

                        case var a when a == typeof(ManhattanScore):
                            userData.SetManhattanScore(score);
                            break;
                    }
                }

                _recommendationUserData.Add(userData);
            }
        }

        /// <summary>
        /// Method to get best 5 users for selected user.
        /// </summary>
        /// <param name="algorithm">Representing by which algorithm return best users.</param>
        /// <returns>Collection of best 5 other users.</returns>
        public IEnumerable<RecommendationUserData> GetTop5Users(AlgorithmType algorithm)
        {
            Func<RecommendationUserData, double> func =
                algorithm == AlgorithmType.Euclidean ? ((x) => x.EuclideanScore) : ((x) => x.ManhattanScore);

            return algorithm == AlgorithmType.Euclidean
                ? _recommendationUserData.OrderBy(func).Take(5)
                : _recommendationUserData.OrderByDescending(func).Take(5);
        }

        /// <summary>
        /// Method to get worst 5 users for selected user.
        /// </summary>
        /// <param name="algorithm">Representing by which algorithm return worst users.</param>
        /// <returns>Collection of worst 5 other users.</returns>
        public IEnumerable<RecommendationUserData> GetWorst5Users(AlgorithmType algorithm)
        {
            Func<RecommendationUserData, double> func =
                algorithm == AlgorithmType.Euclidean ? ((x) => x.EuclideanScore) : ((x) => x.ManhattanScore);

            return algorithm == AlgorithmType.Euclidean
                ? _recommendationUserData.OrderByDescending(func).Take(5)
                : _recommendationUserData.OrderBy(func).Take(5);
        }

        /// <summary>
        /// Method to analyze each database user rated movies to selected user.
        /// </summary>
        /// <param name="userData">Object of other user with analyzed data.</param>
        /// <param name="otherUser">User to analyze his movies positions.</param>
        private void analyzeMovies(RecommendationUserData userData, User otherUser)
        {
            foreach (Movie otherMovie in otherUser.Movie)
            {
                if (_currentUser.Movie.Any(x => x.Title.Equals(otherMovie.Title, StringComparison.OrdinalIgnoreCase)))
                {
                    userData.EqualsMovies.Add(otherMovie);
                    continue;
                }

                userData.DifferentMovies.Add(otherMovie);
            }
        }

        /// <summary>
        /// Method to get list of users without selected user.
        /// </summary>
        /// <param name="selectedUser">User selected from Main menu.</param>
        /// <param name="allUsers">Collection of all users in database.</param>
        /// <returns>Collection of all users expect selected user.</returns>
        private IEnumerable<User> getUsersWithoutSelected(User selectedUser, IEnumerable<User> allUsers)
            => allUsers.Where(x => x != selectedUser);

        /// <summary>
        /// Method to get list of all score algorithms.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IComputeScore> getComputeScoreList()
            => new List<IComputeScore>() { new EuclideanScore(), new ManhattanScore() };
    }
}
