﻿using Client.Commands;
using Client.Commands.LinijaCommands;
using Client.Commands.Manager;
using Client.Provider;
using Common.DTO;
using Common.Interfaces;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using System.Windows;

namespace Client.ViewModel
{
    public class AddEditLinijaViewModel : BindableBase
    {
        private LinijaDTO originalnaLinija, novaLinija;
        public string Mode { get; private set; }
        public bool IsSaved { get; private set; }

        public MyICommand SaveCommand { get; private set; }
        public MyICommand CancelCommand { get; private set; }

        private string oznaka;
        private string polaziste;
        private string odrediste;
        private string errorMessage;

        private readonly CommandManager commandManager = new CommandManager();
        private readonly ILinijaService linijaService = ServiceProvider.LinijaService;

        public AddEditLinijaViewModel(string mode)
        {
            this.Mode = mode;
            ActionButtonText = mode == "ADD" ? "ADD" : "EDIT";
            SaveCommand = new MyICommand(OnSave);
            CancelCommand = new MyICommand(OnCancel);

            originalnaLinija = linijaService.Procitaj(GSPViewModel.SelectedEntityId);
            novaLinija = linijaService.Procitaj(GSPViewModel.SelectedEntityId);
            Oznaka = originalnaLinija.Oznaka;
            Polaziste = originalnaLinija.Polaziste;
            Odrediste = originalnaLinija.Odrediste;
        }

        public string ActionButtonText { get; private set; }

        public LinijaDTO Linija
        {
            get { return novaLinija; }
            set
            {
                if (novaLinija != value)
                {
                    novaLinija = value;
                    OnPropertyChanged("Linija");
                }
            }
        }

        public string LabelText => Mode == "ADD" ? "DODAJ NOVU LINIJU" : "IZMENI LINIJU";

        private void OnSave()
        {
            if (Mode == "ADD")
            {
                novaLinija.Oznaka = oznaka;
                novaLinija.Polaziste = polaziste;
                novaLinija.Odrediste = odrediste;

                Command add = new AddLinijaCommand(linijaService, novaLinija);
                commandManager.AddAndExecuteCommand(add);
                IsSaved = true;

                Oznaka = "";
                Polaziste = "";
                Odrediste = "";
                Messenger.Default.Send(("gsp", ""));
            }
            else if (Mode == "EDIT")
            {
                novaLinija.Oznaka = oznaka;
                novaLinija.Polaziste = polaziste;
                novaLinija.Odrediste = odrediste;

                MessageBox.Show(" Propose your changes?", "Entity has been changed", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                Command add = new EditLinijaCommand(linijaService, originalnaLinija, novaLinija);
                commandManager.AddAndExecuteCommand(add);
                IsSaved = true;

                IsSaved = true;
                Messenger.Default.Send(("gsp", ""));
            }

            Messenger.Default.Send('c');
        }

        private void OnCancel()
        {
            Messenger.Default.Send(("gsp", ""));

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
        #endregion
    }
}
