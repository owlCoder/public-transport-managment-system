using Client.ViewModel;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Client.Commands.VozacCommands
{
    class DuplicateVozac : ICloneable
    {
        private readonly IVozacService vozacService;
        private readonly VozacDTO vozac;
        private bool success;

        public DuplicateVozac(IVozacService vozacService, VozacDTO vozac)
        {
            this.vozacService = vozacService;
            this.vozac = vozac;
        }

        public void Execute()
        {
            try
            {
                // Create a copy of the original VozacDTO
                // Add the duplicated VozacDTO using the service
                success = vozacService.DodajVozaca(vozac);

                if (!success)
                    MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Dupliranje vozača sa ID-jem {vozac.Id} nije uspelo!");
                else
                    MainWindowViewModel.Logger.Log(LogTraceLevel.INFO, $"Vozač sa ID-jem {vozac.Id} je uspešno dupliran!");
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greška prilikom izvršavanja komande dupliranja vozača: {ex.Message}");
            }
        }

        public object Clone()
        {
            try
            {
                VozacDTO v = new VozacDTO
                {
                    Id = 0, // Set ID to 0 or another default value to indicate a new entity
                    Username = vozac.Username,
                    Password = vozac.Password,
                    Ime = vozac.Ime,
                    Prezime = vozac.Prezime,
                    Role = vozac.Role,
                    Oznaka = vozac.Oznaka,
                    Linije = new List<LinijaDTO>(vozac.Linije ?? new List<LinijaDTO>())
                };

                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Kloniranje vozača sa ID-jem {vozac.Id} uspešno izvršeno!");

                return v;
            }
            catch (Exception ex)
            {
                MainWindowViewModel.Logger.Log(LogTraceLevel.ERROR, $"Greška prilikom kloniranja vozača: {ex.Message}");
                return null;
            }
        }
    }
}
