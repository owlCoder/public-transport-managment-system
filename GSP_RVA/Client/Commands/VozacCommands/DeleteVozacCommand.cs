using Common.DTO;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.VozacCommands
{
    class DeleteVozacCommand : Command
    {
        private readonly int vozacId;
        private readonly IVozacService vozacService;
        private VozacDTO backupVozac;
        private bool success;

        public DeleteVozacCommand(IVozacService vozacService, int vozacId)
        {
            this.vozacService = vozacService;
            this.vozacId = vozacId;
        }

        public override void Execute()
        {
            backupVozac = vozacService.Procitaj(vozacId);
            success = vozacService.ObrisiVozaca(vozacId);
        }

        public override void Undo()
        {
            success = vozacService.DodajVozaca(backupVozac);
        }

        public override void Redo()
        {
            success = vozacService.ObrisiVozaca(vozacId);
        }
    }
}
