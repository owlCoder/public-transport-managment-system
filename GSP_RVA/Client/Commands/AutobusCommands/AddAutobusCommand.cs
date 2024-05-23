using Common.DTO;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.AutobusCommands
{
    public class AddAutobusCommand 
    {
        private readonly AutobusDTO autobus;
        private readonly IAutobusService autobusService;

        public AddAutobusCommand(IAutobusService autobusService, AutobusDTO autobus)
        {
            this.autobusService = autobusService;
            this.autobus = autobus;
        }

        public void Execute()
        {
            autobusService.DodajAutobus(autobus.Oznaka);
        }
    }
}
