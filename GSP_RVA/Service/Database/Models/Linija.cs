using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Database.Models
{
    [Table("Linije")]
    public class Linija
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Oznaka { get; set; }

        [Required]
        [StringLength(50)]
        public string Polaziste { get; set; }

        [Required]
        [StringLength(50)]
        public string Odrediste { get; set; }

        // Navigation property for Vozaci
        public List<Vozac> Vozaci { get; set; }

        // Navigation property for Autobusi
        public List<Autobus> Autobusi { get; set; }

        public Linija()
        {
            Vozaci = new List<Vozac>();
            Autobusi = new List<Autobus>();
        }
    }
}
