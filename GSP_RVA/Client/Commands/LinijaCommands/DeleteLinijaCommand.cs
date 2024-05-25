using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;

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
            backupLinija = linijaService.Procitaj(linijaId);
            id = linijaService.ObrisiLiniju(linijaId);

            if (id == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje linije sa ID-jem {id} nije uspesno izvrseno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje linije sa ID-jem {id} je uspesno izvrseno!");
        }

        public override void Undo()
        {
            id = linijaService.DodajLiniju(backupLinija);

            if (id == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje linije sa ID-jem {id} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje linije sa ID-jem {id} je uspesno opozvano!");
        }

        public override void Redo()
        {
            id = linijaService.ObrisiLiniju(linijaId);

            if (id == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje linije sa ID-jem {id} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje linije sa ID-jem {id} je uspesno ponisteno!");
        }
    }
}
