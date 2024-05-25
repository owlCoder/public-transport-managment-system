using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;

namespace Client.Commands.AutobusCommands
{
    public class DeleteAutobusCommand : Command
    {
        private readonly int autobusId;
        private readonly IAutobusService autobusService;
        private AutobusDTO backupAutobus;
        private bool success;

        public DeleteAutobusCommand(IAutobusService autobusService, int autobusId)
        {
            this.autobusService = autobusService;
            this.autobusId = autobusId;
        }

        public override void Execute()
        {
            backupAutobus = autobusService.Procitaj(autobusId);
            success = autobusService.ObrisiAutobus(autobusId);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje autobusa sa ID-jem {autobusId} nije uspesno izvrseno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje linije sa ID-jem {autobusId} je uspesno izvrseno!");
        }

        public override void Undo()
        {
            success = autobusService.DodajAutobus(backupAutobus.Oznaka);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje autobusa sa ID-jem {autobusId} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje autobusa sa ID-jem {autobusId} je uspesno opozvano!");
        }

        public override void Redo()
        {
            success = autobusService.ObrisiAutobus(autobusId);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje autobusa sa ID-jem {autobusId} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje autobusa sa ID-jem {autobusId} je uspesno ponisteno!");
        }
    }
}
