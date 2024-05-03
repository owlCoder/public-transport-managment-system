using Service.Database.CRUD;
using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Database.CRUDOperations.LinijaCrud.FindLinija
{
    /// <summary>
    /// Represents an implementation of the <see cref="IFindOperation{Linija}"/> interface
    /// for finding <see cref="Linija"/> entities based on the starting point (Polaziste).
    /// </summary>
    public class FindByPolaziste : IFindOperation<Linija>
    {
        /// <summary>
        /// Finds <see cref="Linija"/> entities in the specified list based on the starting point criteria.
        /// </summary>
        /// <param name="linije">The list of <see cref="Linija"/> entities to search.</param>
        /// <param name="criteria">The starting point criteria to match.</param>
        /// <returns>A list of <see cref="Linija"/> entities that match the specified starting point criteria.</returns>
        public List<Linija> FindByCriteria(List<Linija> linije, string criteria)
        {
            // Find lines in the provided list where the Polaziste property contains the criteria
            // throw new NotImplementedException();

            // TODO: Implement the logic to filter the list based on the starting point criteria
            // Example:
            // return linije.Where(l => l.Polaziste.Contains(criteria)).ToList();
            throw new NotImplementedException();
        }
    }
}