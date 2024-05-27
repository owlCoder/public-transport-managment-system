using Client.Commands.AutobusCommands;
using Client.Provider;
using Common.DTO;
using Common.Interfaces;
using MVVMLight.Messaging;
using NetworkService.Helpers;

namespace Client.ViewModel
{
    public class AddEditAutobusViewModel : BindableBase
    {
        private AutobusDTO originalniAutobus, noviAutobus;

        public string Mode { get; private set; }
        public bool IsSaved { get; private set; }

        public MyICommand SaveCommand { get; private set; }
        public MyICommand CancelCommand { get; private set; }

        private string oznaka;

        private readonly IAutobusService autobusService = ServiceProvider.AutobusService;

        public string ActionButtonText { get; private set; }

        public AddEditAutobusViewModel(string mode)
        {
            this.Mode = mode;
            ActionButtonText = mode == "ADD" ? "ADD" : "EDIT";
            SaveCommand = new MyICommand(OnSave);
            CancelCommand = new MyICommand(OnCancel);

            originalniAutobus = autobusService.Procitaj(GSPViewModel.SelectedEntityId);
            noviAutobus = autobusService.Procitaj(GSPViewModel.SelectedEntityId);

            Oznaka = originalniAutobus.Oznaka;


        }

        public AutobusDTO Autobus
        {
            get { return originalniAutobus; }

            set
            {
                if (noviAutobus != value)
                {
                    noviAutobus = value;
                    OnPropertyChanged("Autobus");
                }
            }
        }

        public void OnSave()
        {
            if (Mode == "ADD")
            {
                noviAutobus.Oznaka = oznaka;

                AddAutobusCommand add = new AddAutobusCommand(autobusService, noviAutobus);
                add.Execute();
                IsSaved = true;

                Oznaka = "";
            }
            else if (Mode == "EDIT")
            {
                noviAutobus.Oznaka = oznaka;

                EditAutobusCommand edit = new EditAutobusCommand(autobusService, originalniAutobus, noviAutobus);
                edit.Execute();
                IsSaved = true;

                //dodati poruku

            }

            Messenger.Default.Send('c');

        }

        public void OnCancel()
        {
            Messenger.Default.Send(("gsp", ""));

            // Osvezavanje podataka u tabeli
            Messenger.Default.Send('c');

        }

        public string LabelText => Mode == "ADD" ? "DODAJ NOVi AUTOBUS" : "IZMENI AUTOBUS";


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
    }
}
