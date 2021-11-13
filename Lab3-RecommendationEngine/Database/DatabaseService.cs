using System.Collections.Generic;

namespace Lab3_RecommendationEngine.Database
{
    public class DatabaseService
    {
        private IEnumerable<User> _users = null;

        private readonly DatabaseXmlParser _databaseXmlParser;

        public DatabaseService()
        {
            _databaseXmlParser = new DatabaseXmlParser();
        }

        public IEnumerable<User> GetUsers()
        {
            if (_users == null)
            {
                _users = _databaseXmlParser.Parse();
            }

            return _users;
        }
    }
}
