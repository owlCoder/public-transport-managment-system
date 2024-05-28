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
                    Odrediste = linija.Odrediste
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
                    Odrediste = l.Odrediste
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
    }
}
