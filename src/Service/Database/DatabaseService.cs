using Service.Database.Context;

namespace Service.Database
{
    /// <summary>
    /// Represents a singleton service for accessing the database context.
    /// </summary>
    public class DatabaseService
    {
        private static DatabaseService _instance;
        private static readonly object _lock = new object();
        private DatabaseContext _context;

        private DatabaseService()
        {
            // Private constructor to prevent instantiation outside the class
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Gets the singleton instance of the DatabaseService.
        /// </summary>
        public static DatabaseService Instance
        {
            get
            {
                lock (_lock)
                {
                    // Lazy initialization: create the instance if it's null
                    if (_instance == null)
                    {
                        _instance = new DatabaseService();
                    }
                    return _instance;
                }
            }
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        public DatabaseContext Context { get => _context; }
    }
}
