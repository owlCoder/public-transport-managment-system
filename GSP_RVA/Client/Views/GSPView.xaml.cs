using Client.ViewModel;
using Common.DTO;
using MVVMLight.Messaging;
using System.Windows.Controls;

namespace Client.View
{
    public partial class GSPView : UserControl
    {

        public GSPView()
        {
            InitializeComponent();

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabItem = ((sender as TabControl).SelectedItem as TabItem);
            var name = tabItem.Name;

            switch (name)
            {
                case "linija":
                    GSPViewModel.odabraoVozaca = null;
                    GSPViewModel.odabraoLinija = new LinijaDTO();
                    GSPViewModel.odabraoAutobus = null;
                    break;

                case "vozac":
                    GSPViewModel.odabraoVozaca = new VozacDTO();
                    GSPViewModel.odabraoLinija = null;
                    GSPViewModel.odabraoAutobus = null;
                    break;

                case "autobus":
                    GSPViewModel.odabraoVozaca = null;
                    GSPViewModel.odabraoLinija = null;
                    GSPViewModel.odabraoAutobus = new AutobusDTO();
                    break;

                default:
                    GSPViewModel.odabraoVozaca = null;
                    GSPViewModel.odabraoLinija = null;
                    GSPViewModel.odabraoAutobus = null;
                    break;
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            Messenger.Default.Send(("details", "null"));

        }
    }
}
