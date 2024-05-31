using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;
using System;

namespace Service.Database.CRUDOperations.LinijaCrud
{
    public class InsertLinija : ICreateOperation<Linija>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public InsertLinija(DatabaseContext context)
        {
            _context = context;
        }

        public bool Insert(Linija linija)
        {
            try
            {
                lock (_lock)
                {
                    _context.Linije.Add(linija);
                    return _context.SaveChanges() > 0;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
