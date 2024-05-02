using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Database.CRUD
{
    public interface IDeleteOperation<T> where T : class
    {
        bool Delete(T entity);

        bool Delete(int id);
    }
}
