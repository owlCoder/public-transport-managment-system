using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;

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
            try
            {
                backupVozac = vozacService.Procitaj(vozacId);
                success = vozacService.ObrisiVozaca(vozacId);

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Brisanje vozača sa ID-jem {vozacId} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Brisanje vozača sa ID-jem {vozacId} je uspešno izvršeno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greška prilikom izvršavanja komande brisanja vozača: {ex.Message}");
            }
        }

        public override void Undo()
        {
            try
            {
                success = vozacService.DodajVozaca(backupVozac);

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Opozivanje brisanja vozača sa ID-jem {vozacId} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Opozivanje brisanja vozača sa ID-jem {vozacId} je uspešno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greška prilikom opozivanja komande brisanja vozača: {ex.Message}");
            }
        }

        public override void Redo()
        {
            try
            {
                success = vozacService.ObrisiVozaca(vozacId);

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Ponavljanje brisanja vozača sa ID-jem {vozacId} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Ponavljanje brisanja vozača sa ID-jem {vozacId} je uspešno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greška prilikom ponavljanja komande brisanja vozača: {ex.Message}");
            }
        }
    }
}
