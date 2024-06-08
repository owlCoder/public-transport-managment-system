using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using Service.Database;
using Service.Database.CRUDOperations.AutobusCrud;
using Service.Database.CRUDOperations.LinijaCrud;
using Service.Database.Mapper;
using Service.Database.Models;
using System;
using System.Collections.Generic;

namespace Service.Services.AutobusService
{
    public class AutobusService : IAutobusService
    {
        public bool DodajAutobus(string oznaka)
        {
            try
            {
                InsertAutobus insert = new InsertAutobus(DatabaseService.Instance.Context);
                ReadAutobus read = new ReadAutobus(DatabaseService.Instance.Context);

                Autobus noviAutobus = new Autobus()
                {
                    Oznaka = oznaka
                };

                if (insert.Insert(noviAutobus))
                {
                    var autobus = read.ReadByCriteria(a => a.Oznaka == oznaka);

                    if (autobus != null)
                        Program.logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {autobus.Id} je uspešno dodat.");
                    else
                        Program.logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {autobus} nije uspešno dodat.");

                    return autobus != null;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Program.logger.Log(LogTraceLevel.DEBUG, $"Autobus nije dodat. StackTrace: {e.Message}");
                return false;
            }
        }

        public bool IzmeniAutobus(int id, AutobusDTO data)
        {
            try
            {
                ReadAutobus read = new ReadAutobus(DatabaseService.Instance.Context);
                UpdateAutobus update = new UpdateAutobus(DatabaseService.Instance.Context);

                var existingAutobus = read.Read(id);

                if (existingAutobus == null)
                {
                    return false;
                }

                existingAutobus.Oznaka = data.Oznaka;
                existingAutobus.IdLinije = data.IdLinije;

                bool uspesno = update.Update(id, existingAutobus);

                if (uspesno)
                    Program.logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {id} je uspešno ažuriran.");
                else
                    Program.logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {id} nije uspešno ažuriran.");

                return uspesno;
            }
            catch (Exception e)
            {
                Program.logger.Log(LogTraceLevel.DEBUG, $"Autobus sa ID-jem {id} nije ažuriran. StackTrace: {e.Message}");
                return false;
            }
        }

        public bool ObrisiAutobus(int id)
        {
            try
            {
                DeleteAutobus delete = new DeleteAutobus(DatabaseService.Instance.Context);
                bool uspesno = delete.Delete(id);

                if (uspesno)
                    Program.logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {id} je uspešno obrisan.");
                else
                    Program.logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {id} nije uspešno obrisan.");

                return uspesno;
            }
            catch (Exception e)
            {
                Program.logger.Log(LogTraceLevel.DEBUG, $"Autobus sa ID-jem {id} nije obrisan. StackTrace: {e.Message}");
                return false;
            }
        }

        public AutobusDTO Procitaj(int id)
        {
            try
            {
                ReadAutobus readAutobus = new ReadAutobus(DatabaseService.Instance.Context);

                var autobus = readAutobus.Read(id);

                if (autobus == null)
                    return new AutobusDTO() { Id = 0 };

                Program.logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {id} je pročitan.");

                // Dobavi sve linije ciji je ID = 0 znaci nisu dodeljenje autobusu tj autobus liniji
                // I Id = autobus Id jer tu je dodeljen autobus
                List<Linija> filtirane_linije = new ReadLinija(DatabaseService.Instance.Context).ReadAllByCriteria(l => l.Id == 0 || l.Id == autobus.IdLinije);

                // Mapiranje na DTO
                List<LinijaDTO> dtos = new List<LinijaDTO>();

                foreach (Linija l in filtirane_linije)
                {
                    dtos.Add(MappingHelper.MapLinijaToLinijaDTO(l));
                }

                return new AutobusDTO()
                {
                    Id = autobus.Id,
                    Oznaka = autobus.Oznaka,
                    IdLinije = autobus.IdLinije,
                    Linije = dtos,
                };
            }
            catch (Exception e)
            {
                Program.logger.Log(LogTraceLevel.DEBUG, $"Autobus sa ID-jem {id} nije pročitan. StackTrace: {e.Message}");
                return new AutobusDTO() { Id = 0 };
            }
        }

        public List<AutobusDTO> ProcitajSve()
        {
            try
            {
                Program.logger.Log(LogTraceLevel.INFO, $"Primljen je zahtev za čitanje svih autobusa.");

                ReadAutobus readAutobus = new ReadAutobus(DatabaseService.Instance.Context);
                List<Autobus> autobusi = readAutobus.ReadAll();

                List<AutobusDTO> sve = new List<AutobusDTO>();
                foreach (Autobus a in autobusi)
                {
                    sve.Add(Procitaj(a.Id));
                }

                return sve;
            }
            catch (Exception e)
            {
                Program.logger.Log(LogTraceLevel.DEBUG, $"Autobusi nisu pročitani. StackTrace: {e.Message}");
                return new List<AutobusDTO>();
            }
        }
    }
}
