using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;

namespace Client.Commands.AutobusCommands
{
    public class AddAutobusCommand : Command
    {
        private readonly AutobusDTO autobus;
        private readonly IAutobusService autobusService;
        private bool success;

        public AddAutobusCommand(IAutobusService autobusService, AutobusDTO autobus)
        {
            this.autobusService = autobusService;
            this.autobus = autobus;
        }

        public override void Execute()
        {
            success = autobusService.DodajAutobus(autobus.Oznaka);

            // Logovanje
            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, "Dodavanje novog autobusa nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {autobus.Id} je uspesno dodat!");
        }

        public override void Undo()
        {
            success = autobusService.ObrisiAutobus(autobus.Id);

            // Logovanje
            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, "Dodavanje novog autobusa nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {autobus.Id} je uspesno dodat!");
        }

        public override void Redo()
        {
            success = autobusService.DodajAutobus(autobus.Oznaka);
        }
    }
}
