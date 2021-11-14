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
        private const string PARAMETERS = @"search/movie?";
        
        private readonly IRestClient _client;

        public TheMovieDBApiService()
        {
            _client = new RestClient(BASE_URL + PARAMETERS + API_KEY);
        }

        public string Execute(string movieName)
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

            TMDBMovie movie = tmdbMovies.results.FirstOrDefault(x => x.title.Equals(movieName, StringComparison.OrdinalIgnoreCase));

            if (movie == null)
            {
                return "Unable to find description of selected movie in external API";
            }

            return movie.overview;
        }
    }
}
