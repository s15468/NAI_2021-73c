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
            IEnumerable<IComputeScore> algorithms = new List<IComputeScore>()
            {
                new EuclideanScore(),
            };

            foreach (User otherUser in _otherUsers)
            {
                RecommendationUserData userData = new(otherUser.Name);

                analyzeMovies(userData, otherUser);

                foreach (IComputeScore algorithm in algorithms)
                {
                    double score = algorithm.CalculateScore(userData, _currentUser);

                    if (algorithm.GetType() == typeof(EuclideanScore))
                    {
                        userData.SetEuclideanScore(score);
                    }
                }

                _recommendationUserData.Add(userData);
            }

        }

        private void analyzeMovies(RecommendationUserData userData, User otherUser)
        {
            foreach (Movie otherMovie in otherUser.Movie)
            {
                if (_currentUser.Movie.Any(x => x.Title.Equals(otherMovie.Title, StringComparison.OrdinalIgnoreCase)))
                {
                    userData.EqualsMovies.Add(otherMovie);
                }
                else
                {
                    userData.DifferentMovies.Add(otherMovie);
                }
            }
        }

        private IEnumerable<User> getUsersWithoutSelected(User selectedUser, IEnumerable<User> allUsers)
            => allUsers.Where(x => x != selectedUser);
    }
}
