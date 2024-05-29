using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
