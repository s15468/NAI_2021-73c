using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lab3_RecommendationEngine.Database
{
    /// <summary>
    /// Main node of XML database.
    /// </summary>
    [XmlRoot]
    public class Database
    {
        /// <summary>
        /// XML node representing collection of users in database.
        /// </summary>
        [XmlElement(ElementName = "User")]
        public List<User> Users { get; set; }
    }

    /// <summary>
    /// Sub-node of XML database representing single user in database.
    /// </summary>
    [XmlRoot]
    public class User
    {
        /// <summary>
        /// Sub-node attribute of XML database representing user name.
        /// </summary>
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Sub-node attribute of XML database representing user list of rated movies.
        /// </summary>
        [XmlElement(ElementName = "Movie")]
        public List<Movie> Movie { get; set; }
    }

    /// <summary>
    /// Sub-node of XML database representing single movie in database.
    /// </summary>
    [XmlRoot]
    public class Movie
    {
        /// <summary>
        /// Sub-node attribute of XML database representing movie title.
        /// </summary>
        [XmlAttribute(AttributeName = "Title")]
        public string Title { get; set; }

        /// <summary>
        /// Sub-node attribute of XML database representing movie rating given by user.
        /// </summary>
        [XmlAttribute(AttributeName = "Rating")]
        public int Rating { get; set; }
    }
}
