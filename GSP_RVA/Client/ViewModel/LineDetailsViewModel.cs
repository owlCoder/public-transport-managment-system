using Client.Provider;
using Common.DTO;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using System.Collections.ObjectModel;
using System.Linq;

namespace Client.ViewModel
{
    public class LineDetailsViewModel : BindableBase
    {
        private ObservableCollection<LinijaDTO> linije = new ObservableCollection<LinijaDTO>();
        private ObservableCollection<VozacDTO> vozaci = new ObservableCollection<VozacDTO>();
        private ObservableCollection<AutobusDTO> autobusi = new ObservableCollection<AutobusDTO>();

        public MyICommand SaveChangesCommand { get; set; }
        public MyICommand BackCommand { get; set; }


        public LinijaDTO linija;
        public int id;
        public string oznaka, polaziste, odrediste;


        public LineDetailsViewModel()
        {
            linija = ServiceProvider.LinijaService.Procitaj(GSPViewModel.SelectedEntityId);


            // Dobavljanje podataka za liniju
            Linije.Add(linija);
            Vozaci = new ObservableCollection<VozacDTO>(linije[0].Vozaci);
            Autobusi = new ObservableCollection<AutobusDTO>(Linije[0].Autobusi);

            SaveChangesCommand = new MyICommand(SaveChanges);
            BackCommand = new MyICommand(GoBack);
        }

        private void GoBack()
        {
            Messenger.Default.Send(("gsp", "null"));
        }

        private void SaveChanges()
        {
            // Sacuvaj prvo promene za uparene vozace i liniju koja se trenutno uparuje
            foreach(VozacDTO vozacDTO in Vozaci) 
            {
                if(vozacDTO.IsChecked == true)
                    ServiceProvider.VozaciLinijaService.DodajVozacaNaLiniju(vozacDTO.Id, linije[0].Id);
                else
                    ServiceProvider.VozaciLinijaService.UkloniVozacaNaLiniji(vozacDTO.Id, linije[0].Id);
            }

            // Cuvanje promena za autobuse
            foreach(AutobusDTO autobusDTO in Autobusi)
            {
                // Vezivanje linije za autobus
                if (autobusDTO.IsChecked == true)
                    autobusDTO.IdLinije = Linije[0].Id;
                else
                    autobusDTO.IdLinije = 0; // ukloni bus sa linije

                ServiceProvider.AutobusService.IzmeniAutobus(autobusDTO.Id, autobusDTO);
            }
            
        }

        #region PROPERTIES
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
        #endregion
    }
}
