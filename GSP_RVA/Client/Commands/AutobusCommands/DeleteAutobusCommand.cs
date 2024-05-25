﻿using Client.ViewModel;
using Common.Enums;
using Common.Interfaces;

namespace Client.Commands.AutobusCommands
{
    public class DeleteAutobusCommand
    {
        private readonly int autobusId;
        private readonly IAutobusService autobusService;
        private bool success;

        public DeleteAutobusCommand(IAutobusService autobusService, int autobusId)
        {
            this.autobusService = autobusService;
            this.autobusId = autobusId;
        }

        public void Execute()
        {
            success = autobusService.ObrisiAutobus(autobusId);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje autobusa sa ID-jem {autobusId} nije uspesno izvrseno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje linije sa ID-jem {autobusId} je uspesno izvrseno!");
        }
    }
}
