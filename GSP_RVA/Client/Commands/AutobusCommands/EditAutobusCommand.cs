using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.AutobusCommands
{
    class EditAutobusCommand : Command
    {
        private readonly AutobusDTO originalAutobus;
        private readonly AutobusDTO newAutobus;
        private readonly IAutobusService autobusService;
        private AutobusDTO backupAutobus;
        private bool success;

        public EditAutobusCommand(IAutobusService autobusService, AutobusDTO originalAutobus, AutobusDTO newAutobus)
        {
            this.autobusService = autobusService;
            this.originalAutobus = originalAutobus;
            this.newAutobus = newAutobus;
        }

        public override void Execute()
        {
            backupAutobus = autobusService.Procitaj(originalAutobus.Id);
            success = autobusService.IzmeniAutobus(originalAutobus.Id, newAutobus);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena autobusa sa {originalAutobus.Id} nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {originalAutobus.Id} je uspesno izmenjen!");
        }

        public override void Undo()
        {
            success = autobusService.IzmeniAutobus(originalAutobus.Id, backupAutobus);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena autobusa sa ID-jem {originalAutobus.Id} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Izmena autobusa sa ID-jem {originalAutobus.Id} je uspesno opozvano!");
        }

        public override void Redo()
        {
            success = autobusService.IzmeniAutobus(originalAutobus.Id, newAutobus);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena autobusa sa ID-jem {originalAutobus.Id} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Izmena autobusa sa ID-jem {originalAutobus.Id} je uspesno ponisteno!");
        }
    }
}
