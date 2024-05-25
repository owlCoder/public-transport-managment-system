﻿using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;

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

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje vozaca sa ID-jem {vozacId} nije uspesno izvrseno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje vozaca sa ID-jem {vozacId} je uspesno izvrseno!");
        }

        public override void Undo()
        {
            success = vozacService.DodajVozaca(backupVozac);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje vozaca sa ID-jem {vozacId} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje vozaca sa ID-jem {vozacId} je uspesno opozvano!");
        }

        public override void Redo()
        {
            success = vozacService.ObrisiVozaca(vozacId);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje vozaca sa ID-jem {vozacId} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje vozaca sa ID-jem {vozacId} je uspesno ponisteno!");
        }
    }
}
