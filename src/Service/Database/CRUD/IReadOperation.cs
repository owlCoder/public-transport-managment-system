using System;
using System.Collections.Generic;

namespace Service.Database.Operations
{
    public interface IReadOperation<T> where T : class
    {
        T Read(int id);

        List<T> ReadAll();

        T ReadByCriteria(Func<T, bool> filter);

        List<T> ReadAllByCriteria(Func<T, bool> filter);
    }
}