using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;
using System.Collections.Generic;
using System.Linq;

namespace Service.Database.CRUDOperations.VozacCrud
{
    public class ReadVozac : IReadOperation<Vozac>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public ReadVozac(DatabaseContext context)
        {
            _context = context;
        }

        public Vozac Read(int id)
        {
            // Use a lock to ensure thread safety
            lock (_lock)
            {
                return _context.Vozaci.AsNoTracking().FirstOrDefault(u => u.Id == id);
            }
        }

        public List<Vozac> ReadAll()
        {
            // Use a lock to ensure thread safety
            lock (_lock)
            {
                return _context.Vozaci.AsNoTracking().ToList();
            }
        }
    }
}
