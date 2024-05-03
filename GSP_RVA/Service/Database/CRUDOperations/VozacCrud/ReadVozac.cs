using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;
using System.Collections.Generic;
using System.Linq;

namespace Service.Database.CRUDOperations.VozacCrud
{
    /// <summary>
    /// Represents an implementation of the <see cref="IReadOperation{Vozac}"/> interface
    /// for performing read operations on <see cref="Vozac"/> entities.
    /// </summary>
    public class ReadVozac : IReadOperation<Vozac>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadVozac"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to use for reading <see cref="Vozac"/> entities.</param>
        public ReadVozac(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves the <see cref="Vozac"/> entity with the specified identifier from the database.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="Vozac"/> entity to retrieve.</param>
        /// <returns>The <see cref="Vozac"/> entity with the specified identifier, or null if not found.</returns>
        public Vozac Read(int id)
        {
            // Use a lock to ensure thread safety
            lock (_lock)
            {
                return _context.Vozaci.AsNoTracking().FirstOrDefault(u => u.Id == id);
            }
        }

        /// <summary>
        /// Retrieves all <see cref="Vozac"/> entities from the database.
        /// </summary>
        /// <returns>A list of all <see cref="Vozac"/> entities.</returns>
        public List<Vozac> ReadAll()
        {
            // Use a lock to ensure thread safety
            lock (_lock)
            {
                return _context.Vozaci.AsNoTracking().ToList();
            }
        }
    }
}