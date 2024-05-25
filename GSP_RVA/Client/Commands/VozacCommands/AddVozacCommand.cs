using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DTO;
using System.Threading.Tasks;
using Common.Interfaces;
using Client.ViewModel;
using Common.Enums;

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
            vozacService.ObrisiVozaca(vozac.Id);
        }

        public override void Redo()
        {
            success = vozacService.DodajVozaca(vozac);
        }
    }
}
