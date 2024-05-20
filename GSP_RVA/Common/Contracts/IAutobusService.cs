using Common.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface IAutobusService
    {
        [OperationContract]
        bool DodajAutobus(string oznaka);

        [OperationContract]
        bool IzmeniAutobus(int id, AutobusDTO data);

        [OperationContract]
        bool ObrisiAutobus(int id);

        [OperationContract]
        AutobusDTO Procitaj(int id);

        [OperationContract]
        List<AutobusDTO> ProcitajSve();
    }
}
