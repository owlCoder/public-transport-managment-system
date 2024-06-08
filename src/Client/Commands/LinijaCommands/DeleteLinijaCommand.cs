using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;

namespace Client.Commands.LinijaCommands
{
    public class DeleteLinijaCommand : Command
    {
        private readonly int linijaId;
        private readonly ILinijaService linijaService;
        private LinijaDTO backupLinija;
        private int id;

        public DeleteLinijaCommand(ILinijaService linijaService, int linijaId)
        {
            this.linijaService = linijaService;
            this.linijaId = linijaId;
        }

        public override void Execute()
        {
            try
            {
                backupLinija = linijaService.Procitaj(linijaId);
                bool success = linijaService.ObrisiLiniju(linijaId) != 0;

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje linije sa ID-jem {linijaId} nije uspesno izvrseno!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje linije sa ID-jem {linijaId} je uspesno izvrseno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom brisanja linije sa ID-jem {linijaId}: {ex.Message}");
            }
        }

        public override void Undo()
        {
            try
            {
                id = linijaService.DodajLiniju(backupLinija);

                if (id == 0)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Opozivanje brisanja linije sa ID-jem {linijaId} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Opozivanje brisanja linije sa ID-jem {linijaId} je uspesno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom opozivanja brisanja linije sa ID-jem {linijaId}: {ex.Message}");
            }
        }

        public override void Redo()
        {
            try
            {
                bool success = linijaService.ObrisiLiniju(linijaId) != 0;

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Ponistavanje brisanja linije sa ID-jem {linijaId} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Ponistavanje brisanja linije sa ID-jem {linijaId} je uspesno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom ponistavanja brisanja linije sa ID-jem {linijaId}: {ex.Message}");
            }
        }
    }
}
