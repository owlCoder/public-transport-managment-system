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
        public LinijaDTO linija;
        public int id;
        public string oznaka, polaziste, odrediste;

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

        public LineDetailsViewModel()
        {
            linija = ServiceProvider.LinijaService.Procitaj(GSPViewModel.SelectedEntityId);

      
            Linije.Add(linija);
        }

       

    }
}
