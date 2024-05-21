using Common.DTO;
using Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Client.Commands.LinijaCommands
{
    public class DuplicateLinija : Command, ICloneable
    {
        private readonly ILinijaService linijaService;
        private readonly LinijaDTO originalLinija;
        private LinijaDTO duplicatedLinija;
        private int duplicatedLinijaId;

        public DuplicateLinija(ILinijaService linijaService, LinijaDTO originalLinija)
        {
            this.linijaService = linijaService ?? throw new ArgumentNullException(nameof(linijaService));
            this.originalLinija = originalLinija ?? throw new ArgumentNullException(nameof(originalLinija));
        }

        public override void Execute()
        {
            // Create a copy of the original LinijaDTO
            duplicatedLinija = (LinijaDTO)Clone();
            // Add the duplicated LinijaDTO using the service
            duplicatedLinijaId = linijaService.DodajLiniju(duplicatedLinija);
        }

        public override void Undo()
        {
            // Remove the duplicated LinijaDTO
            linijaService.ObrisiLiniju(duplicatedLinijaId);
        }

        public override void Redo()
        {
            // Re-add the duplicated LinijaDTO
            duplicatedLinijaId = linijaService.DodajLiniju(duplicatedLinija);
        }

        public object Clone()
        {
            return new LinijaDTO
            {
                // Assuming LinijaDTO has a copy constructor or similar mechanism
                // to create a deep copy of the object
                Id = 0, // Set ID to 0 or another default value to indicate a new entity
                Oznaka = originalLinija.Oznaka,
                Polaziste = originalLinija.Polaziste,
                Odrediste = originalLinija.Odrediste,
                Vozaci = new List<VozacDTO>(originalLinija.Vozaci),
                Autobusi = new List<AutobusDTO>(originalLinija.Autobusi)
            };
        }
    }
}
