using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;
using System;
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
            lock (_lock)
            {
                Linija linija = _context.Linije.AsNoTracking().FirstOrDefault(u => u.Id == id);
                linija.Vozaci = _context.GetVozaciWithRelatedData().ToList().FindAll(l => l.Linije.Any(ll => ll.Vozaci.Contains(l)));
                linija.Autobusi = _context.GetAutobusiWithRelatedData().ToList();
                return linija;
            }
        }

        public List<Linija> ReadAll()
        {
            lock (_lock)
            {
                return _context.Linije.AsNoTracking().ToList();
            }
        }

        public Linija ReadByCriteria(Func<Linija, bool> filter)
        {
            lock (_lock)
            {
                return _context.Linije.AsNoTracking().FirstOrDefault(filter);
            }
        }

        public List<Linija> ReadAllByCriteria(Func<Linija, bool> filter)
        {
            lock (_lock)
            {
                return _context.Linije.AsNoTracking().Where(filter).ToList();
            }
        }
    }
}
