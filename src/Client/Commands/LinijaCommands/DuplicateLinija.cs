using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Client.Commands.LinijaCommands
{
    public class DuplicateLinija : Command, ICloneable
    {
        private readonly ILinijaService linijaService;
        private readonly LinijaDTO originalLinija;
        private LinijaDTO duplicatedLinija;
        private int duplicatedLinijaId;

        public DuplicateLinija(ILinijaService linijaService, LinijaDTO originalLinija)
        {
            this.linijaService = linijaService;
            this.originalLinija = originalLinija;
        }

        public override void Execute()
        {
            // Create a copy of the original LinijaDTO
            duplicatedLinija = (LinijaDTO)Clone();
            // Add the duplicated LinijaDTO using the service
            duplicatedLinijaId = linijaService.DodajLiniju(duplicatedLinija);

            // Logovanje
            if (duplicatedLinijaId == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dupliranje linije sa ID-jem {originalLinija.Id} nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Linija sa ID-jem {originalLinija.Id} je uspesno duplirana!");
        }

        public override void Undo()
        {
            // Remove the duplicated LinijaDTO
            linijaService.ObrisiLiniju(duplicatedLinijaId);

            if (duplicatedLinijaId == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dupliranje linije sa ID-jem {originalLinija.Id} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dupliranje linije sa ID-jem {originalLinija.Id} je uspesno opozvano!");
        }

        public override void Redo()
        {
            // Re-add the duplicated LinijaDTO
            duplicatedLinijaId = linijaService.DodajLiniju(duplicatedLinija);

            if (duplicatedLinijaId == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dupliranje linije sa ID-jem {originalLinija.Id} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dupliranje linije sa ID-jem {originalLinija.Id} je uspesno ponisteno!");
        }

        public object Clone()
        {
            LinijaDTO linija = new LinijaDTO
            {
                // Assuming LinijaDTO has a copy constructor or similar mechanism
                // to create a deep copy of the object
                Id = 0, // Set ID to 0 or another default value to indicate a new entity
                Oznaka = originalLinija.Oznaka,
                Polaziste = originalLinija.Polaziste,
                Odrediste = originalLinija.Odrediste,
                Vozaci = new List<VozacDTO>(originalLinija.Vozaci ?? new List<VozacDTO>()),
                Autobusi = new List<AutobusDTO>(originalLinija.Autobusi ?? new List<AutobusDTO>())
            };

            MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Kloniranje linije sa ID-jem {originalLinija.Id} uspesno izvrseno!");

            return linija;
        }
    }
}
