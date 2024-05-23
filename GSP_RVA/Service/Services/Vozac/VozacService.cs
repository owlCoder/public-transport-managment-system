using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using Service.Database;
using Service.Database.CRUDOperations.LinijaCrud;
using Service.Database.CRUDOperations.VozacCrud;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services.Vozac
{
    public class VozacService : IVozacService
    {
        public bool DodajVozaca(VozacDTO data)
        {
            throw new NotImplementedException();
        }

        public bool IzmeniVozaca(int id, VozacDTO data)
        {
            throw new NotImplementedException();
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

                //var povezaneLinije = readLinija.ReadAllByCriteria(l => l.Vozaci.Any(v => v.Id == vozac.Id)).ToList();

                //var linijeDTO = povezaneLinije.Select(l => new LinijaDTO
                //{
                //    Id = l.Id,
                //    Oznaka = l.Oznaka,
                //    Polaziste = l.Polaziste,
                //    Odrediste = l.Odrediste
                //}).ToList();

                //return new VozacDTO
                //{
                //    Id = vozac.Id,
                //    Ime = vozac.Ime,
                //    Prezime = vozac.Prezime,
                //    Username = vozac.Username,
                //    Oznaka = vozac.Oznaka,
                //    Role = vozac.Role == "Admin" ? UserRole.Admin : UserRole.Vozac,
                //    Linije = new List<LinijaDTO>()
                //};
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
                var db = DatabaseService.Instance.Context;
                ReadVozac readVozac = new ReadVozac(db);
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
