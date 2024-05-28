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
        private ObservableCollection<LinijaDTO> linije = new ObservableCollection<LinijaDTO>();
        private ObservableCollection<VozacDTO> vozaci = new ObservableCollection<VozacDTO>();
        private ObservableCollection<AutobusDTO> autobusi = new ObservableCollection<AutobusDTO>();


        public LinijaDTO linija;
        public int id;
        public string oznaka, polaziste, odrediste;


        public LineDetailsViewModel()
        {
            linija = ServiceProvider.LinijaService.Procitaj(GSPViewModel.SelectedEntityId);

      
            Linije.Add(linija);
            DohvatiSveVozace();
            DohvatiAutobuse();
        

        }

        private void DohvatiSveVozace()
        {
            // Dohvaćanje svih vozača preko odgovarajuće usluge (pretpostavka)
            var sviVozaciDTO = ServiceProvider.VozacService.ProcitajSve();

            foreach (var vozac in sviVozaciDTO)
            {
                vozaci.Add(vozac);
            }
        }

        private void DohvatiAutobuse()
        {
            // Dohvaćanje autobusa vezanih za odabranu liniju preko odgovarajuće usluge
            var autobusiDTO = ServiceProvider.AutobusService.ProcitajSve();

            Autobusi.Clear();
            foreach (var autobus in autobusiDTO)
            {
                if(autobus.IdLinije == linija.Id || autobus.IdLinije == null)
                Autobusi.Add(autobus);
            }
        }

      

        public ObservableCollection<LinijaDTO> Linije
        {
            get { return linije; }
            set
            {
                if (linije != value)
                {
                    linije = value;
                    OnPropertyChanged("Linije");
                }
            }
        }

        public ObservableCollection<VozacDTO> Vozaci
        {
            get { return vozaci; }
            set
            {
                if (vozaci != value)
                {
                    vozaci = value;
                    OnPropertyChanged("Vozaci");
                }
            }
        }
        public ObservableCollection<AutobusDTO> Autobusi
        {
            get { return autobusi; }
            set
            {
                if (autobusi != value)
                {
                    autobusi = value;
                    OnPropertyChanged("Autobusi");
                }
            }
        }

    }
}
