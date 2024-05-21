using Common.DTO;
using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services.Linija
{
    public class LinijaService : ILinijaService
    {
        public int DodajLiniju(LinijaDTO data)
        {
            throw new NotImplementedException();
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
