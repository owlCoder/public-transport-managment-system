using Common.Enums;
using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Database.CRUD
{
    public interface IFindOperation<T> where T : class
    {
        List<T> FindByCriteria(List<Linija> linije, string criteria);
    }
}
