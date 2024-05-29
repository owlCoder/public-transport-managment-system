using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
