using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Database.Models
{
    [Table("Autobusi")]
    public class Autobus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Oznaka { get; set; }

        // Foreign key for Linija
        public int? IdLinije { get; set; }

        [ForeignKey("IdLinije")]
        public Linija Linija { get; set; }
    }
}
