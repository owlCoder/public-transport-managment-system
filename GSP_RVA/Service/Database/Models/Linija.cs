using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // TODO: ADD MORE FIELDS
    }
}