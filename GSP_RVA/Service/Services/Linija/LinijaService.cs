using Common.DTO;
using Common.Interfaces;
using Service.Database;
using Service.Database.CRUDOperations.LinijaCrud;
using Service.Database.Models;
using System;
using System.Collections.Generic;

namespace Service.Services.LinijaService
{
    public class LinijaService : ILinijaService
    {
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
                    Odrediste = data.Odrediste
                };

                if(insert.Insert(linija))
                {
                    Linija pronadjeno = read.ReadByCriteria(l => l.Oznaka == data.Oznaka && l.Polaziste == data.Polaziste && l.Odrediste == data.Odrediste);
                    return pronadjeno != null ? pronadjeno.Id : 0;
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception)
            {
                return 0;
            }
        }

        public int IzmeniLiniju(int id, LinijaDTO data)
        {
            throw new NotImplementedException();
        }

        public int ObrisiLiniju(int id)
        {
            throw new NotImplementedException();
        }

        public LinijaDTO Procitaj(int id)
        {
            throw new NotImplementedException();
        }

        public List<LinijaDTO> ProcitajSve()
        {
            throw new NotImplementedException();
        }

        public LinijaDTO Pretraga(bool poOdredistu, string unos)
        {
            throw new NotImplementedException();
        }
    }
}
