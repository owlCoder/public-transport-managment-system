using Service.Database.Context;

namespace Service.Database
{
    /// <summary>
    /// Provides a singleton instance of the <see cref="DatabaseContext"/> class.
    /// </summary>
    public class DatabaseService
    {
        /// <summary>
        /// A private static instance of the <see cref="DatabaseService"/> class.
        /// </summary>
        private static DatabaseService _instance;

        /// <summary>
        /// A private object used to synchronize access to the <see cref="_instance"/> field.
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// The <see cref="DatabaseContext"/> instance associated with this service.
        /// </summary>
        private DatabaseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseService"/> class.
        /// </summary>
        private DatabaseService()
        {
            // Private constructor to prevent instantiation outside the class
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Gets the singleton instance of the <see cref="DatabaseService"/> class.
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
        /// Gets the <see cref="DatabaseContext"/> instance associated with this service.
        /// </summary>
        public DatabaseContext Context { get => _context; }
    }
}