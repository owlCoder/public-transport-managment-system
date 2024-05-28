using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.DTO
{
    [DataContract]
    public class AutobusDTO
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
        public int? IdLinije
        {
            get; set;
        }

        [DataMember]
        public List<LinijaDTO> Linije
        {
            get; set;
        }
    }
}
