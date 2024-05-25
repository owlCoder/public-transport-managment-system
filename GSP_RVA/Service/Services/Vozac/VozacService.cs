using Common.DTO;
using Common.Interfaces;
using Service.Database;
using Service.Database.CRUDOperations.LinijaCrud;
using Service.Database.CRUDOperations.VozacCrud;
using Service.Database.Models;
using System;
using System.Collections.Generic;

namespace Service.Services.VozacService
{
    public class VozacService : IVozacService
    {
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
                    // vrati vozaca nazad
                    var vozac = read.ReadByCriteria(v => v.Username == novi.Username);
                    return vozac != null;
                }
                else
                    return false;
            }
            catch (Exception)
            {
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
                    return new VozacDTO() { Id = 0 };
                }

                existingVozac.Ime = data.Ime;
                existingVozac.Prezime = data.Prezime;
                existingVozac.Username = data.Username;
                existingVozac.Password = data.Password;
                existingVozac.Role = data.Role.ToString();
                existingVozac.Oznaka = data.Oznaka;

                if (update.Update(existingVozac))
                {
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
                    return new VozacDTO() { Id = 0 };
                }
            }
            catch (Exception)
            {
                return new VozacDTO() { Id = 0 };
            }

        }

        public bool ObrisiVozaca(int id)
        {
            throw new NotImplementedException();
        }

        public int Prijava(string username, string password)
        {
            try
            {
                ReadVozac read = new ReadVozac(DatabaseService.Instance.Context);
                ReadLinija readLinija = new ReadLinija(DatabaseService.Instance.Context);

                var vozac = read.ReadByCriteria(v => v.Username == username && v.Password == password);

                if (vozac == null)
                    return 0;
                else
                    return vozac.Id;
            }
            catch (Exception)
            {
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
                    return new VozacDTO() { Id = 0 };

                return new VozacDTO() { Id = vozac.Id, Ime = vozac.Ime, Linije = new List<LinijaDTO>(), Oznaka = vozac.Oznaka, Prezime = vozac.Prezime, Role = vozac.Role == "Admin" ? Common.Enums.UserRole.Admin : Common.Enums.UserRole.Vozac, Username = vozac.Username };
            }
            catch
            {
                return new VozacDTO { Id = 0 };
            }

        }

        public List<VozacDTO> ProcitajSve()
        {
            throw new NotImplementedException();
        }
    }
}
