using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Client.Commands.AutobusCommands
{
    class DuplicateAutobus : ICloneable
    {
        private readonly IAutobusService autobusService;
        private readonly AutobusDTO autobus;
        private bool success;

        public DuplicateAutobus(IAutobusService autobusService, AutobusDTO originalAutobus)
        {
            this.autobusService = autobusService;
            autobus = originalAutobus;
        }

        public void Execute()
        {
            // Add the duplicated AutobusDTO using the service
            success = autobusService.DodajAutobus(autobus.Oznaka);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dupliranje autobusa sa ID-jem {autobus.Id} nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {autobus.Id} je uspesno dupliran!");
        }

        public object Clone()
        {
            AutobusDTO bus = new AutobusDTO
            {
                Id = 0,
                Oznaka = autobus.Oznaka,
                IdLinije = autobus.IdLinije,
                Linije = new List<LinijaDTO>(autobus.Linije ?? new List<LinijaDTO>())
            };

            MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Kloniranje autobusa sa ID-jem {autobus.Id} uspesno izvrseno!");

            return bus;
        }
    }
}
