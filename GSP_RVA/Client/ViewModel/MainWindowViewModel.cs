using NetworkService.Helpers;
using System.Diagnostics;
using System.Windows;

namespace Client.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public BindableBase currentViewModel;
        public MyICommand LoginCommand { get; private set; }
        public MyICommand<Window> CloseWindow { get; private set; }

        public HomeViewModel HomeViewModel;

        private string username;
        private string password;

        private string errorMessage;

        public MainWindowViewModel()
        {
            CurrentViewModel = this;
            LoginCommand = new MyICommand(OnLogin);
            CloseWindow = new MyICommand<Window>(OnClose);
            HomeViewModel = new HomeViewModel();
        }

        private void OnClose(Window window)
        {
            window.Close();
        }

        private void OnLogin()
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrorMessage = "Username or password must be non empty strings!";
            }
            else
            {
                // UserAPI

                // pass data

                // check is response true
                bool success = true; // replace with api response

                //new GSP().Show();

                if (success)
                {
                    CurrentViewModel = HomeViewModel;
                }
                else
                {
                    ErrorMessage = "Invalid credentials has been provided!";
                }
            }

            Trace.TraceInformation(ErrorMessage);
            Trace.TraceInformation(Username);
            Trace.TraceInformation(Password);
        }

        #region PROPERTY
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

        public BindableBase CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }

            set
            {
                if (currentViewModel != value)
                {
                    currentViewModel = value;
                    OnPropertyChanged("CurrentViewModel");
                }
            }
        }
        #endregion
    }
}
