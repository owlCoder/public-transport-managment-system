using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Database.Models
{
    public class Vozac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Index(nameof(Username), IsUnique = true)]
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
        [DefaultValue(UserRole.Vozac)]
        public UserRole Role { get; set; }

        [Required]
        [StringLength(8)]
        public string Oznaka { get; set; }

        // proveri
        public List<Linija> Linije { get; set; }
    }
}
