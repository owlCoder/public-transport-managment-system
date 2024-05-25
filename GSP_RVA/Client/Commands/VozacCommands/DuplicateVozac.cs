using Common.DTO;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.VozacCommands
{
    class DuplicateCommand : Command, ICloneable
    {
        private readonly IVozacService vozacService;
        private readonly VozacDTO originalVozac;
        private VozacDTO duplicatedVozac;
        private int duplicatedVozacId;
        private bool success;

        public DuplicateCommand(IVozacService vozacService, VozacDTO originalVozac)
        {
            this.vozacService = vozacService;
            this.originalVozac = originalVozac;
        }

        public override void Execute()
        {
            // Create a copy of the original VozacDTO
            duplicatedVozac = (VozacDTO)Clone();
            // Add the duplicated VozacDTO using the service
            success = vozacService.DodajVozaca(duplicatedVozac);
        }

        public override void Undo()
        {
            // Remove the duplicated VozacDTO
            vozacService.ObrisiVozaca(duplicatedVozacId);
        }

        public override void Redo()
        {
            // Re-add the duplicated VozacDTO
            success = vozacService.DodajVozaca(duplicatedVozac);
        }

        public object Clone()
        {
            return new VozacDTO
            {
                Id = 0, // Set ID to 0 or another default value to indicate a new entity
                Username = originalVozac.Username,
                Password = originalVozac.Password,
                Ime = originalVozac.Ime,
                Prezime = originalVozac.Prezime,
                Role = originalVozac.Role,
                Oznaka = originalVozac.Oznaka,
                Linije = new List<LinijaDTO>(originalVozac.Linije ?? new List<LinijaDTO>())
            };
        }
    }
}
