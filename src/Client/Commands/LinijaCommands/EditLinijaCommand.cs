using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;

namespace Client.Commands.LinijaCommands
{
    public class EditLinijaCommand : Command
    {
        private readonly LinijaDTO originalLinija;
        private readonly LinijaDTO newLinija;
        private readonly ILinijaService linijaService;
        private LinijaDTO backupLinija;

        public EditLinijaCommand(ILinijaService linijaService, LinijaDTO originalLinija, LinijaDTO newLinija)
        {
            this.linijaService = linijaService;
            this.originalLinija = originalLinija;
            this.newLinija = newLinija;
        }

        public override void Execute()
        {
            try
            {
                backupLinija = linijaService.Procitaj(originalLinija.Id);
                bool success = linijaService.IzmeniLiniju(originalLinija.Id, newLinija) != 0;

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena linije sa ID-jem {originalLinija.Id} nije uspela!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Linija sa ID-jem {originalLinija.Id} je uspesno izmenjena!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom izmene linije sa ID-jem {originalLinija.Id}: {ex.Message}");
            }
        }

        public override void Undo()
        {
            try
            {
                bool success = linijaService.IzmeniLiniju(originalLinija.Id, backupLinija) != 0;

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Opozivanje izmene linije sa ID-jem {originalLinija.Id} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Opozivanje izmene linije sa ID-jem {originalLinija.Id} je uspesno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom opozivanja izmene linije sa ID-jem {originalLinija.Id}: {ex.Message}");
            }
        }

        public override void Redo()
        {
            try
            {
                bool success = linijaService.IzmeniLiniju(originalLinija.Id, newLinija) != 0;

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Ponistavanje izmene linije sa ID-jem {originalLinija.Id} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Ponistavanje izmene linije sa ID-jem {originalLinija.Id} je uspesno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom ponistavanja izmene linije sa ID-jem {originalLinija.Id}: {ex.Message}");
            }
        }
    }
}
