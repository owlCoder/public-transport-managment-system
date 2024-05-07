using Service.Database.Context;
using Service.Database.Models;

namespace Service.Database.CRUDOperations.LinijaCrud
{
    public class DeleteLinija
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public DeleteLinija(DatabaseContext context)
        {
            _context = context;
        }

        public bool Delete(Linija linija)
        {
            lock (_lock)
            {
                _context.Linije.Remove(linija);
                return _context.SaveChanges() > 0;
            }
        }

        public bool Delete(int id)
        {
            lock (_lock)
            {
                Linija find = _context.Linije.Find(id);
                if (find == null)
                    return false;

                _context.Linije.Remove(find);
                return _context.SaveChanges() > 0;
            }
        }
    }
}
