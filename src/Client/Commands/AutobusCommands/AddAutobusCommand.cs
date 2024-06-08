using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;

namespace Client.Commands.AutobusCommands
{
    public class AddAutobusCommand : Command
    {
        private readonly AutobusDTO autobus;
        private readonly IAutobusService autobusService;
        private bool success;

        public AddAutobusCommand(IAutobusService autobusService, AutobusDTO autobus)
        {
            this.autobusService = autobusService;
            this.autobus = autobus;
        }

        public override void Execute()
        {
            try
            {
                success = autobusService.DodajAutobus(autobus.Oznaka);

                // Logovanje
                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, "Dodavanje novog autobusa nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Autobus sa oznakom {autobus.Oznaka} je uspesno dodat!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom dodavanja autobusa: {ex.Message}");
            }
        }

        public override void Undo()
        {
            try
            {
                success = autobusService.ObrisiAutobus(autobus.Id);

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dodavanje autobusa sa ID-jem {autobus.Id} nije uspesno opozvano!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dodavanje autobusa sa ID-jem {autobus.Id} je uspesno opozvano!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom opozivanja dodavanja autobusa: {ex.Message}");
            }
        }

        public override void Redo()
        {
            try
            {
                success = autobusService.DodajAutobus(autobus.Oznaka);

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dodavanje autobusa sa ID-jem {autobus.Id} nije uspesno ponisteno!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Dodavanje autobusa sa ID-jem {autobus.Id} je uspesno ponisteno!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greska prilikom ponistavanja dodavanja autobusa: {ex.Message}");
            }
        }
    }
}
