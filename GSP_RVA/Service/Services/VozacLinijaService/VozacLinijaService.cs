using Common.Contracts;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using Service.Database;
using Service.Database.CRUDOperations.LinijaCrud;
using Service.Database.CRUDOperations.VozacCrud;
using Service.Database.Mapper;
using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.VozacLinijeService
{
    public class VozacLinijaService : IVozacLinijaService
    {
        private static ILogger logger = Program.logger;
        private static readonly object _lock = new object();

        public static List<VozacDTO> IscitajVozaceZaLiniju(int linija_id)
        {
            try
            {
                List<VozacLinija> vozaci_za_liniju = new List<VozacLinija>();

                lock (_lock)
                {
                    vozaci_za_liniju = DatabaseService.Instance.Context.VozaciLinije.AsNoTracking().ToList();
                }

                var svi_vozaci = new ReadVozac(DatabaseService.Instance.Context).ReadAllByCriteria(v => v.Role == UserRole.Vozac.ToString());

                List<VozacDTO> vozac_dtos = new List<VozacDTO>();

                foreach (var v in svi_vozaci)
                {
                    // Ako vozac na liniji postoji vozacdto ischecked je true u suprutnom je false

                    VozacDTO trenutni = MappingHelper.MapVozacToVozacDTO(v); // mapiranje na dto

                    // Da li postoji vozac koji za sebe ima trazenu liniju u veznoj tabeli
                    if (vozaci_za_liniju.Any(vozacId => vozacId.VozacId == v.Id && vozacId.LinijaID == linija_id))
                        trenutni.IsChecked = true;
                    else
                        trenutni.IsChecked = false;

                    vozac_dtos.Add(trenutni);
                }

                return vozac_dtos;
            }
            catch(Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom citanja vozaca za liniju. StackTrace: {e.Message}");
                return new List<VozacDTO> { };
            }
        }

        public bool DodajVozacaNaLiniju(int vozac_id, int linija_id)
        {
            throw new NotImplementedException();
        }

        public bool UkloniVozacaNaLiniji(int vozac_id, int linija_id)
        {
            throw new NotImplementedException();
        }
    }
}
