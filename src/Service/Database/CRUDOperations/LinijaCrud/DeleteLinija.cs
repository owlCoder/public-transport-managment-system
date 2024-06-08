using Service.Database.Context;
using Service.Database.Models;
using System.Linq;

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

                var za_brisanje = _context.VozaciLinije.Where(vl => vl.LinijaID == find.Id).ToList();

                // ukloni iz vezne tabele
                _context.VozaciLinije.RemoveRange(za_brisanje);

                // oslobodi autobuse jer je linija obrisana
                var za_oslobodjene = _context.Autobusi.Where(a => a.IdLinije == find.Id && a.IdLinije != 0);

                foreach (var bus in za_oslobodjene)
                {
                    bus.IdLinije = 0; // ukloni liniju
                }
                return _context.SaveChanges() > 0;
            }
        }
    }
}
