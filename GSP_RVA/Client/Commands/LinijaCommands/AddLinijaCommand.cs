using Common.DTO;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands.LinijaCommands
{
    public class AddLinijaCommand : Command
    {
        private readonly LinijaDTO linija;
        private readonly ILinijaService linijaService;
        private int id;

        public AddLinijaCommand(ILinijaService linijaService, LinijaDTO linija)
        {
            this.linijaService = linijaService;
            this.linija = linija;
        }

        public override void Execute()
        {
            id = linijaService.DodajLiniju(linija);
        }

        public override void Undo()
        {
            linijaService.ObrisiLiniju(id);
        }

        public override void Redo()
        {
            id = linijaService.DodajLiniju(linija);
        }
    }
}
