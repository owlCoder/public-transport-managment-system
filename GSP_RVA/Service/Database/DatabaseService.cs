using Service.Database.Context;
using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Service.Database
{
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
    }
}
