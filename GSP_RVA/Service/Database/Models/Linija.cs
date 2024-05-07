using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Service.Database.Models
{
    /// <summary>
    /// Represents a Linija (Line) entity in the database.
    /// </summary>
    [Table("Linije")]
    public class Linija
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Linija.
        /// </summary>
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

        public virtual DbSet<Vozac> Vozaci { get; set; }

        public virtual DbSet<Autobus> Autobusi { get; set; }
    }
}