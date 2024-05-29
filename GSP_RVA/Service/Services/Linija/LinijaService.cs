using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using Service.Database;
using Service.Database.CRUD;
using Service.Database.CRUDOperations.LinijaCrud;
using Service.Database.CRUDOperations.LinijaCrud.FindLinija;
using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services.LinijaService
{
    public class LinijaService : ILinijaService
    {
        private ILogger logger = Program.logger;

        public int DodajLiniju(LinijaDTO data)
        {
            try
            {
                InsertLinija insert = new InsertLinija(DatabaseService.Instance.Context);
                ReadLinija read = new ReadLinija(DatabaseService.Instance.Context);

                Linija linija = new Linija()
                {
                    Oznaka = data.Oznaka,
                    Polaziste = data.Polaziste,
                    Odrediste = data.Odrediste,
                };

                if (insert.Insert(linija))
                {
                    Linija pronadjeno = read.ReadByCriteria(l => l.Oznaka == data.Oznaka && l.Polaziste == data.Polaziste && l.Odrediste == data.Odrediste);
                    if (pronadjeno != null)
                        logger.Log(LogTraceLevel.INFO, $"Linija sa ID-jem {pronadjeno.Id} je uspešno dodata.");
                    else
                        logger.Log(LogTraceLevel.INFO, $"Linija nije uspešno dodata.");

                    return pronadjeno != null ? pronadjeno.Id : 0;
                }
                else
                {
                    logger.Log(LogTraceLevel.INFO, "Dodavanje linije nije uspelo.");
                    return 0;
                }
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom dodavanja linije. StackTrace: {e.Message}");
                return 0;
            }
        }

        public int IzmeniLiniju(int id, LinijaDTO data)
        {
            try
            {
                ReadLinija read = new ReadLinija(DatabaseService.Instance.Context);
                UpdateLinija update = new UpdateLinija(DatabaseService.Instance.Context);

                var existingLinija = read.Read(id);

                if (existingLinija == null)
                {
                    logger.Log(LogTraceLevel.INFO, $"Linija sa ID-jem {id} ne postoji za izmenu.");
                    return 0;
                }

                existingLinija.Oznaka = data.Oznaka;
                existingLinija.Polaziste = data.Polaziste;
                existingLinija.Odrediste = data.Odrediste;
                //existingLinija.Autobusi = MapAutobusDTOsToAutobuses(data.Autobusi);
                existingLinija.Vozaci = MapDriverDTOsToDrivers(data.Vozaci);

                if (update.Update(id, existingLinija))
                {
                    logger.Log(LogTraceLevel.INFO, $"Linija sa ID-jem {id} je uspešno ažurirana.");
                    return existingLinija.Id;
                }
                else
                {
                    logger.Log(LogTraceLevel.INFO, $"Ažuriranje linije sa ID-jem {id} nije uspelo.");
                    return 0;
                }
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom izmene linije sa ID-jem {id}. StackTrace: {e.Message}");
                return 0;
            }
        }

        public int ObrisiLiniju(int id)
        {
            try
            {
                DeleteLinija delete = new DeleteLinija(DatabaseService.Instance.Context);
                if (delete.Delete(id))
                {
                    logger.Log(LogTraceLevel.INFO, $"Linija sa ID-jem {id} je uspešno obrisana.");
                    return id;
                }
                else
                {
                    logger.Log(LogTraceLevel.INFO, $"Brisanje linije sa ID-jem {id} nije uspelo.");
                    return 0;
                }
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom brisanja linije sa ID-jem {id}. StackTrace: {e.Message}");
                return 0;
            }
        }

        public LinijaDTO Procitaj(int id)
        {
            try
            {
                ReadLinija readLinija = new ReadLinija(DatabaseService.Instance.Context);
                var linija = readLinija.Read(id);

                if (linija == null)
                {
                    logger.Log(LogTraceLevel.INFO, $"Linija sa ID-jem {id} ne postoji.");
                    return new LinijaDTO() { Id = 0 };
                }

                return new LinijaDTO()
                {
                    Id = linija.Id,
                    Oznaka = linija.Oznaka,
                    Polaziste = linija.Polaziste,
                    Odrediste = linija.Odrediste,
                    Vozaci = MapDriversToDriverDTO(linija.Vozaci)
                    // dodaj i autobuse ovde mamu im
                };
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom čitanja linije sa ID-jem {id}. StackTrace: {e.Message}");
                return new LinijaDTO() { Id = 0 };
            }
        }

        public List<LinijaDTO> ProcitajSve()
        {
            try
            {
                ReadLinija readLinija = new ReadLinija(DatabaseService.Instance.Context);
                List<Linija> linije = readLinija.ReadAll();

                List<LinijaDTO> sve = new List<LinijaDTO>();
                foreach (Linija l in linije)
                {
                    sve.Add(Procitaj(l.Id));
                }

                logger.Log(LogTraceLevel.INFO, "Čitanje svih linija je završeno.");
                return sve;
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom čitanja svih linija. StackTrace: {e.Message}");
                return new List<LinijaDTO>();
            }
        }

        public List<LinijaDTO> Pretraga(bool poOdredistu, string unos)
        {
            try
            {
                ReadLinija read = new ReadLinija(DatabaseService.Instance.Context);
                List<Linija> linije = read.ReadAll();

                IFindOperation<Linija> findOperation;

                if (poOdredistu)
                {
                    findOperation = new FindByOdrediste();
                }
                else
                {
                    findOperation = new FindByPolaziste();
                }

                List<Linija> filteredLinije = findOperation.FindByCriteria(linije, unos);

                List<LinijaDTO> filteredLinijaDTOs = filteredLinije.Select(l => new LinijaDTO
                {
                    Id = l.Id,
                    Oznaka = l.Oznaka,
                    Polaziste = l.Polaziste,
                    Odrediste = l.Odrediste,
                    //Vozaci = l.Vozaci.ToList(),

                }).ToList();

                logger.Log(LogTraceLevel.INFO, "Pretraga linija je završena.");
                return filteredLinijaDTOs;
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom pretrage linija.StackTrace: {e.Message}");
                return new List<LinijaDTO>();
            }
        }



        // Ubaci u poseban fajl!!! i napravi static
        public List<AutobusDTO> MapAutobusesToAutobusDTOs(List<Autobus> autobuses)
        {
            return autobuses.Select(autobus => new AutobusDTO
            {
                Id = autobus.Id,
                Oznaka = autobus.Oznaka,
                IdLinije = autobus.IdLinije,
                Linije = autobus.Linija != null ? new List<LinijaDTO>
        {
            new LinijaDTO
            {
                Id = autobus.Linija.Id,
                Oznaka = autobus.Linija.Oznaka,
                Polaziste = autobus.Linija.Polaziste,
                Odrediste = autobus.Linija.Odrediste
            }
        } : new List<LinijaDTO>()
            }).ToList();
        }

        public List<Autobus> MapAutobusDTOsToAutobuses(List<AutobusDTO> autobusDTOs)
        {
            return autobusDTOs.Select(dto => new Autobus
            {
                Id = dto.Id,
                Oznaka = dto.Oznaka,
                IdLinije = dto.IdLinije,
                Linija = dto.Linije != null && dto.Linije.Any() ? new Linija
                {
                    Id = dto.Linije.First().Id,
                    Oznaka = dto.Linije.First().Oznaka,
                    Polaziste = dto.Linije.First().Polaziste,
                    Odrediste = dto.Linije.First().Odrediste
                } : null
            }).ToList();
        }

        public List<Vozac> MapDriverDTOsToDrivers(List<VozacDTO> driverDTOs)
        {
            return driverDTOs.Select(dto => new Vozac
            {
                Id = dto.Id,
                Username = dto.Username,
                Password = dto.Password,
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                Role = dto.Role == UserRole.Admin ? "Admin" : "Vozac",
                Oznaka = dto.Oznaka,
                Linije = dto.Linije.Select(linijaDto => new Linija
                {
                    Id = linijaDto.Id,
                    Oznaka = linijaDto.Oznaka,
                    Polaziste = linijaDto.Polaziste,
                    Odrediste = linijaDto.Odrediste
                }).ToList()
            }).ToList();
        }


        public List<VozacDTO> MapDriversToDriverDTO(List<Vozac> drivers)
        {
            return drivers.Select(driver => new VozacDTO
            {
                Id = driver.Id,
                Username = driver.Username,
                Password = driver.Password,
                Ime = driver.Ime,
                Prezime = driver.Prezime,
                Role = driver.Role == "Admin" ? UserRole.Admin : UserRole.Vozac,
                Oznaka = driver.Oznaka,
                IsChecked = true,
                Linije = driver.Linije.Select(linija => new LinijaDTO
                {
                    Id = linija.Id,
                    Oznaka = linija.Oznaka,
                    Polaziste = linija.Polaziste,
                    Odrediste = linija.Odrediste
                }).ToList()
            }).ToList();
        }
    }
}
