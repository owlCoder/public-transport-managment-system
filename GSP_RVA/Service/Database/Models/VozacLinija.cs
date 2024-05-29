using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Database.Models
{
    [Table("VozacLinija")]
    public class VozacLinija
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int VozacId { get; set; }

        [Required]
        public int LinijaID { get; set; }
    }
}
