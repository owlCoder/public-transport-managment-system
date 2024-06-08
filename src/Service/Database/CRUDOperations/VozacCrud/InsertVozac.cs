using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;
using System;

namespace Service.Database.CRUDOperations.VozacCrud
{
    public class InsertVozac : ICreateOperation<Vozac>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public InsertVozac(DatabaseContext context)
        {
            _context = context;
        }

        public bool Insert(Vozac vozac)
        {
            try
            {
                // Use a lock to ensure thread safety
                lock (_lock)
                {
                    _context.Vozaci.Add(vozac);
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