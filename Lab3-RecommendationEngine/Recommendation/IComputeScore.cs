using Lab3_RecommendationEngine.Database;

namespace Lab3_RecommendationEngine.Recommendation
{
    public interface IComputeScore
    {
        public double CalculateScore(RecommendationUserData userData, User currentUser);
    }
}
