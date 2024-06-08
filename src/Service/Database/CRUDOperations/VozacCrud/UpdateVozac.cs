using Common.Enums;
using Common.Interfaces;
using Service.Database.Context;
using Service.Database.CRUD;
using Service.Database.Models;
using System;

namespace Service.Database.CRUDOperations.VozacCrud
{
    /// <summary>
    /// Implements update operation for the Vozac entity.
    /// </summary>
    public class UpdateVozac : IUpdateOperation<Vozac>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();
        private static ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the UpdateVozac class with the specified DatabaseContext.
        /// </summary>
        /// <param name="context">The DatabaseContext instance to use for database operations.</param>
        /// <param name="logger">The ILogger instance to use for logging.</param>
        public UpdateVozac(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = Program.logger;
        }

        /// <inheritdoc/>
        public bool Update(int id, Vozac vozac)
        {
            try
            {
                // Use a lock to ensure thread safety
                lock (_lock)
                {
                    Vozac find = _context.Vozaci.Find(id);
                    if (find == null)
                        return false;

                    // Update fields
                    find.Ime = vozac.Ime;
                    find.Prezime = vozac.Prezime;
                    find.Oznaka = vozac.Oznaka;

                    return _context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error updating Vozac with ID {id}: {ex.Message}");
                return false;
            }
        }
    }
}
