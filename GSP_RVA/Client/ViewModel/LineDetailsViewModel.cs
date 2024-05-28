using Client.Provider;
using Common.DTO;
using NetworkService.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class LineDetailsViewModel : BindableBase
    {
        LinijaDTO linija;

        public LineDetailsViewModel()
        {
            linija = ServiceProvider.LinijaService.Procitaj(GSPViewModel.SelectedEntityId);
        }
    }
}
