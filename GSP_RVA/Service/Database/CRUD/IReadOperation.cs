using System.Collections.Generic;

namespace Service.Database.Operations
{
    /// <summary>
    /// Defines the contract for read operations on entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of entity to perform the read operation on.</typeparam>
    public interface IReadOperation<T> where T : class
    {
        /// <summary>
        /// Retrieves the entity with the specified identifier from the database.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>The entity with the specified identifier, or null if not found.</returns>
        T Read(int id);

        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/> from the database.
        /// </summary>
        /// <returns>A list of all entities of type <typeparamref name="T"/>.</returns>
        List<T> ReadAll();
    }
}