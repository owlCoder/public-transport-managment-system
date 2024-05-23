using Service.Database.Context;
using Service.Database.Models;

namespace Service.Database.CRUDOperations.VozacCrud
{
    public class DeleteVozac
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public DeleteVozac(DatabaseContext context)
        {
            _context = context;
        }

        public bool Delete(Vozac vozac)
        {
            // Use a lock to ensure thread safety
            lock (_lock)
            {
                _context.Vozaci.Remove(vozac);
                return _context.SaveChanges() > 0;
            }
        }

        public bool Delete(int id)
        {
            // Use a lock to ensure thread safety
            lock (_lock)
            {
                Vozac find = _context.Vozaci.Find(id);
                if (find == null)
                    return false;

                _context.Vozaci.Remove(find);
                return _context.SaveChanges() > 0;
            }
        }
    }
}