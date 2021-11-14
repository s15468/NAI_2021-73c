using Lab3_RecommendationEngine.TheMovieDB;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_RecommendationEngine.REST
{
    public class TheMovieDBApiService
    {
        private const string BASE_URL = @"https://api.themoviedb.org/3/";
        private const string API_KEY = @"api_key=957d8e820a52252469f1d3b08fc2f59c";
        private const string PARAMETERS = @"search/multi?";
        
        private readonly IRestClient _client;

        public TheMovieDBApiService()
        {
            _client = new RestClient(BASE_URL + PARAMETERS + API_KEY);
        }

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

        private bool isMediaTypeValid(TMDBMovie movie)
            => movie.media_type.Contains("movie") || movie.media_type.Contains("tv");

        private bool isNameOrTitleValid(TMDBMovie movie)
            => movie.name != null || movie.title != null;

        private bool isNameOrTitleEqualsExpectedName(TMDBMovie movie, string expectedName)
        {
            if (movie.name != null && movie.name.Equals(expectedName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (movie.title != null && movie.title.Equals(expectedName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
