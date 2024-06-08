using Service.Database.CRUD;
using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Database.CRUDOperations.LinijaCrud.FindLinija
{
    public class FindByPolaziste : IFindOperation<Linija>
    {
        public List<Linija> FindByCriteria(List<Linija> linije, string criteria)
        {
            if (linije == null)
                throw new ArgumentNullException(nameof(linije));

            if (string.IsNullOrWhiteSpace(criteria))
                throw new ArgumentException("Criteria cannot be null or empty.", nameof(criteria));

            return linije.Where(l => l.Polaziste.Contains(criteria)).ToList();
        }
    }
}