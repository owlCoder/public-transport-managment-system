using Service.Database.CRUD;
using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Database.CRUDOperations.LinijaCrud.FindLinija
{
    /// <summary>
    /// Implements find operation for the Linija entity by destination.
    /// </summary>
    public class FindByOdrediste : IFindOperation<Linija>
    {
        /// <inheritdoc/>
        public List<Linija> FindByCriteria(List<Linija> linije, string criteria)
        {
            try
            {
                if (linije == null)
                    throw new ArgumentNullException(nameof(linije));

                if (string.IsNullOrWhiteSpace(criteria))
                    throw new ArgumentException("Criteria cannot be null or empty.", nameof(criteria));

                return linije.Where(l => l.Odrediste.Contains(criteria)).ToList();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error finding Linija by destination: {ex.Message}");
                return new List<Linija>();
            }
        }
    }
}
