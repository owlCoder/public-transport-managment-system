using Service.Database.Context;
using Service.Database.CRUD;
using Service.Database.Models;

namespace Service.Database.CRUDOperations.AutobusCrud
{
    public class UpdateAutobus : IUpdateOperation<Autobus>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public UpdateAutobus(DatabaseContext context)
        {
            _context = context;
        }

        public bool Update(int id, Autobus autobus)
        {
            lock (_lock)
            {
                Autobus find = _context.Autobusi.Find(id);
                if (find == null)
                    return false;

                // Update fields
                find.Oznaka = autobus.Oznaka;
                find.IdLinije = autobus.IdLinije;

                return _context.SaveChanges() > 0;
            }
        }
    }
}

