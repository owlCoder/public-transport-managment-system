using Common.Enums;
using Common.Interfaces;
using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;
using System;

namespace Service.Database.CRUDOperations.AutobusCrud
{
    /// <summary>
    /// Implements insert operation for the Autobus entity.
    /// </summary>
    public class InsertAutobus : ICreateOperation<Autobus>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();
        private static ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the InsertAutobus class with the specified DatabaseContext.
        /// </summary>
        /// <param name="context">The DatabaseContext instance to use for database operations.</param>
        /// <param name="logger">The ILogger instance to use for logging.</param>
        public InsertAutobus(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = Program.logger;
        }

        /// <inheritdoc/>
        public bool Insert(Autobus autobus)
        {
            try
            {
                lock (_lock)
                {
                    _context.Autobusi.Add(autobus);
                    return _context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error inserting Autobus: {ex.Message}");
                return false;
            }
        }
    }
}
