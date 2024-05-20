using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;

namespace Service.Database.CRUDOperations.AutobusCrud
{
    public class InsertAutobus : ICreateOperation<Autobus>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public InsertAutobus(DatabaseContext context)
        {
            _context = context;
        }

        public bool Insert(Autobus autobus)
        {
            lock (_lock)
            {
                _context.Autobusi.Add(autobus);
                return _context.SaveChanges() > 0;
            }
        }
    }
}
