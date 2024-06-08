using Common.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface ILinijaService
    {
        [OperationContract]
        int DodajLiniju(LinijaDTO data);

        [OperationContract]
        int IzmeniLiniju(int id, LinijaDTO data);

        [OperationContract]
        int ObrisiLiniju(int id);

        [OperationContract]
        LinijaDTO Procitaj(int id);

        [OperationContract]
        List<LinijaDTO> ProcitajSve();

        [OperationContract]
        List<LinijaDTO> Pretraga(bool poOdredistu, string unos);
    }
}
