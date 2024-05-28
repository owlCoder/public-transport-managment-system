using Client.Commands.VozacCommands;
using Client.Provider;
using Common.DTO;
using MVVMLight.Messaging;
using NetworkService.Helpers;

namespace Client.ViewModel
{
    public class EditProfileViewModel : BindableBase
    {
        public string Mode { get; private set; }
        public bool IsSaved { get; private set; }

        public MyICommand SaveCommand { get; private set; }
        public MyICommand CancelCommand { get; private set; }

        public string ime;
        public string prezime;

        private VozacDTO korisnik, original;

        public EditProfileViewModel() 
        {
            SaveCommand = new MyICommand(OnSave);
            CancelCommand = new MyICommand(OnCancel);

            // Ucitavanje podataka o trenutno ulogovanom korisniku
            korisnik = ServiceProvider.VozacService.Procitaj(MainWindowViewModel.CurrentUserId);
            original = ServiceProvider.VozacService.Procitaj(MainWindowViewModel.CurrentUserId);

            // Ucitavanje podataka
            Ime = korisnik.Ime;
            Prezime = korisnik.Prezime;
        }

        public void OnSave()
        {
            try
            {
                // Postavljanje novih podataka
                korisnik.Ime = Ime;
                korisnik.Prezime = Prezime;

                new EditVozacCommand(ServiceProvider.VozacService, original, korisnik).Execute();

                OnCancel();
            }
            catch
            {
                MainWindowViewModel.Logger.Log(Common.Enums.LogTraceLevel.DEBUG, "Nije moguće ažuriranje korisničkog profila!");
            }
        }

        public void OnCancel()
        {
            Messenger.Default.Send(("gsp", ""));

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
    }
}
