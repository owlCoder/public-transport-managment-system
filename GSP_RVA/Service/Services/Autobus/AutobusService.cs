using Common.DTO;
using Common.Interfaces;
using Service.Database.CRUDOperations.AutobusCrud;
using Service.Database.Models;
using Service.Database;
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

                    // da li je uspesno!!!!!!!!!!!! PROMENI
                    Program.logger.Log(Common.Enums.LogTraceLevel.INFO, "Sve je do jaja");

                    return autobus != null;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
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

                return update.Update(id, existingAutobus);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ObrisiAutobus(int id)
        {
            try
            {
                DeleteAutobus delete = new DeleteAutobus(DatabaseService.Instance.Context);
                return delete.Delete(id);
            }
            catch (Exception)
            {
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

                return new AutobusDTO()
                {
                    Id = autobus.Id,
                    Oznaka = autobus.Oznaka,
                    IdLinije = autobus.IdLinije ?? 0
                };
            }
            catch (Exception)
            {
                return new AutobusDTO() { Id = 0 };
            }
        }

        public List<AutobusDTO> ProcitajSve()
        {
            try
            {
                ReadAutobus readAutobus = new ReadAutobus(DatabaseService.Instance.Context);
                List<Autobus> autobusi = readAutobus.ReadAll();

                List<AutobusDTO> sve = new List<AutobusDTO>();
                foreach (Autobus a in autobusi)
                {
                    sve.Add(Procitaj(a.Id));
                }

                return sve;
            }
            catch (Exception)
            {
                return new List<AutobusDTO>();
            }
        }
    }
}
