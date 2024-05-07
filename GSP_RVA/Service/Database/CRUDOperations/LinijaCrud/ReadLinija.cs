using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;
using System.Collections.Generic;
using System.Linq;

namespace Service.Database.CRUDOperations.LinijaCrud
{
    public class ReadLinija : IReadOperation<Linija>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public ReadLinija(DatabaseContext context)
        {
            _context = context;
        }

        public Linija Read(int id)
        {
            lock(_lock)
            {
                return _context.Linije.AsNoTracking().FirstOrDefault(u => u.Id == id);
            }
        }

        public List<Linija> ReadAll()
        {
            lock(_lock)
            {
                return _context.Linije.AsNoTracking().ToList();
            }
        }
    }
}
