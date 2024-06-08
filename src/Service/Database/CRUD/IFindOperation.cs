using Service.Database.Models;
using System.Collections.Generic;

namespace Service.Database.CRUD
{
    /// <summary>
    /// Interface for database operations to find entities of type T.
    /// </summary>
    /// <typeparam name="T">The type of entity to find.</typeparam>
    public interface IFindOperation<T> where T : class
    {
        /// <summary>
        /// Finds entities of type T based on specified criteria.
        /// </summary>
        /// <param name="linije">The list of bus lines to search within.</param>
        /// <param name="criteria">The criteria to search for.</param>
        /// <returns>A list of entities matching the criteria.</returns>
        List<T> FindByCriteria(List<Linija> linije, string criteria);
    }
}
