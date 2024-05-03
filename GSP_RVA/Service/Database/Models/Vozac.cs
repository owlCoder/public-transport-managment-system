using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Service.Database.Models
{
    /// <summary>
    /// Represents a Vozac (Driver) entity in the database.
    /// </summary>
    [Table("Vozaci")]
    public class Vozac
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Vozac.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the Vozac.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the Vozac.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the first name of the Vozac.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Ime { get; set; }

        /// <summary>
        /// Gets or sets the last name of the Vozac.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Prezime { get; set; }

        /// <summary>
        /// Gets or sets the role of the Vozac.
        /// </summary>
        [Required]
        [EnumDataType(typeof(UserRole))]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the designation code (oznaka) of the Vozac.
        /// </summary>
        [Required]
        [StringLength(8)]
        public string Oznaka { get; set; }

        /// <summary>
        /// Gets or sets the virtual DbSet of Linija entities associated with the Vozac.
        /// </summary>
        public virtual DbSet<Linija> Linije { get; set; }
    }
}