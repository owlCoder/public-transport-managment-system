using Common.DTO;
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

        public EditAutobusCommand(IAutobusService autobusService, AutobusDTO originalAutobus, AutobusDTO newAutobus)
        {
            this.autobusService = autobusService;
            this.originalAutobus = originalAutobus;
            this.newAutobus = newAutobus;
        }

        public override void Execute()
        {
            backupAutobus = autobusService.Procitaj(originalAutobus.Id);
            autobusService.IzmeniAutobus(originalAutobus.Id, newAutobus);
        }

        public override void Undo()
        {
            autobusService.IzmeniAutobus(originalAutobus.Id, backupAutobus);
        }

        public override void Redo()
        {
            autobusService.IzmeniAutobus(originalAutobus.Id, newAutobus);
        }
    }
}
