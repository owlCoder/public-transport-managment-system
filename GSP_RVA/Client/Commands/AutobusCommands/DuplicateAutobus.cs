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
    class DuplicateAutobus : Command, ICloneable
    {
        private readonly IAutobusService autobusService;
        private readonly AutobusDTO originalAutobus;
        private AutobusDTO duplicatedAutobus;
        private int duplicatedAutobusId;
        private bool success;

        public DuplicateAutobus(IAutobusService autobusService, AutobusDTO originalAutobus)
        {
            this.autobusService = autobusService;
            this.originalAutobus = originalAutobus;
        }

        public override void Execute()
        {
            // Create a copy of the original AutobusDTO
            duplicatedAutobus = (AutobusDTO)Clone();
            // Add the duplicated AutobusDTO using the service
            success = autobusService.DodajAutobus(duplicatedAutobus.Oznaka);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dupliranje autobusa sa ID-jem {originalAutobus.Id} nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Autobus sa ID-jem {originalAutobus.Id} je uspesno dupliran!");
        }

        public override void Undo()
        {
            // Remove the duplicated AutobusDTO
            success = autobusService.ObrisiAutobus(duplicatedAutobusId);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dupliranje autobusa sa ID-jem {originalAutobus.Id} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dupliranje autobusa sa ID-jem {originalAutobus.Id} je uspesno opozvano!");
        }

        public override void Redo()
        {
            // Re-add the duplicated AutobusDTO
            success = autobusService.DodajAutobus(duplicatedAutobus.Oznaka);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dupliranje autobusa sa ID-jem {originalAutobus.Id} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dupliranje autobusa sa ID-jem {originalAutobus.Id} je uspesno ponisteno!");
        }

        public object Clone()
        {
            AutobusDTO autobus =  new AutobusDTO
            {
                Id = 0,
                Oznaka = originalAutobus.Oznaka,
                IdLinije = originalAutobus.IdLinije,
                Linije = new List<LinijaDTO>(originalAutobus.Linije ?? new List<LinijaDTO>())
            };

            MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Kloniranje autobusa sa ID-jem {originalAutobus.Id} uspesno izvrseno!");

            return autobus;
        }
    }
}
