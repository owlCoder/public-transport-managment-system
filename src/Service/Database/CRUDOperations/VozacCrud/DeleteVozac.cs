using Common.Enums;
using Common.Interfaces;
using Service.Database.Context;
using Service.Database.Models;
using System;

namespace Service.Database.CRUDOperations.VozacCrud
{
    /// <summary>
    /// Implements delete operations for the Vozac entity.
    /// </summary>
    public class DeleteVozac
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();
        private static ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the DeleteVozac class with the specified DatabaseContext.
        /// </summary>
        /// <param name="context">The DatabaseContext instance to use for database operations.</param>
        /// <param name="logger">The ILogger instance to use for logging.</param>
        public DeleteVozac(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = Program.logger;
        }

        /// <summary>
        /// Deletes the specified Vozac entity from the database.
        /// </summary>
        /// <param name="vozac">The Vozac entity to delete.</param>
        /// <returns>True if the entity is successfully deleted; otherwise, false.</returns>
        public bool Delete(Vozac vozac)
        {
            try
            {
                // Use a lock to ensure thread safety
                lock (_lock)
                {
                    _context.Vozaci.Remove(vozac);
                    return _context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error deleting Vozac: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Deletes the Vozac entity with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the Vozac entity to delete.</param>
        /// <returns>True if the entity with the specified ID is successfully deleted; otherwise, false.</returns>
        public bool Delete(int id)
        {
            try
            {
                // Use a lock to ensure thread safety
                lock (_lock)
                {
                    Vozac find = _context.Vozaci.Find(id);
                    if (find == null)
                        return false;

                    _context.Vozaci.Remove(find);
                    return _context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error deleting Vozac with ID {id}: {ex.Message}");
                return false;
            }
        }
    }
}
