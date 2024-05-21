using Common.DTO;
using Common.Interfaces;

namespace Client.Commands.LinijaCommands
{
    public class EditLinijaCommand : Command
    {
        private readonly LinijaDTO originalLinija;
        private readonly LinijaDTO newLinija;
        private readonly ILinijaService linijaService;
        private LinijaDTO backupLinija;

        public EditLinijaCommand(ILinijaService linijaService, LinijaDTO originalLinija, LinijaDTO newLinija)
        {
            this.linijaService = linijaService;
            this.originalLinija = originalLinija;
            this.newLinija = newLinija;
        }

        public override void Execute()
        {
            backupLinija = linijaService.Procitaj(originalLinija.Id);
            linijaService.IzmeniLiniju(originalLinija.Id, newLinija);
        }

        public override void Undo()
        {
            linijaService.IzmeniLiniju(originalLinija.Id, backupLinija);
        }

        public override void Redo()
        {
            linijaService.IzmeniLiniju(originalLinija.Id, newLinija);
        }
    }
}
