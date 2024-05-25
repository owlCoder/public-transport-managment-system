using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;

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
            id = linijaService.DodajLiniju(linija);

            // Logovanje
            if (id == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, "Dodavanje nove linije nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Linija sa ID-jem {id} je uspesno dodata!");
        }

        public override void Undo()
        {
            linijaService.ObrisiLiniju(id);

            if (id == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dodavanje linije sa ID-jem {id} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dodavanje linije sa ID-jem {id} je uspesno opozvano!");
        }

        public override void Redo()
        {
            id = linijaService.DodajLiniju(linija);

            if (id == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dodavanje linije sa ID-jem {id} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dodavanje linije sa ID-jem {id} je uspesno ponisteno!");
        }
    }
}
