using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lab3_RecommendationEngine.Database
{
    [XmlRoot]
    public class Database
    {
        [XmlElement(ElementName = "User")]
        public List<User> Users { get; set; }
    }

    [XmlRoot]
    public class User
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Movie")]
        public List<Movie> Movie { get; set; }
    }

    [XmlRoot]
    public class Movie
    {
        [XmlAttribute(AttributeName = "Title")]
        public string Title { get; set; }

        [XmlAttribute(AttributeName = "Rate")]
        public int Rating { get; set; }
    }

}
