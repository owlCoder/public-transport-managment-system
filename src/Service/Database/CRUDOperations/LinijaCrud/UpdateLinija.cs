using Common.Enums;
using Common.Interfaces;
using Service.Database.Context;
using Service.Database.CRUD;
using Service.Database.Models;
using System;

namespace Service.Database.CRUDOperations.LinijaCrud
{
    /// <summary>
    /// Implements update operation for the Linija entity.
    /// </summary>
    public class UpdateLinija : IUpdateOperation<Linija>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();
        private static ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the UpdateLinija class with the specified DatabaseContext.
        /// </summary>
        /// <param name="context">The DatabaseContext instance to use for database operations.</param>
        /// <param name="logger">The ILogger instance to use for logging.</param>
        public UpdateLinija(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = Program.logger;
        }

        /// <inheritdoc/>
        public bool Update(int id, Linija linija)
        {
            try
            {
                lock (_lock)
                {
                    Linija find = _context.Linije.Find(id);
                    if (find == null)
                        return false;

                    // Update fields
                    find.Oznaka = linija.Oznaka;
                    find.Polaziste = linija.Polaziste;
                    find.Odrediste = linija.Odrediste;

                    return _context.SaveChanges() > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.Log(LogTraceLevel.ERROR, $"Error updating Linija with ID {id}: {ex.Message}");
                return false;
            }
        }
    }
}
