using System.ServiceModel;

namespace Common.Contracts
{
    [ServiceContract]
    public interface IVozacLinijaService
    {
        [OperationContract]
        bool DodajVozacaNaLiniju(int vozac_id, int linija_id);

        [OperationContract]
        bool UkloniVozacaNaLiniji(int vozac_id, int linija_id);
    }
}
