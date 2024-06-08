using System;
using System.Collections.Generic;

namespace Service.Database.Operations
{
    /// <summary>
    /// Interface for database operations to read entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity to read.</typeparam>
    public interface IReadOperation<T> where T : class
    {
        /// <summary>
        /// Reads the entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity to read.</param>
        /// <returns>The entity with the specified ID.</returns>
        T Read(int id);

        /// <summary>
        /// Reads all entities of type T.
        /// </summary>
        /// <returns>A list of all entities of type T.</returns>
        List<T> ReadAll();

        /// <summary>
        /// Reads the entity that satisfies the specified criteria.
        /// </summary>
        /// <param name="filter">The criteria to filter entities by.</param>
        /// <returns>The entity that satisfies the criteria.</returns>
        T ReadByCriteria(Func<T, bool> filter);

        /// <summary>
        /// Reads all entities that satisfy the specified criteria.
        /// </summary>
        /// <param name="filter">The criteria to filter entities by.</param>
        /// <returns>A list of entities that satisfy the criteria.</returns>
        List<T> ReadAllByCriteria(Func<T, bool> filter);
    }
}
