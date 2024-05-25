﻿using Client.Provider;
using MVVMLight.Messaging;
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

        private readonly ServiceProvider provider;

        private GSPViewModel gspViewModel;
        private AddEditLinijaViewModel addEditLinijaViewModel;

        public static int CurrentUserId { get; set; } = 0;

        private string username;
        private string password;
        private string errorMessage;

        public MainWindowViewModel()
        {
            CurrentViewModel = this;
            LoginCommand = new MyICommand(OnLogin);
            CloseWindow = new MyICommand<Window>(OnClose);
            gspViewModel = new GSPViewModel();

            // Register messenger actions
            Messenger.Default.Register<(string viewModelName, string mode)>(this, Change);

            // Ukloni POSLE!!!!!!!!!
            // Ukloni POSLE!!!!!!!!!
            // Ukloni POSLE!!!!!!!!!
            Username = "danijel";
            Password = "danijel";
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
                int id = ServiceProvider.VozacService.Prijava(username, password);
                // pass data

                if (id != 0)
                {
                    // Clear login fields
                    Username = "";
                    Password = "";

                    CurrentViewModel = gspViewModel;
                    CurrentUserId = id;
                    OnPropertyChanged("CurrentViewModel");
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

        #region NAVIGATION
        public void Change((string viewModelName, string mode) message)
        {
            switch (message.viewModelName)
            {
                case "main":
                    CurrentViewModel = this;
                    break;

                case "gsp":
                    CurrentViewModel = gspViewModel;
                    break;

                case "addEditLinija":
                    CurrentViewModel = new AddEditLinijaViewModel(message.mode);
                    break;

                case "profile":
                    CurrentViewModel = new EditProfileViewModel();
                    break;
                // dodaj ostale view modele

                default:
                    CurrentViewModel = this;
                    break;
            }
        }

        protected void ChangeViewModel(BindableBase view_model)
        {
            CurrentViewModel = view_model;
        }
        #endregion
    }
}
