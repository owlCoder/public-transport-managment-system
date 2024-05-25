using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;

namespace Client.Commands.VozacCommands
{
    class AddVozacCommand : Command
    {
        private readonly VozacDTO vozac;
        private readonly IVozacService vozacService;
        private bool success;

        public AddVozacCommand(IVozacService vozacService, VozacDTO vozac)
        {
            this.vozacService = vozacService;
            this.vozac = vozac;
        }

        public override void Execute()
        {
            success = vozacService.DodajVozaca(vozac);

            // Logovanje
            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, "Dodavanje novog vozaca nije uspelo!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Vozac sa ID-jem {vozac.Id} je uspesno dodata!");
        }

        public override void Undo()
        {
            success = vozacService.ObrisiVozaca(vozac.Id);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dodavanje vozaca sa ID-jem {vozac.Id} nije uspesno opozvano!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dodavanje vozaca sa ID-jem {vozac.Id} je uspesno opozvano!");
        }

        public override void Redo()
        {
            success = vozacService.DodajVozaca(vozac);

            if (!success)
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dodavanje vozaca sa ID-jem {vozac.Id} nije uspesno ponisteno!");
            else
                MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dodavanje vozaca sa ID-jem {vozac.Id} je uspesno ponisteno!");
        }
    }
}
