using Service.Database.Context;
using Service.Database.CRUD;
using Service.Database.Models;

namespace Service.Database.CRUDOperations.VozacCrud
{
    /// <summary>
    /// Represents an implementation of the <see cref="IUpdateOperation{Vozac}"/> interface
    /// for performing update operations on <see cref="Vozac"/> entities.
    /// </summary>
    public class UpdateVozac : IUpdateOperation<Vozac>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVozac"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to use for updating <see cref="Vozac"/> entities.</param>
        public UpdateVozac(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Updates the <see cref="Vozac"/> entity with the specified identifier in the database with the provided entity.
        /// </summary>
        /// <param name="id">The identifier of the <see cref="Vozac"/> entity to update.</param>
        /// <param name="vozac">The updated <see cref="Vozac"/> entity to replace the existing one.</param>
        /// <returns>
        /// <c>true</c> if the update operation was successful; otherwise, <c>false</c>.
        /// </returns>
        public bool Update(int id, Vozac vozac)
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
    }
}