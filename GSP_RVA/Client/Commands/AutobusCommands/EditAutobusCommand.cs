using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;

namespace Client.Commands.AutobusCommands
{
    class EditAutobusCommand
    {
        private readonly AutobusDTO originalAutobus;
        private readonly AutobusDTO newAutobus;
        private readonly IAutobusService autobusService;
        private bool success;

        public EditAutobusCommand(IAutobusService autobusService, AutobusDTO originalAutobus, AutobusDTO newAutobus)
        {
            this.autobusService = autobusService;
            this.originalAutobus = originalAutobus;
            this.newAutobus = newAutobus;
        }

        public void Execute()
        {
            success = autobusService.IzmeniAutobus(originalAutobus.Id, newAutobus);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena autobusa sa {originalAutobus.Id} nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {originalAutobus.Id} je uspesno izmenjen!");
        }
    }
}
