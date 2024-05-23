using Common.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface IVozacService
    {
        [OperationContract]
        VozacDTO DodajVozaca(VozacDTO data);

        [OperationContract]
        VozacDTO IzmeniVozaca(int id, VozacDTO data);

        [OperationContract]
        bool ObrisiVozaca(int id);

        [OperationContract]
        VozacDTO Procitaj(int id);

        [OperationContract]
        List<VozacDTO> ProcitajSve();

        [OperationContract]
        int Prijava(string username, string password);
    }
}
