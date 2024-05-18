using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Service.Database.Models
{
    [Table("Autobusi")]
    [DataContract]
    public class Autobus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Oznaka { get; set; }
    
        public int IdLinije { get; set; } = 0;

    }
}
