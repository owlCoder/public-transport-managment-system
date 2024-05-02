using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Database.Operations
{
    public interface ICreateOperation<T> where T : class
    {
        bool Insert(T entity);
    }
}
