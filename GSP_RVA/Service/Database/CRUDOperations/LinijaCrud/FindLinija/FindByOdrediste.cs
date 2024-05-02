using Service.Database.CRUD;
using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Database.CRUDOperations.LinijaCrud.FindLinija
{
    public class FindByOdrediste : IFindOperation<Linija>
    {
        public List<Linija> FindByCriteria(List<Linija> linije, string criteria)
        {
            // pronadji mi u bazi po polazistu find l => l.Polaziste.containt(crtiea)
            throw new NotImplementedException();
        }
    }
}
