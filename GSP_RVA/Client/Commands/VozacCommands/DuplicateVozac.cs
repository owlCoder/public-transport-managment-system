using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.VozacCommands
{
    class DuplicateVozac : Command, ICloneable
    {
        private readonly IVozacService vozacService;
        private readonly VozacDTO originalVozac;
        private VozacDTO duplicatedVozac;
        private int duplicatedVozacId;
        private bool success;

        public DuplicateVozac(IVozacService vozacService, VozacDTO originalVozac)
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

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dupliranje vozaca sa ID-jem {originalVozac.Id} nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Vozac sa ID-jem {originalVozac.Id} je uspesno duplirana!");
        }

        public override void Undo()
        {
            // Remove the duplicated VozacDTO
            success = vozacService.ObrisiVozaca(duplicatedVozacId);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dupliranje vozaca sa ID-jem {originalVozac.Id} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dupliranje vozaca sa ID-jem {originalVozac.Id} je uspesno opozvano!");
        }

        public override void Redo()
        {
            // Re-add the duplicated VozacDTO
            success = vozacService.DodajVozaca(duplicatedVozac);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dupliranje vozaca sa ID-jem {originalVozac.Id} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dupliranje vozaca sa ID-jem {originalVozac.Id} je uspesno ponisteno!");
        }

        public object Clone()
        {
            VozacDTO vozac = new VozacDTO
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

            MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Kloniranje vozaca sa ID-jem {originalVozac.Id} uspesno izvrseno!");

            return vozac;
        }
    }
}
