using System.Collections.Generic;

namespace Lab3_RecommendationEngine.Database
{
    /// <summary>
    /// Class representing service which parsing XML database.
    /// </summary>
    public class DatabaseService
    {
        /// <summary>
        /// Field of parsed users.
        /// </summary>
        private IEnumerable<User> _users = null;

        /// <summary>
        /// Field for DatabaseXmlParser class instance.
        /// </summary>
        private readonly DatabaseXmlParser _databaseXmlParser;

        /// <summary>
        /// Default constructor with initializing field methods.
        /// </summary>
        public DatabaseService()
        {
            _databaseXmlParser = new DatabaseXmlParser();
        }

        /// <summary>
        /// Method to get users from field; if field is null then invoking XML parser.
        /// </summary>
        /// <returns>Collection of parsed user from database.</returns>
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
