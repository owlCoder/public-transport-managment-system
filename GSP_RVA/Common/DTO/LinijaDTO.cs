using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.DTO
{
    [DataContract(IsReference = true)]
    public class LinijaDTO
    {
        [DataMember]
        public int Id
        {
            get; set;
        }

        [DataMember]
        public string Oznaka
        {
            get; set;
        }

        [DataMember]
        public string Polaziste
        {
            get; set;
        }

        [DataMember]
        public string Odrediste
        {
            get; set;
        }

        [DataMember]
        public List<VozacDTO> Vozaci
        {
            get; set;
        }

        [DataMember]
        public List<AutobusDTO> Autobusi
        {
            get; set;
        }
    }
}
