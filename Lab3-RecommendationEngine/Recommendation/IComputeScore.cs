using Lab3_RecommendationEngine.Database;

namespace Lab3_RecommendationEngine.Recommendation
{
    /// <summary>
    /// Interface to implement ICompueScore methods for each algorithm
    /// </summary>
    public interface IComputeScore
    {
        /// <summary>
        /// Method to calculate score for selected user and single user from database.
        /// </summary>
        /// <param name="userData">Single user from database other than selected.</param>
        /// <param name="currentUser">Selected user from database.</param>
        /// <returns>Score of single user from database.</returns>
        public double CalculateScore(RecommendationUserData userData, User currentUser);
    }
}
