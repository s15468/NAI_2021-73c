using Lab3_RecommendationEngine.Database;
using Lab3_RecommendationEngine.Recommendation;
using Lab3_RecommendationEngine.REST;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_RecommendationEngine
{
    public class MenuService
    {
        private readonly RenderService _renderService;
        private readonly TheMovieDBApiService _tMDBApiService;
        private RecommendationService _recommendationService;

        public MenuService()
        {
            _renderService = new RenderService();
            _tMDBApiService = new TheMovieDBApiService();
        }

        public void MainMenu()
        {
            IEnumerable<User> allUsers = new DatabaseService().GetUsers();
            User currentUser = selectUserToRecommendMovies(allUsers);

            _renderService.ClearConsole();

            _recommendationService = new RecommendationService(currentUser, allUsers);
            _recommendationService.CalculateScore();

            do
            {
                (Option option, AlgorithmType data) selectedAlgorithm = selectRecommendationAlgorithm();

                if (selectedAlgorithm.option == Option.ExitApp)
                {
                    Environment.Exit(1);
                }

                renderSelectedAlgorithmBestAndWorseUsers(selectedAlgorithm.data);
                (Option option, RecommendationType data) selectedRecommendation = selectBestOrWorstRecommendation();

                switch (selectedRecommendation)
                {
                    case (Option, RecommendationType) selected when selected.option == Option.ExitApp:
                        Environment.Exit(1);
                        break;

                    case (Option, RecommendationType) selected when selected.option == Option.Return:
                        continue;

                    case (Option, RecommendationType) selected when selected.option == Option.Action:
                        recommendationMenu(selectedAlgorithm.data, selected.data);
                        break;
                }
            }
            while (true);
        }

        private (Option option, RecommendationType data) selectBestOrWorstRecommendation()
        {
            do
            {
                _renderService.SelectBestOrWorstRecommendation();

                string? input = Console.ReadLine();
                bool status = int.TryParse(input, out int index);

                if (!status)
                {
                    wrongUserInputMessage();
                    continue;
                }

                switch (index)
                {
                    case 0:
                        return (Option.Action, RecommendationType.Best);

                    case 1:
                        return (Option.Action, RecommendationType.Worst);

                    case 8:
                        return (Option.Return, RecommendationType.None);

                    case 9:
                        return (Option.ExitApp, RecommendationType.None);

                    default:
                        wrongUserInputMessage();
                        continue;
                }
            }
            while (true);
        }

        private User selectUserToRecommendMovies(IEnumerable<User> users)
        {
            do
            {
                _renderService.RenderUsers(users);
                _renderService.SelectUserMessage();

                string? input = Console.ReadLine();
                bool status = int.TryParse(input, out int index);

                if (!status)
                {
                    wrongUserInputMessage();
                    continue;
                }

                switch (index)
                {
                    case int i when i >= 0 && i < users.Count():
                        return users.ElementAt(index);

                    default:
                        wrongUserInputMessage();
                        continue;
                }
            }
            while (true);
        }

        private (Option option, AlgorithmType data) selectRecommendationAlgorithm()
        {
            do
            {
                _renderService.SelectRecommendationAlgorithm(getAlgorithmTypes());

                string? input = Console.ReadLine();
                bool status = int.TryParse(input, out int index);

                _renderService.ClearConsole();

                if (!status)
                {
                    wrongUserInputMessage();
                    continue;
                }



                switch (index)
                {
                    case int i when i >= 0 && i < getAlgorithmTypes().Count():
                        return (Option.Action, getAlgorithmTypes().ElementAt(i));

                    case int i when i == 9:
                        return (Option.ExitApp, AlgorithmType.None);

                    default:
                        wrongUserInputMessage();
                        continue;
                }
            }
            while (true);
        }

        private void wrongUserInputMessage()
        {
            _renderService.RenderCustomMessage("Write incorrect index. Press anything to start again.");
            Console.ReadKey();
            _renderService.ClearConsole();
        }

        private void renderSelectedAlgorithmBestAndWorseUsers(AlgorithmType algorithmType)
        {
            switch (algorithmType)
            {
                case AlgorithmType.Euclidean:
                    renderEuclideanBestAndWorstUsers();
                    return;

                case AlgorithmType.Manhattan:
                    renderManhattanBestAndWorstUsers();
                    return;

                default:
                    throw new NotImplementedException();
            }
        }
        
        private void renderEuclideanBestAndWorstUsers()
        {
            IEnumerable<RecommendationUserData> top5EuclideanUsers = _recommendationService.GetTop5Users(AlgorithmType.Euclidean);
            IEnumerable<RecommendationUserData> worst5EuclideanUsers = _recommendationService.GetWorst5Users(AlgorithmType.Euclidean);

            _renderService.RenderScoreUsers(top5EuclideanUsers, AlgorithmType.Euclidean, RecommendationType.Best);
            _renderService.RenderScoreUsers(worst5EuclideanUsers, AlgorithmType.Euclidean, RecommendationType.Worst);
        }

        private void renderManhattanBestAndWorstUsers()
        {
            IEnumerable<RecommendationUserData> top5ManhattanUsers = _recommendationService.GetTop5Users(AlgorithmType.Manhattan);
            IEnumerable<RecommendationUserData> worst5ManhattanUsers = _recommendationService.GetWorst5Users(AlgorithmType.Manhattan);

            _renderService.RenderScoreUsers(top5ManhattanUsers, AlgorithmType.Manhattan, RecommendationType.Best);
            _renderService.RenderScoreUsers(worst5ManhattanUsers, AlgorithmType.Manhattan, RecommendationType.Worst);
        }

        private void renderSelectedRecommendation(AlgorithmType algorithmType, RecommendationType recommendationType)
        {
            switch (recommendationType)
            {
                case RecommendationType.Best:
                    IEnumerable<RecommendationUserData> top5Users = _recommendationService.GetTop5Users(algorithmType);
                    _renderService.RenderScoreUsers(top5Users, algorithmType, recommendationType);
                    break;
                case RecommendationType.Worst:
                    IEnumerable<RecommendationUserData> worst5users = _recommendationService.GetWorst5Users(algorithmType);
                    _renderService.RenderScoreUsers(worst5users, algorithmType, recommendationType);
                    break;
            }
        }

        private void recommendationMenu(AlgorithmType algorithmType, RecommendationType recommendationType)
        {

            renderSelectedRecommendation(algorithmType, recommendationType);

            IEnumerable<RecommendationUserData> usersForSelectedOptions = getUsersForSelectedOptions((algorithmType, recommendationType));
            IEnumerable<Movie> recommendedMovies = getRecommendedMovies(usersForSelectedOptions, recommendationType);

            do
            {
                switch (selectMovieToPrintDescription(recommendedMovies))
                {
                    case var select when select.option == Option.Action:
                        _renderService.RenderMovieDescription(_tMDBApiService.Execute(select.data).overview);
                        Console.ReadKey();
                        break;

                    case var select when select.option == Option.Return:
                        return;

                    case var select when select.option == Option.ExitApp:
                        Environment.Exit(1);
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }
            while (true);
        }

        private IEnumerable<RecommendationUserData> getUsersForSelectedOptions((AlgorithmType algorithmType, RecommendationType recommendationType) selectedOptions)
        {
            switch (selectedOptions)
            {
                case (AlgorithmType, RecommendationType) option when option.recommendationType == RecommendationType.Best:
                    return _recommendationService.GetTop5Users(option.algorithmType);

                case (AlgorithmType, RecommendationType) option when option.recommendationType == RecommendationType.Worst:
                    return _recommendationService.GetWorst5Users(option.algorithmType);

                default:
                    throw new NotImplementedException();
            }
        }

        private IEnumerable<Movie> getRecommendedMovies(IEnumerable<RecommendationUserData> usersForSelectedOptions, RecommendationType recommendationType)
        {
            List<Movie> recommendedMovies = new();

            foreach (var user in usersForSelectedOptions)
            {
                recommendedMovies.AddRange(user.DifferentMovies);
            }

            recommendedMovies = recommendedMovies.Distinct().ToList();

            IOrderedEnumerable<Movie> sortedMovies =
                recommendationType == RecommendationType.Best
                ? recommendedMovies.OrderByDescending(x => x.Rating)
                : recommendedMovies.OrderBy(x => x.Rating);

            return sortedMovies.Count() >= 5 ? sortedMovies.Take(5) : sortedMovies.Take(sortedMovies.Count());
        }

        private (Option option, string data) selectMovieToPrintDescription(IEnumerable<Movie> moviesList)
        {
            do
            {
                _renderService.SelectMovieToPrintDescription(moviesList);

                string? input = Console.ReadLine();
                bool status = int.TryParse(input, out int index);

                if (!status)
                {
                    wrongUserInputMessage();
                    continue;
                }

                switch (index)
                {
                    case var i when i >= 0 && i < moviesList.Count():
                        return (Option.Action, moviesList.ElementAt(index).Title);

                    case var i when i == 8:
                        return (Option.Return, null);

                    case var i when i == 9:
                        return (Option.ExitApp, null);

                    default:
                        wrongUserInputMessage();
                        continue;
                }
            }
            while (true);
        }

        private IEnumerable<AlgorithmType> getAlgorithmTypes()
            => new List<AlgorithmType>() { AlgorithmType.Euclidean, AlgorithmType.Manhattan };
    }
}
