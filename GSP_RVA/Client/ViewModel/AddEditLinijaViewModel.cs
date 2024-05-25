using Client.Commands;
using Client.Commands.LinijaCommands;
using Client.Commands.Manager;
using Client.Provider;
using Common.DTO;
using Common.Interfaces;
using MVVMLight.Messaging;
using NetworkService.Helpers;

namespace Client.ViewModel
{
    public class AddEditLinijaViewModel : BindableBase
    {
        private LinijaDTO linija;
        public string Mode { get; private set; }
        public bool IsSaved { get; private set; }

        public MyICommand SaveCommand { get; private set; }
        public MyICommand CancelCommand { get; private set; }

        private string oznaka;
        private string polaziste;
        private string odrediste;

        private readonly CommandManager commandManager = new CommandManager();
        private readonly ILinijaService linijaService = ServiceProvider.LinijaService;

        public AddEditLinijaViewModel(LinijaDTO linija, string mode)
        {
            this.linija = linija ?? new LinijaDTO();
            this.Mode = mode;
            ActionButtonText = mode == "ADD" ? "ADD" : "EDIT";
            SaveCommand = new MyICommand(OnSave);
            CancelCommand = new MyICommand(OnCancel);
        }

        public string ActionButtonText { get; private set; }

        public LinijaDTO Linija
        {
            get { return linija; }
            set
            {
                if (linija != value)
                {
                    linija = value;
                    OnPropertyChanged("Linija");
                }
            }
        }

        public string LabelText => Mode == "ADD" ? "DODAJ NOVU LINIJU" : "IZMENI LINIJU";

        private void OnSave()
        {
            if (Mode == "ADD")
            {
                linija.Oznaka = oznaka;
                linija.Polaziste = polaziste;
                linija.Odrediste = odrediste;

                Command add = new AddLinijaCommand(linijaService, linija);
                commandManager.AddAndExecuteCommand(add);
                IsSaved = true;
            }
            else if (Mode == "EDIT")
            {
                // Implementacija logike za uređivanje
                IsSaved = true;
            }

            Messenger.Default.Send('c');
        }

        private void OnCancel()
        {
            Messenger.Default.Send(("gsp", ""));
        }

        public string Oznaka
        {
            get
            {
                return oznaka;
            }

            set
            {
                if(oznaka != value)
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
