using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;

namespace Client.Commands.VozacCommands
{
    class EditVozacCommand
    {
        private readonly VozacDTO vozac;
        private readonly IVozacService vozacService;
        private bool success;

        public EditVozacCommand(IVozacService vozacService, VozacDTO originalVozac, VozacDTO newVozac)
        {
            this.vozacService = vozacService;
            vozac = newVozac;
        }

        public void Execute()
        {
            success = vozacService.IzmeniVozaca(vozac.Id, vozac).Id != 0;

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Izmena vozaca sa {vozac.Id} nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Vozac sa ID-jem {vozac.Id} je uspesno izmenjen!");
        }
    }
}
