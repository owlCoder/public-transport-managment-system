using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;

namespace Client.Commands.VozacCommands
{
    class EditVozacCommand
    {
        private readonly VozacDTO originalVozac;
        private readonly VozacDTO newVozac;
        private readonly IVozacService vozacService;
        private bool success;

        public EditVozacCommand(IVozacService vozacService, VozacDTO originalVozac, VozacDTO newVozac)
        {
            this.vozacService = vozacService;
            this.originalVozac = originalVozac;
            this.newVozac = newVozac;
        }

        public void Execute()
        {
            try
            {
                // Call the service method to update the VozacDTO
                success = vozacService.IzmeniVozaca(originalVozac.Id, newVozac).Id != 0;

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena vozača sa ID-jem {originalVozac.Id} nije uspela!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Vozač sa ID-jem {originalVozac.Id} je uspešno izmenjen!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greška prilikom izvršavanja komande izmene vozača: {ex.Message}");
            }
        }
    }
}
