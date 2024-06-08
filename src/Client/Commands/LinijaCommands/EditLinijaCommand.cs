using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;

namespace Client.Commands.LinijaCommands
{
    public class EditLinijaCommand : Command
    {
        private readonly LinijaDTO originalLinija;
        private readonly LinijaDTO newLinija;
        private readonly ILinijaService linijaService;
        private LinijaDTO backupLinija;
        private int id;

        public EditLinijaCommand(ILinijaService linijaService, LinijaDTO originalLinija, LinijaDTO newLinija)
        {
            this.linijaService = linijaService;
            this.originalLinija = originalLinija;
            this.newLinija = newLinija;
        }

        public override void Execute()
        {
            backupLinija = linijaService.Procitaj(originalLinija.Id);
            id = linijaService.IzmeniLiniju(originalLinija.Id, newLinija);

            if (id == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena linije sa ID-jem {id} nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Linija sa ID-jem {id} je uspesno izmenjena!");
        }

        public override void Undo()
        {
            id = linijaService.IzmeniLiniju(originalLinija.Id, backupLinija);

            if (id == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena linije sa ID-jem {id} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Izmena linije sa ID-jem {id} je uspesno opozvano!");
        }

        public override void Redo()
        {
            id = linijaService.IzmeniLiniju(originalLinija.Id, newLinija);

            if (id == 0)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena linije sa ID-jem {id} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Izmena linije sa ID-jem {id} je uspesno ponisteno!");
        }
    }
}
