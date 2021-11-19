using Lab3_RecommendationEngine.TheMovieDB;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_RecommendationEngine.REST
{
    /// <summary>
    /// Class to request from external API info about searching TV series or movie.
    /// </summary>
    public class TheMovieDBApiService
    {
        /// <summary>
        /// Field representing link to external API.
        /// </summary>
        private const string BASE_URL = @"https://api.themoviedb.org/3/";

        /// <summary>
        /// Field representing API key to external API.
        /// </summary>
        private const string API_KEY = @"api_key=957d8e820a52252469f1d3b08fc2f59c";

        /// <summary>
        /// Field representing request command to external API.
        /// </summary>
        private const string PARAMETERS = @"search/multi?";

        /// <summary>
        /// Field representing instance of IRestClient interface.
        /// </summary>
        private readonly IRestClient _client;

        /// <summary>
        /// Default constructor with initializing field.
        /// </summary>
        public TheMovieDBApiService()
        {
            _client = new RestClient(BASE_URL + PARAMETERS + API_KEY);
        }

        /// <summary>
        /// Method to request info about any movie and parse it to expected model.
        /// </summary>
        /// <param name="movieName">Name of search movie or tv series.</param>
        /// <returns>Object representing found element returns by external API.</returns>
        public TMDBMovie Execute(string movieName)
        {
            IRestRequest request = new RestRequest();
            request.AddParameter("query", movieName);
            IRestResponse response = _client.Execute(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response. Check inner details for more information";
                throw new Exception(message, response.ErrorException);
            }

            TMDBMovies tmdbMovies = new JsonDeserializer().Deserialize<TMDBMovies>(response);
            IEnumerable<TMDBMovie> foundMovies = tmdbMovies.results.Where(x => isMediaTypeValid(x) && isNameOrTitleValid(x));
            TMDBMovie movie = foundMovies.FirstOrDefault(x => isNameOrTitleEqualsExpectedName(x, movieName));

            if (movie == null || movie.overview == null)
            {
                Console.WriteLine("Unable to find description of selected movie in external API");
            }

            return movie;
        }

        /// <summary>
        /// Method to check if object media type is as expected.
        /// </summary>
        /// <param name="movie">Object representing movie or tv series.</param>
        /// <returns>Boolean if media type is as expected.</returns>
        private bool isMediaTypeValid(TMDBMovie movie)
            => movie.media_type.Contains("movie") || movie.media_type.Contains("tv");

        /// <summary>
        /// Method to check if object title is as expected.
        /// </summary>
        /// <param name="movie">Object representing movie or tv series.</param>
        /// <returns>Boolean if title is as expected.</returns>
        private bool isNameOrTitleValid(TMDBMovie movie)
            => movie.name != null || movie.title != null;

        /// <summary>
        /// Method to check if object name or title is as expected.
        /// </summary>
        /// <param name="movie">Object representing movie or tv series.</param>
        /// <param name="expectedName">Expected object name or title.</param>
        /// <returns>Boolean if title or name is as expected.</returns>
        private bool isNameOrTitleEqualsExpectedName(TMDBMovie movie, string expectedName)
        {
            if (movie.name != null && movie.name.Equals(expectedName, StringComparison.OrdinalIgnoreCase) && movie.overview.Length > 0)
            {
                return true;
            }

            if (movie.title != null && movie.title.Equals(expectedName, StringComparison.OrdinalIgnoreCase) && movie.overview.Length > 0)
            {
                return true;
            }

            return false;
        }
    }
}
