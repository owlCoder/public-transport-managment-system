using Service.Database.Context;
using Service.Database.CRUD;
using Service.Database.Models;

namespace Service.Database.CRUDOperations.LinijaCrud
{
    public class UpdateLinija : IUpdateOperation<Linija>
    {
        private readonly DatabaseContext _context;
        private static readonly object _lock = new object();

        public UpdateLinija(DatabaseContext context)
        {
            _context = context;
        }

        public bool Update(int id, Linija linija)
        {
            lock (_lock)
            {
                Linija find = _context.Linije.Find(id);
                if (find == null)
                    return false;

                // Update fields
                find.Oznaka = linija.Oznaka;
                find.Polaziste = linija.Polaziste;
                find.Odrediste = linija.Odrediste;

                return _context.SaveChanges() > 0;
            }
        }
    }
}
