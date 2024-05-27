using Client.Commands.AutobusCommands;
using Client.Commands.Manager;
using Client.Commands.VozacCommands;
using Client.Provider;
using Common.DTO;
using Common.Enums;
using Common.Interfaces;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using Service.Services.LinijaService;
using System.Collections.ObjectModel;
using System.Security.AccessControl;

namespace Client.ViewModel
{
    public class AddEditVozacViewModel : BindableBase
    {
        private VozacDTO originalniVozac, noviVozac;

        public string Mode { get; private set; }
        public bool IsSaved { get; private set; }

        public MyICommand SaveCommand { get; private set; }
        public MyICommand CancelCommand { get; private set; }

        private string ime;
        private string prezime;
        private string username;
        private string password;
        private string oznaka;
        private ObservableCollection<UserRole> uloge;
        private UserRole odabranaRola;
        private string errorMessage;

        private readonly IVozacService vozacService = ServiceProvider.VozacService;

        public string ActionButtonText { get; private set; }

        public AddEditVozacViewModel(string mode)
        {
            this.Mode = mode;
            ActionButtonText = mode == "ADD" ? "ADD" : "EDIT";
            SaveCommand = new MyICommand(OnSave);
            CancelCommand = new MyICommand(OnCancel);

            originalniVozac = vozacService.Procitaj(GSPViewModel.SelectedEntityId);
            noviVozac = vozacService.Procitaj(GSPViewModel.SelectedEntityId);
            Ime = originalniVozac.Ime;
            Prezime = originalniVozac.Prezime;
            Username = originalniVozac.Username;
            Password = originalniVozac.Password;
            Oznaka = originalniVozac.Oznaka;

            Uloge = new ObservableCollection<UserRole>() { UserRole.Admin, UserRole.Vozac };
        }

        public string LabelText => Mode == "ADD" ? "DODAJ NOVOG VOZACA" : "IZMENI VOZACA";


        public ObservableCollection<UserRole> Uloge
        {
            get
            {
                return uloge;
            }

            set
            {
                if(value != uloge)
                {
                    uloge = value;
                    OnPropertyChanged("Uloge");
                }
            }
        }

        public UserRole OdabranaRola
        {
            get
            {
                return odabranaRola;
            }
            set
            {
                if(odabranaRola != value)
                {
                    odabranaRola = value;
                    OnPropertyChanged("OdabranaRola");
                }
            }
        }

        public VozacDTO Vozac
        {
            get { return noviVozac;  }

            set
            {
                if(noviVozac != value)
                {
                    noviVozac = value;
                    OnPropertyChanged("Vozac");
                }
            }
        }

        public void OnSave()
        {
            if(Mode == "ADD")
            {
                noviVozac.Ime = ime;
                noviVozac.Prezime = prezime;
                noviVozac.Username = username;
                noviVozac.Password = password;
                noviVozac.Oznaka = oznaka;
                noviVozac.Role = OdabranaRola;

                AddVozacCommand add = new AddVozacCommand(vozacService, noviVozac);
                add.Execute();
                IsSaved = true;

                //Clear inputs and set message
                Ime = "";
                Prezime = "";
                Username = "";
                Password = "";
                Oznaka = "";
                ErrorMessage = "Vozač je uspešno dodat u bazu podataka!";
            }
            else if(Mode == "EDIT")
            {
                noviVozac.Ime = ime;
                noviVozac.Prezime = prezime;
                noviVozac.Username = username;
                noviVozac.Password = password;
                noviVozac.Oznaka = oznaka;

                EditVozacCommand edit = new EditVozacCommand(vozacService, originalniVozac, noviVozac);
                edit.Execute();
                IsSaved = true;

                //dodati poruku
                ErrorMessage = "Vozač je uspešno izmenjen u bazi podataka!";

            }

            Messenger.Default.Send('c');

        }

        public void OnCancel()
        {
            Messenger.Default.Send(("gsp", ""));

            // Osvezavanje podataka u tabeli
            Messenger.Default.Send('c');

        }

        #region PROPERTY
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }

            set
            {
                if (errorMessage != value)
                {
                    errorMessage = value;
                    OnPropertyChanged("ErrorMessage");
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

        public string Ime
        {
            get
            {
                return ime;
            }

            set
            {
                if (ime != value)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }

        public string Prezime
        {
            get
            {
                return prezime;
            }

            set
            {
                if (prezime != value)
                {
                    prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        #endregion

    }
}
