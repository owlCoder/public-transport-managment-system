using Common.DTO;
using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services.AutobusService
{
    public class AutobusService : IAutobusService
    {
        public bool DodajAutobus(string oznaka)
        {
            throw new NotImplementedException();
        }

        public bool IzmeniAutobus(int id, AutobusDTO data)
        {
            throw new NotImplementedException();
        }

        public bool ObrisiAutobus(int id)
        {
            throw new NotImplementedException();
        }

        public AutobusDTO Procitaj(int id)
        {
            throw new NotImplementedException();
        }

        public List<AutobusDTO> ProcitajSve()
        {
            throw new NotImplementedException();
        }
    }
}
