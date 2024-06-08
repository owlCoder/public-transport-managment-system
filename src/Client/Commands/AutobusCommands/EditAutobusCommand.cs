using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;

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
            try
            {
                success = autobusService.IzmeniAutobus(originalAutobus.Id, newAutobus);

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena autobusa sa ID-jem {originalAutobus.Id} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {originalAutobus.Id} je uspesno izmenjen!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom izmene autobusa sa ID-jem {originalAutobus.Id}: {ex.Message}");
            }
        }
    }
}
