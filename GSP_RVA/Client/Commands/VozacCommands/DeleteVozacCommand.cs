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

        public DeleteVozacCommand(IVozacService vozacService, int vozacId)
        {
            this.vozacService = vozacService;
            this.vozacId = vozacId;
        }

        public override void Execute()
        {
            backupVozac = vozacService.Procitaj(vozacId);
            vozacService.ObrisiVozaca(vozacId);
        }

        public override void Undo()
        {
            vozacService.DodajVozaca(backupVozac);
        }

        public override void Redo()
        {
            vozacService.ObrisiVozaca(vozacId);
        }
    }
}
