using Service.Database.Context;
using Service.Database.CRUD;
using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Database.CRUDOperations.VozacCrud
{
    public class UpdateVozac : IUpdateOperation<Vozac>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public UpdateVozac(DatabaseContext context)
        {
            _context = context;
        }

        public bool Update(int id, Vozac vozac)
        {
            // Use a lock to ensure thread safety
            lock (_lock)
            {
                Vozac find = _context.Vozaci.Find(id);

                if (find == null)
                    return false;

                find = vozac;
                return _context.SaveChanges() > 0;
            }
        }
    }
}
