using System.Runtime.Serialization;

namespace Common.Enums
{
    [DataContract]
    public enum UserRole
    {
        [EnumMember] Admin,

        [EnumMember] Vozac
    }
}