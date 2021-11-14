using System.Collections.Generic;

namespace Lab3_RecommendationEngine.TheMovieDB
{
    public class TMDBMovie
    {
        public string overview { get; set; }
        public string title { get; set; }
    }

    public class TMDBMovies
    {
        public List<TMDBMovie> results { get; set; }
    }
}
