using System.Collections.Generic;

namespace Lab3_RecommendationEngine.TheMovieDB
{
    /// <summary>
    /// Model representing return by external API movie infos.
    /// </summary>
    public class TMDBMovie
    {
        /// <summary>
        /// Name of return entry.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Description of return entry.
        /// </summary>
        public string overview { get; set; }

        /// <summary>
        /// Title of return entry.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Media tyle of return entry.
        /// </summary>
        public string media_type { get; set; }
    }

    /// <summary>
    /// Model representing collection of TV series, Movies and Actors return from external API.
    /// </summary>
    public class TMDBMovies
    {
        /// <summary>
        /// Collection of TV series, Movies and Actors return from external API.
        /// </summary>
        public List<TMDBMovie> results { get; set; }
    }
}
