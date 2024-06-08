using Service.Database.Models;
using System.Collections.Generic;

namespace Service.Database.CRUD
{
    public interface IFindOperation<T> where T : class
    {
        List<T> FindByCriteria(List<Linija> linije, string criteria);
    }
}