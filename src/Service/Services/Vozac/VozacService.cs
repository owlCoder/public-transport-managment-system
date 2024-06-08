using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using Service.Database;
using Service.Database.CRUDOperations.VozacCrud;
using Service.Database.Models;
using System;
using System.Collections.Generic;

namespace Service.Services.VozacService
{
    public class VozacService : IVozacService
    {
        private ILogger logger = Program.logger;

        public bool DodajVozaca(VozacDTO data)
        {
            try
            {
                InsertVozac insert = new InsertVozac(DatabaseService.Instance.Context);
                ReadVozac read = new ReadVozac(DatabaseService.Instance.Context);

                Vozac novi = new Vozac()
                {
                    Ime = data.Ime,
                    Prezime = data.Prezime,
                    Username = data.Username,
                    Password = data.Password,
                    Role = data.Role.ToString(),
                    Oznaka = data.Oznaka
                };

                if (insert.Insert(novi))
                {
                    var vozac = read.ReadByCriteria(v => v.Username == novi.Username);
                    logger.Log(LogTraceLevel.INFO, $"Vozac sa ID-jem {vozac.Id} je uspešno dodat.");
                    return vozac != null;
                }
                else
                {
                    logger.Log(LogTraceLevel.INFO, "Dodavanje vozača nije uspelo.");
                    return false;
                }
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom dodavanja vozača. StackTrace: {e.Message}");
                return false;
            }
        }

        public VozacDTO IzmeniVozaca(int id, VozacDTO data)
        {
            try
            {
                ReadVozac read = new ReadVozac(DatabaseService.Instance.Context);
                UpdateVozac update = new UpdateVozac(DatabaseService.Instance.Context);

                var existingVozac = read.Read(id);

                if (existingVozac == null)
                {
                    logger.Log(LogTraceLevel.INFO, $"Vozac sa ID-jem {id} ne postoji za izmenu.");
                    return new VozacDTO() { Id = 0 };
                }

                existingVozac.Ime = data.Ime;
                existingVozac.Prezime = data.Prezime;
                existingVozac.Username = data.Username;
                existingVozac.Password = data.Password;
                existingVozac.Role = data.Role.ToString();
                existingVozac.Oznaka = data.Oznaka;

                if (update.Update(id, existingVozac))
                {
                    logger.Log(LogTraceLevel.INFO, $"Vozac sa ID-jem {id} je uspešno ažuriran.");
                    return new VozacDTO()
                    {
                        Id = existingVozac.Id,
                        Ime = existingVozac.Ime,
                        Prezime = existingVozac.Prezime,
                        Username = existingVozac.Username,
                        Password = existingVozac.Password,
                        Role = data.Role,
                        Oznaka = existingVozac.Oznaka
                    };
                }
                else
                {
                    logger.Log(LogTraceLevel.INFO, $"Ažuriranje vozača sa ID-jem {id} nije uspelo.");
                    return new VozacDTO() { Id = 0 };
                }
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom izmene vozača sa ID-jem {id}. StackTrace: {e.Message}");
                return new VozacDTO() { Id = 0 };
            }

        }

        public bool ObrisiVozaca(int id)
        {
            try
            {
                DeleteVozac delete = new DeleteVozac(DatabaseService.Instance.Context);
                if (delete.Delete(id))
                {
                    logger.Log(LogTraceLevel.INFO, $"Vozac sa ID-jem {id} je uspešno obrisan.");
                    return true;
                }
                else
                {
                    logger.Log(LogTraceLevel.INFO, $"Brisanje vozača sa ID-jem {id} nije uspelo.");
                    return false;
                }
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom brisanja vozača sa ID-jem {id}. StackTrace: {e.Message}");
                return false;
            }
        }

        public int Prijava(string username, string password)
        {
            try
            {
                ReadVozac read = new ReadVozac(DatabaseService.Instance.Context);
                var vozac = read.ReadByCriteria(v => v.Username == username && v.Password == password);

                if (vozac == null)
                {
                    logger.Log(LogTraceLevel.INFO, $"Neuspešna prijava za korisnika sa korisničkim imenom '{username}'.");
                    return 0;
                }
                else
                {
                    logger.Log(LogTraceLevel.INFO, $"Korisnik '{username}' je uspešno prijavljen.");
                    return vozac.Id;
                }
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom prijave korisnika '{username}'. StackTrace: {e.Message}");
                return 0;
            }
        }

        public VozacDTO Procitaj(int id)
        {
            try
            {
                ReadVozac readVozac = new ReadVozac(DatabaseService.Instance.Context);
                var vozac = readVozac.Read(id);

                if (vozac == null)
                {
                    logger.Log(LogTraceLevel.INFO, $"Vozac sa ID-jem {id} ne postoji.");
                    return new VozacDTO() { Id = 0 };
                }

                return new VozacDTO()
                {
                    Id = vozac.Id,
                    Ime = vozac.Ime,
                    Linije = new List<LinijaDTO>(), // You might want to populate this
                    Oznaka = vozac.Oznaka,
                    Prezime = vozac.Prezime,
                    Password = vozac.Password,
                    Role = vozac.Role == "Admin" ? Common.Enums.UserRole.Admin : Common.Enums.UserRole.Vozac,
                    Username = vozac.Username
                };
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom čitanja vozača sa ID-jem {id}. StackTrace: {e.Message}");
                return new VozacDTO() { Id = 0 };
            }

        }

        public List<VozacDTO> ProcitajSve()
        {
            try
            {
                ReadVozac readVozac = new ReadVozac(DatabaseService.Instance.Context);
                var vozaci = readVozac.ReadAll();

                List<VozacDTO> vozacDTOList = new List<VozacDTO>();

                foreach (var vozac in vozaci)
                {
                    vozacDTOList.Add(Procitaj(vozac.Id));
                }

                logger.Log(LogTraceLevel.INFO, "Čitanje svih vozača je završeno.");
                return vozacDTOList;
            }
            catch (Exception e)
            {
                logger.Log(LogTraceLevel.DEBUG, $"Greška prilikom čitanja svih vozača. StackTrace: {e.Message}");
                return new List<VozacDTO>();
            }
        }
    }
}
