using Lab3_RecommendationEngine.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_RecommendationEngine.Recommendation
{
    public class RecommendationService
    {
        private User _currentUser;
        private IEnumerable<User> _otherUsers;
        private List<RecommendationUserData> _recommendationUserData;

        public RecommendationService(User currentUser, IEnumerable<User> allUsers)
        {
            _recommendationUserData = new();
            _currentUser = currentUser;
            _otherUsers = getUsersWithoutSelected(currentUser, allUsers);
        }

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

        private IEnumerable<IComputeScore> getComputeScoreList()
            => new List<IComputeScore>() { new EuclideanScore(), new ManhattanScore() };

        public IEnumerable<RecommendationUserData> GetTop5Users(AlgorithmType algorithm)
        {
            Func<RecommendationUserData, double> func =
                algorithm == AlgorithmType.Euclidean ? ((x) => x.EuclideanScore) : ((x) => x.ManhattanScore);

            return algorithm == AlgorithmType.Euclidean
                ? _recommendationUserData.OrderBy(func).Take(5)
                : _recommendationUserData.OrderByDescending(func).Take(5);
        }

        public IEnumerable<RecommendationUserData> GetWorst5Users(AlgorithmType algorithm)
        {
            Func<RecommendationUserData, double> func =
                algorithm == AlgorithmType.Euclidean ? ((x) => x.EuclideanScore) : ((x) => x.ManhattanScore);

            return algorithm == AlgorithmType.Euclidean
                ? _recommendationUserData.OrderByDescending(func).Take(5)
                : _recommendationUserData.OrderBy(func).Take(5);
        }

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

        private IEnumerable<User> getUsersWithoutSelected(User selectedUser, IEnumerable<User> allUsers)
            => allUsers.Where(x => x != selectedUser);
    }
}
