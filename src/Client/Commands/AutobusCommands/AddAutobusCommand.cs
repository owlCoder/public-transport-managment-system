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
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Autobus sa oznakom {autobus.Oznaka} je uspesno dodat!");
        }

        public override void Undo()
        {
            success = autobusService.ObrisiAutobus(autobus.Id);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dodavanje autobusa sa ID-jem {autobus.Id} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dodavanje autobusa sa ID-jem {autobus.Id} je uspesno opozvano!");
        }

        public override void Redo()
        {
            success = autobusService.DodajAutobus(autobus.Oznaka);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dodavanje autobusa sa ID-jem {autobus.Id} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dodavanje autobusa sa ID-jem {autobus.Id} je uspesno ponisteno!");
        }
    }
}
