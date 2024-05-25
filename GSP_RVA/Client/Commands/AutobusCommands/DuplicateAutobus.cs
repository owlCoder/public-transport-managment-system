using Common.DTO;
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
        }

        public override void Undo()
        {
            // Remove the duplicated AutobusDTO
            success = autobusService.ObrisiAutobus(duplicatedAutobusId);
        }

        public override void Redo()
        {
            // Re-add the duplicated AutobusDTO
            success = autobusService.DodajAutobus(duplicatedAutobus.Oznaka);
        }

        public object Clone()
        {
            return new AutobusDTO
            {
                Id = 0,
                Oznaka = originalAutobus.Oznaka,
                IdLinije = originalAutobus.IdLinije,
                Linije = new List<LinijaDTO>(originalAutobus.Linije ?? new List<LinijaDTO>())
            };
        }
    }
}
