using Common.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface ILinijaService
    {
        [OperationContract]
        bool DodajLiniju(LinijaDTO data);

        [OperationContract]
        bool IzmeniLiniju(int id, LinijaDTO data);

        [OperationContract]
        bool ObrisiLiniju(int id);

        [OperationContract]
        LinijaDTO Procitaj(int id);

        [OperationContract]
        List<LinijaDTO> ProcitajSve();

        [OperationContract]
        LinijaDTO Pretraga(bool poOdredistu, string unos);
    }
}
