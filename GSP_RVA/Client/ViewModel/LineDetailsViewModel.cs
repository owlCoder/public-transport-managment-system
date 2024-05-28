using Client.Provider;
using Common.DTO;
using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Client.ViewModel
{
    public class LineDetailsViewModel : BindableBase
    {
        public LinijaDTO linija;
        public int id;
        public string oznaka, polaziste, odrediste;

        public LineDetailsViewModel()
        {
            linija = ServiceProvider.LinijaService.Procitaj(GSPViewModel.SelectedEntityId);

            Id = linija.Id;
            Oznaka = linija.Oznaka;
            Polaziste = linija.Polaziste;
            Odrediste = linija.Odrediste;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Oznaka
        {
            get
            {
                return oznaka;
            }

            set
            {
                if (oznaka != value)
                {
                    oznaka = value;
                    OnPropertyChanged("Oznaka");
                }
            }
        }

        public string Polaziste
        {
            get
            {
                return polaziste;
            }

            set
            {
                if (polaziste != value)
                {
                    polaziste = value;
                    OnPropertyChanged("Polaziste");
                }
            }
        }

        public string Odrediste
        {
            get
            {
                return odrediste;
            }

            set
            {
                if (odrediste != value)
                {
                    odrediste = value;
                    OnPropertyChanged("Odrediste");
                }
            }
        }

    }
}
