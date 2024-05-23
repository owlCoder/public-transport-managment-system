using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Runtime.Serialization;

namespace Service.Database.Models
{
    [Table("Vozaci")]
    [DataContract]
    public class Vozac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Ime { get; set; }

        [Required]
        [StringLength(50)]
        public string Prezime { get; set; }

        [Required]
        [EnumDataType(typeof(UserRole))]
        public string Role { get; set; }

        [Required]
        [StringLength(8)]
        public string Oznaka { get; set; }

        public virtual DbSet<Linija> Linije { get; set; }
    }
}