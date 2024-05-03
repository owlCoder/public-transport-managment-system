using Service.Database.Context;
using Service.Database.Models;

namespace Service.Database.CRUDOperations.VozacCrud
{
    /// <summary>
    /// Represents a class for performing delete operations on <see cref="Vozac"/> entities.
    /// </summary>
    public class DeleteVozac
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteVozac"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to use for deleting <see cref="Vozac"/> entities.</param>
        public DeleteVozac(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Deletes the specified <see cref="Vozac"/> entity from the database.
        /// </summary>
        /// <param name="vozac">The <see cref="Vozac"/> entity to be deleted.</param>
        /// <returns>
        /// <c>true</c> if the delete operation was successful; otherwise, <c>false</c>.
        /// </returns>
        public bool Delete(Vozac vozac)
        {
            // Use a lock to ensure thread safety
            lock (_lock)
            {
                _context.Vozaci.Remove(vozac);
                return _context.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Deletes the <see cref="Vozac"/> entity with the specified identifier from the database.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="Vozac"/> entity to be deleted.</param>
        /// <returns>
        /// <c>true</c> if the delete operation was successful; otherwise, <c>false</c>.
        /// </returns>
        public bool Delete(int id)
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
    }
}