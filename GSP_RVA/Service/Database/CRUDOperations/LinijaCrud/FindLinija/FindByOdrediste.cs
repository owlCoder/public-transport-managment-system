using Service.Database.CRUD;
using Service.Database.Models;
using System;
using System.Collections.Generic;

namespace Service.Database.CRUDOperations.LinijaCrud.FindLinija
{
                    public class FindByOdrediste : IFindOperation<Linija>
    {
                                                        public List<Linija> FindByCriteria(List<Linija> linije, string criteria)
        {
            // Find lines in the provided list where the Odrediste property contains the criteria
            // throw new NotImplementedException();

            // TODO: Implement the logic to filter the list based on the destination criteria
            // Example:
            // return linije.Where(l => l.Odrediste.Contains(criteria)).ToList();
            throw new NotImplementedException();
        }
    }
}