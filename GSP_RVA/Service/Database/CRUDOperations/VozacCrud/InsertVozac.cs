using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;

namespace Service.Database.CRUDOperations.VozacCrud
{
    /// <summary>
    /// Represents an implementation of the <see cref="ICreateOperation{Vozac}"/> interface
    /// for performing insert operations on <see cref="Vozac"/> entities.
    /// </summary>
    public class InsertVozac : ICreateOperation<Vozac>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertVozac"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to use for inserting <see cref="Vozac"/> entities.</param>
        public InsertVozac(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserts the specified <see cref="Vozac"/> entity into the database.
        /// </summary>
        /// <param name="vozac">The <see cref="Vozac"/> entity to be inserted.</param>
        /// <returns>
        /// <c>true</c> if the insert operation was successful; otherwise, <c>false</c>.
        /// </returns>
        public bool Insert(Vozac vozac)
        {
            // Use a lock to ensure thread safety
            lock (_lock)
            {
                _context.Vozaci.Add(vozac);
                return _context.SaveChanges() > 0;
            }
        }
    }
}