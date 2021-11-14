using System.Collections.Generic;

namespace Lab3_RecommendationEngine.TheMovieDB
{
    public class TMDBMovie
    {
        public string name { get; set; }
        public string overview { get; set; }
        public string title { get; set; }
        public string media_type { get; set; }
    }

    public class TMDBMovies
    {
        public List<TMDBMovie> results { get; set; }
    }
}
