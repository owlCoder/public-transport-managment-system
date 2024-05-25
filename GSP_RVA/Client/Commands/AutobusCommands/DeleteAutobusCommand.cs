using Common.DTO;
using Common.Interfaces;

namespace Client.Commands.AutobusCommands
{
    public class DeleteAutobusCommand : Command
    {
        private readonly int autobusId;
        private readonly IAutobusService autobusService;
        private AutobusDTO backupAutobus;
        private bool success;

        public DeleteAutobusCommand(IAutobusService autobusService, int autobusId)
        {
            this.autobusService = autobusService;
            this.autobusId = autobusId;
        }

        public override void Execute()
        {
            backupAutobus = autobusService.Procitaj(autobusId);
            success = autobusService.ObrisiAutobus(autobusId);
        }

        public override void Undo()
        {
            success = autobusService.DodajAutobus(backupAutobus.Oznaka);
        }

        public override void Redo()
        {
            success = autobusService.ObrisiAutobus(autobusId);
        }
    }
}
