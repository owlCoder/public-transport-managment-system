using Common.Enums;
using Common.Interfaces;
using Service.Database.Context;
using Service.Database.Models;
using System;
using System.Linq;

namespace Service.Database.CRUDOperations.LinijaCrud
{
    /// <summary>
    /// Implements delete operations for the Linija entity.
    /// </summary>
    public class DeleteLinija
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();
        private static ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the DeleteLinija class with the specified DatabaseContext.
        /// </summary>
        /// <param name="context">The DatabaseContext instance to use for database operations.</param>
        /// <param name="logger">The ILogger instance to use for logging.</param>
        public DeleteLinija(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = Program.logger;
        }

        /// <summary>
        /// Deletes the specified Linija entity from the database.
        /// </summary>
        /// <param name="linija">The Linija entity to delete.</param>
        /// <returns>True if the entity is successfully deleted; otherwise, false.</returns>
        public bool Delete(Linija linija)
        {
            try
            {
                lock (_lock)
                {
                    _context.Linije.Remove(linija);
                    return _context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error deleting Linija: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Deletes the Linija entity with the specified ID from the database.
        /// </summary>
        /// <param name="id">The ID of the Linija entity to delete.</param>
        /// <returns>True if the entity with the specified ID is successfully deleted; otherwise, false.</returns>
        public bool Delete(int id)
        {
            try
            {
                lock (_lock)
                {
                    Linija find = _context.Linije.Find(id);
                    if (find == null)
                        return false;

                    _context.Linije.Remove(find);

                    var za_brisanje = _context.VozaciLinije.Where(vl => vl.LinijaID == find.Id).ToList();

                    // Remove from the connecting table
                    _context.VozaciLinije.RemoveRange(za_brisanje);

                    // Release buses because the line is deleted
                    var za_oslobodjene = _context.Autobusi.Where(a => a.IdLinije == find.Id && a.IdLinije != 0);

                    foreach (var bus in za_oslobodjene)
                    {
                        bus.IdLinije = 0; // Remove line
                    }
                    return _context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error deleting Linija with ID {id}: {ex.Message}");
                return false;
            }
        }
    }
}
