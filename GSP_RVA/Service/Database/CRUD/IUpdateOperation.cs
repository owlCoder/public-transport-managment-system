using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Database.CRUD
{
    public interface IUpdateOperation<T> where T : class
    {
        bool Update(int id, T entity);
    }
}
