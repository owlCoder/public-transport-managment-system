using Service.Database.Context;
using Service.Database.Models;

namespace Service.Database.CRUDOperations.AutobusCrud
{
    public class DeleteAutobus
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public DeleteAutobus(DatabaseContext context)
        {
            _context = context;
        }

        public bool Delete(Autobus autobus)
        {
            lock (_lock)
            {
                _context.Autobusi.Remove(autobus);
                return _context.SaveChanges() > 0;
            }
        }

        public bool Delete(int id)
        {
            lock (_lock)
            {
                Autobus find = _context.Autobusi.Find(id);
                if (find == null)
                    return false;

                _context.Autobusi.Remove(find);
                return _context.SaveChanges() > 0;
            }
        }
    }
}
