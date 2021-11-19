using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Lab3_RecommendationEngine.Database
{
    /// <summary>
    /// Class which is use to parse XML database to Database object.
    /// </summary>
    public class DatabaseXmlParser
    {
        /// <summary>
        /// Method open XML database and parse it to special object.
        /// </summary>
        /// <returns>Collection of users from database.</returns>
        public IEnumerable<User> Parse()
        {
            Database data;
            XmlSerializer serializer = new XmlSerializer(typeof(Database));

            using (FileStream stream = File.OpenRead(@"Database\Database.xml"))
            {
                data = (Database)serializer.Deserialize(stream);
            }

            return data.Users;
        }
    }
}
