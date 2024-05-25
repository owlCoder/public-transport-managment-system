using Common.DTO;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.VozacCommands
{
    class EditVozacCommand : Command
    {
        private readonly VozacDTO originalVozac;
        private readonly VozacDTO newVozac;
        private readonly IVozacService vozacService;
        private VozacDTO backupVozac;

        public EditVozacCommand(IVozacService vozacService, VozacDTO originalVozac, VozacDTO newLinija)
        {
            this.vozacService = vozacService;
            this.originalVozac = originalVozac;
            this.newVozac = newLinija;
        }

        public override void Execute()
        {
            backupVozac = vozacService.Procitaj(originalVozac.Id);
            vozacService.IzmeniVozaca(originalVozac.Id, newVozac);
        }

        public override void Undo()
        {
            vozacService.IzmeniVozaca(originalVozac.Id, backupVozac);
        }

        public override void Redo()
        {
            vozacService.IzmeniVozaca(originalVozac.Id, newVozac);
        }
    }
}
