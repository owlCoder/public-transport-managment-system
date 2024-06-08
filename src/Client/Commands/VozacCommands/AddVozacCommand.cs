using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;

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
            try
            {
                success = vozacService.DodajVozaca(vozac);

                // Logging
                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, "Dodavanje novog vozača nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Vozač oznake {vozac.Oznaka} je uspešno dodat!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greška prilikom izvršavanja komande dodavanja vozača: {ex.Message}");
            }
        }

        public override void Undo()
        {
            try
            {
                success = vozacService.ObrisiVozaca(vozac.Id);

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Opozivanje dodavanja vozača sa ID-jem {vozac.Id} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Opozivanje dodavanja vozača sa ID-jem {vozac.Id} je uspešno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greška prilikom opozivanja komande dodavanja vozača: {ex.Message}");
            }
        }

        public override void Redo()
        {
            try
            {
                success = vozacService.DodajVozaca(vozac);

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Poništavanje dodavanja vozača sa ID-jem {vozac.Id} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Poništavanje dodavanja vozača sa ID-jem {vozac.Id} je uspešno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greška prilikom ponavljanja komande dodavanja vozača: {ex.Message}");
            }
        }
    }
}
