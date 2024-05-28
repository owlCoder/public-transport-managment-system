using Common.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.DTO
{
    [DataContract]
    public class VozacDTO
    {
        [DataMember]
        public int Id
        {
            get; set;
        }

        [DataMember]
        public string Username
        {
            get; set;
        }

        [DataMember]
        public string Password
        {
            get; set;
        }

        [DataMember]
        public string Ime
        {
            get; set;
        }

        [DataMember]
        public string Prezime
        {
            get; set;
        }

        [DataMember]
        public UserRole Role
        {
            get; set;
        }

        [DataMember]
        public string Oznaka
        {
            get; set;
        }

        [DataMember]
        public bool IsChecked
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
