using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;

namespace Client.Commands.LinijaCommands
{
    public class AddLinijaCommand : Command
    {
        private readonly LinijaDTO linija;
        private readonly ILinijaService linijaService;
        private int id;

        public AddLinijaCommand(ILinijaService linijaService, LinijaDTO linija)
        {
            this.linijaService = linijaService;
            this.linija = linija;
        }

        public override void Execute()
        {
            try
            {
                id = linijaService.DodajLiniju(linija);

                // Logovanje
                if (id == 0)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, "Dodavanje nove linije nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Linija sa ID-jem {id} je uspesno dodata!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom dodavanja linije: {ex.Message}");
            }
        }

        public override void Undo()
        {
            try
            {
                bool success = linijaService.ObrisiLiniju(id) != 0;

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dodavanje linije sa ID-jem {id} nije uspesno opozvano!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dodavanje linije sa ID-jem {id} je uspesno opozvano!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom opozivanja dodavanja linije: {ex.Message}");
            }
        }

        public override void Redo()
        {
            try
            {
                id = linijaService.DodajLiniju(linija);

                if (id == 0)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dodavanje linije sa ID-jem {id} nije uspesno ponisteno!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dodavanje linije sa ID-jem {id} je uspesno ponisteno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom ponistavanja dodavanja linije: {ex.Message}");
            }
        }
    }
}
