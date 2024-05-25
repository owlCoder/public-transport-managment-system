using Client.ViewModel;
using Common.DTO;
using Common.Enums;
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
        private bool success;

        public EditVozacCommand(IVozacService vozacService, VozacDTO originalVozac, VozacDTO newVozac)
        {
            this.vozacService = vozacService;
            this.originalVozac = originalVozac;
            this.newVozac = newVozac;
        }

        public override void Execute()
        {
            backupVozac = vozacService.Procitaj(originalVozac.Id);
            success = vozacService.IzmeniVozaca(originalVozac.Id, newVozac).Id != 0;

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena vozaca sa {originalVozac.Id} nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Vozac sa ID-jem {originalVozac.Id} je uspesno izmenjen!");
        }

        public override void Undo()
        {
            success = vozacService.IzmeniVozaca(originalVozac.Id, backupVozac).Id != 0;

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena vozaca sa ID-jem {originalVozac.Id} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Izmena vozaca sa ID-jem {originalVozac.Id} je uspesno opozvano!");
        }

        public override void Redo()
        {
            success = vozacService.IzmeniVozaca(originalVozac.Id, newVozac).Id != 0;

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena vozaca sa ID-jem {originalVozac.Id} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Izmena vozaca sa ID-jem {originalVozac.Id} je uspesno ponisteno!");
        }
    }
}
