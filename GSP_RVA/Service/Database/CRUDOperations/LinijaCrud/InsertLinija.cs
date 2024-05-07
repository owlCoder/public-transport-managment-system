using Service.Database.Context;
using Service.Database.Models;
using Service.Database.Operations;

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
            lock (_lock)
            {
                _context.Linije.Add(linija);
                return _context.SaveChanges() > 0;
            }
        }
    }
}
