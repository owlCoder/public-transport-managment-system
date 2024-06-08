using Common.Enums;
using Common.Interfaces;
using Service.Database.Context;
using Service.Database.CRUD;
using Service.Database.Models;
using System;

namespace Service.Database.CRUDOperations.AutobusCrud
{
    /// <summary>
    /// Implements update operation for the Autobus entity.
    /// </summary>
    public class UpdateAutobus : IUpdateOperation<Autobus>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();
        private static ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the UpdateAutobus class with the specified DatabaseContext.
        /// </summary>
        /// <param name="context">The DatabaseContext instance to use for database operations.</param>
        /// <param name="logger">The ILogger instance to use for logging.</param>
        public UpdateAutobus(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = Program.logger;
        }

        /// <inheritdoc/>
        public bool Update(int id, Autobus autobus)
        {
            try
            {
                lock (_lock)
                {
                    Autobus find = _context.Autobusi.Find(id);
                    if (find == null)
                        return false;

                    // Update fields
                    find.Oznaka = autobus.Oznaka;
                    find.IdLinije = autobus.IdLinije;

                    return _context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error updating Autobus with ID {id}: {ex.Message}");
                return false;
            }
        }
    }
}
