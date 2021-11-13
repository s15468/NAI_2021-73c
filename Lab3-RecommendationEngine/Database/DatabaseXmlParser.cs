using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Lab3_RecommendationEngine.Database
{
    public class DatabaseXmlParser
    {
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
