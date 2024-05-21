using Common.DTO;
using Common.Interfaces;

namespace Client.Commands.LinijaCommands
{
    public class DeleteLinijaCommand : Command
    {
        private readonly int linijaId;
        private readonly ILinijaService linijaService;
        private LinijaDTO backupLinija;

        public DeleteLinijaCommand(ILinijaService linijaService, int linijaId)
        {
            this.linijaService = linijaService;
            this.linijaId = linijaId;
        }

        public override void Execute()
        {
            backupLinija = linijaService.Procitaj(linijaId);
            linijaService.ObrisiLiniju(linijaId);
        }

        public override void Undo()
        {
            linijaService.DodajLiniju(backupLinija);
        }

        public override void Redo()
        {
            linijaService.ObrisiLiniju(linijaId);
        }
    }
}
