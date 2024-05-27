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

        public EditProfileViewModel() 
        {
            SaveCommand = new MyICommand(OnSave);
            CancelCommand = new MyICommand(OnCancel);
        }

        public void OnSave()
        {

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
