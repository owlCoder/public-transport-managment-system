using Common.Enums;
using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Database.CRUD
{
    /// <summary>
    /// Defines the contract for find operations on entities of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of entity to perform the find operation on.</typeparam>
    public interface IFindOperation<T> where T : class
    {
        /// <summary>
        /// Finds entities of type <typeparamref name="T"/> based on the specified criteria and list of lines.
        /// </summary>
        /// <param name="linije">The list of lines to consider when finding entities.</param>
        /// <param name="criteria">The criteria to use for finding entities.</param>
        /// <returns>A list of entities that match the specified criteria and lines.</returns>
        List<T> FindByCriteria(List<Linija> linije, string criteria);
    }
}