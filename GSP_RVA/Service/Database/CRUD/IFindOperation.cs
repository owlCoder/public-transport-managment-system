using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Database.CRUD
{
    public interface IFindOperation<T> where T : class
    {
        List<T> FindByCriteria(string criteria);
    }
}
