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
            try
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
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom dupliranja linije: {ex.Message}");
            }
        }

        public override void Undo()
        {
            try
            {
                bool success = linijaService.ObrisiLiniju(duplicatedLinijaId) != 0;

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Opozivanje dupliranja linije sa ID-jem {duplicatedLinijaId} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Opozivanje dupliranja linije sa ID-jem {duplicatedLinijaId} je uspesno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom opozivanja dupliranja linije: {ex.Message}");
            }
        }

        public override void Redo()
        {
            try
            {
                duplicatedLinijaId = linijaService.DodajLiniju(duplicatedLinija);

                if (duplicatedLinijaId == 0)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Ponistavanje dupliranja linije sa ID-jem {originalLinija.Id} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Ponistavanje dupliranja linije sa ID-jem {originalLinija.Id} je uspesno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom ponistavanja dupliranja linije: {ex.Message}");
            }
        }

        public object Clone()
        {
            try
            {
                LinijaDTO linija = new LinijaDTO
                {
                    Id = 0, // Set ID to 0 or another default value to indicate a new entity
                    Oznaka = originalLinija.Oznaka,
                    Polaziste = originalLinija.Polaziste,
                    Odrediste = originalLinija.Odrediste,
                    Vozaci = new List<VozacDTO>(originalLinija.Vozaci ?? new List<VozacDTO>()),
                    Autobusi = new List<AutobusDTO>(originalLinija.Autobusi ?? new List<AutobusDTO>())
                };

                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Kloniranje linije sa ID-jem {originalLinija.Id} uspesno izvrseno!");

                return linija;
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom kloniranja linije: {ex.Message}");
                return null;
            }
        }
    }
}
