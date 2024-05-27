using Client.Commands;
using Client.Commands.AutobusCommands;
using Client.Commands.LinijaCommands;
using Client.Commands.Manager;
using Client.Commands.VozacCommands;
using Client.Provider;
using Client.Views;
using Common.DTO;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using System.Collections.ObjectModel;
using System.Windows;

namespace Client.ViewModel
{
    public class GSPViewModel : BindableBase
    {
        private CommandManager CommandManager = new CommandManager();

        private ObservableCollection<AutobusDTO> autobusi = new ObservableCollection<AutobusDTO>();
        private ObservableCollection<VozacDTO> vozaci = new ObservableCollection<VozacDTO>();
        private ObservableCollection<LinijaDTO> linije = new ObservableCollection<LinijaDTO>();

        private object selectedEntity;

        private string searchText;

        public static int SelectedEntityId { get; set; } = 0;

        private bool poPolazistuRadio, poOdredistuRadio;

        public MyICommand EditCommand { get; private set; }
        public MyICommand EditProfileCommand { get; private set; }
        public MyICommand RefreshCommand { get; private set; }
        public MyICommand LogOutCommand { get; private set; }
        public MyICommand AddCommand { get; private set; }
        public MyICommand DeleteCommand { get; private set; }
        public MyICommand UndoCommand { get; private set; }
        public MyICommand RedoCommand { get; private set; }
        public MyICommand CloneCommand { get; private set; }
        public MyICommand SearchCommand { get; private set; }
        public MyICommand PoPolazistuCommand { get; private set; }
        public MyICommand PoOdredistuCommand { get; private set; }

        public GSPViewModel()
        {
            EditCommand = new MyICommand(OnEdit);
            EditProfileCommand = new MyICommand(OnEditProfile);
            RefreshCommand = new MyICommand(OnRefresh);
            LogOutCommand = new MyICommand(OnLogOut);
            AddCommand = new MyICommand(OnAdd);
            DeleteCommand = new MyICommand(OnDelete);
            UndoCommand = new MyICommand(OnUndo);
            RedoCommand = new MyICommand(OnRedo);
            CloneCommand = new MyICommand(OnClone);
            SearchCommand = new MyICommand(OnSearch);

            // pretraga
            PoPolazistuCommand = new MyICommand(PoPolazistuPromena);
            PoOdredistuCommand = new MyICommand(PoOdredistuPromena);

            // Inicijalizacija metode za osvezavanje podataka
            Messenger.Default.Register<char>(this, RefreshData);

            LoadData(); //  učitavanje podataka prilikom inicijalizacije pogleda
        }

        private void PoOdredistuPromena()
        {
            PoOdredistuRadio = true;
            PoPolazistuRadio = false;
            SelectedEntityId = 0;
            SelectedEntity = null;
        }

        private void PoPolazistuPromena()
        {
            PoOdredistuRadio = false;
            PoPolazistuRadio = true;
            SelectedEntityId = 0;
            SelectedEntity = null;
        }

        public void OnEditProfile()
        {
            SelectedEntityId = 0;
            SelectedEntity = null;
            Messenger.Default.Send(("profile", "null"));
        }

        private void RefreshData(char mode = 'c')
        {
            LoadData();
        }

        private void LoadData()
        {
            SelectedEntityId = 0;
            SelectedEntity = null;

            Linije = new ObservableCollection<LinijaDTO>(ServiceProvider.LinijaService.ProcitajSve());
            Vozaci = new ObservableCollection<VozacDTO>(ServiceProvider.VozacService.ProcitajSve());
            Autobusi = new ObservableCollection<AutobusDTO>(ServiceProvider.AutobusService.ProcitajSve());
            //kreirati ChannelFactory, pozvati proxy za svaki od servisa 
            //Autobus.Servis.DobaviSve();
        }
        #region
        /*
        private void OnEdit()
        {
            if (SelectedEntity == null)
            {
                MessageBox.Show("Nije odabran entitet za uređivanje.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedEntity is Autobus autobus)
            {
                // Priprema za uređivanje autobusa
                // Na primjer: otvaranje dijaloga za uređivanje autobusa

                EditAutobusViewModel editAutobusViewModel = new EditAutobusViewModel(autobus);
                EditAutobusWindow editAutobusWindow = new EditAutobusWindow(editAutobusViewModel);
        
                if (editAutobusWindow.ShowDialog() == true)
                {
                    // Ako je uređivanje potvrđeno, ažurirajte entitet u bazi podataka
                    autobus = editAutobusViewModel.Autobus; // Ažurirajte lokalni entitet
                    dbContext.SaveChanges(); // Spremi promjene u bazi podataka

                    // Osvježite kolekciju ili dohvatite ponovno podatke iz baze
                    autobus.Clear(); // Očistite kolekciju autobusa
                    foreach (var item in dbContext.Autobusi)
                    {
                        autobus.Add(item); // Dodajte ponovno entitete u kolekciju
                    }

                    OnPropertyChanged(nameof(Autobus)); // Osvježite Binding u korisničkom sučelju
                }
            }
            else if (SelectedEntity is Vozac vozac)
            {
                // Priprema za uređivanje vozača
                // Na primjer: otvaranje dijaloga za uređivanje vozača

                EditVozacViewModel editVozacViewModel = new EditVozacViewModel(vozac);
                EditVozacWindow editVozacWindow = new EditVozacWindow(editVozacViewModel);

                if (editVozacWindow.ShowDialog() == true)
                {
                    // Ako je uređivanje potvrđeno, ažurirajte entitet u bazi podataka
                    vozac = editVozacViewModel.Vozac; // Ažurirajte lokalni entitet
                    dbContext.SaveChanges(); // Spremi promjene u bazi podataka

                    // Osvježite kolekciju ili dohvatite ponovno podatke iz baze
                    vozac.Clear(); // Očistite kolekciju vozača
                    foreach (var item in dbContext.Vozaci)
                    {
                        vozac.Add(item); // Dodajte ponovno entitete u kolekciju
                    }

                    OnPropertyChanged(nameof(Vozac)); // Osvježite Binding u korisničkom sučelju
                }
            }
            else if (SelectedEntity is Linija linija)
            {
                // Priprema za uređivanje linije
                // Na primjer: otvaranje dijaloga za uređivanje linije

                EditLinijaViewModel editLinijaViewModel = new EditLinijaViewModel(linija);
                EditLinijaWindow editLinijaWindow = new EditLinijaWindow(editLinijaViewModel);

                if (editLinijaWindow.ShowDialog() == true)
                {
                    // Ako je uređivanje potvrđeno, ažurirajte entitet u bazi podataka
                    linija = editLinijaViewModel.Linija; // Ažurirajte lokalni entitet
                    dbContext.SaveChanges(); // Spremi promjene u bazi podataka

                    // Osvježite kolekciju ili dohvatite ponovno podatke iz baze
                    linija.Clear(); // Očistite kolekciju linija
                    foreach (var item in dbContext.Linije)
                    {
                        linija.Add(item); // Dodajte ponovno entitete u kolekciju
                    }

                    OnPropertyChanged(nameof(Linija)); // Osvježite Binding u korisničkom sučelju
                }
            }
        }
        */
        #endregion
        private void OnRefresh()
        {
            LoadData(); // Osvježavanje podataka
        }

        private void OnLogOut()
        {
            SelectedEntityId = 0;
            SelectedEntity = null;
            Messenger.Default.Send(("main", ""));
        }

        private void OnAdd()
        {

            if (SelectedEntity is LinijaDTO)
            {
                SelectedEntityId = 0;
                SelectedEntity = null;
                string mode = "ADD";
                Messenger.Default.Send(("addEditLinija", mode));
            }
            else if (SelectedEntity is AutobusDTO)
            {
                // kasnije dodati
                SelectedEntityId = 0;
                SelectedEntity = null;
                string mode = "ADD";
                Messenger.Default.Send(("addEditAutobus", mode));
            }
            else if (SelectedEntity is VozacDTO)
            {
                // kasnije dodati
                SelectedEntityId = 0;
                SelectedEntity = null;
                string mode = "ADD";
                Messenger.Default.Send(("addEditVozac", mode));
            }
            else
            {
                // ispisi gresku!!!!
              
                SelectedEntityId = 0;
                SelectedEntity = null;
            }

        }


        private void OnEdit()
        {
            if (SelectedEntityId == 0) // TODO: DODAJ PORUKU NA UI niste odabrali entitet!!!!!!!!!!!
                return;

            if (SelectedEntity is LinijaDTO)
            {
                string mode = "EDIT";
                Messenger.Default.Send(("addEditLinija", mode));
            }
            else if (SelectedEntity is AutobusDTO)
            {
                string mode = "EDIT";
                Messenger.Default.Send(("addEditAutobus", mode));
            }
            else if (SelectedEntity is VozacDTO)
            {
                string mode = "EDIT";
                Messenger.Default.Send(("addEditVozac", mode));
            }
        }



        private void OnDelete()
        {
            if (SelectedEntity is LinijaDTO)
            {
                Command add = new DeleteLinijaCommand(ServiceProvider.LinijaService, SelectedEntityId);
                new CommandManager().AddAndExecuteCommand(add);
                LoadData();
                SelectedEntityId = 0; // resetovanje odabrane linije
            }
            else if (SelectedEntity is AutobusDTO)
            {
                // kasnije dodati
                DeleteAutobusCommand add = new DeleteAutobusCommand(ServiceProvider.AutobusService, SelectedEntityId);
                add.Execute();
                LoadData();
                SelectedEntityId = 0; // resetovanje odabrane linije
            }
            else if (SelectedEntity is VozacDTO)
            {
                // kasnije dodati
                DeleteVozacCommand add = new DeleteVozacCommand(ServiceProvider.VozacService, SelectedEntityId);
                add.Execute();
                LoadData();
                SelectedEntityId = 0; // resetovanje odabrane linije
            }
            else
            {
                // ispisi gresku dodaj
            }

            SelectedEntityId = 0;
            SelectedEntity = null;
        }

        private void OnUndo()
        {
            // Implementacija undo akcije
            CommandManager.Undo();
            LoadData();
        }

        private void OnRedo()
        {
            // Implementacija redo akcije
            CommandManager.Redo();
            LoadData();
        }

        //srediti za vozac i autobus
        private void OnClone()
        {
            // Implementacija kloniranja podataka
            // Odabrana linija
            LinijaDTO oroginalnaLinija = ServiceProvider.LinijaService.Procitaj(SelectedEntityId);

            Command add = new DuplicateLinija(ServiceProvider.LinijaService, oroginalnaLinija);
            new CommandManager().AddAndExecuteCommand(add);
            LoadData();
        }

        private void OnSearch()
        {
            SelectedEntityId = 0;
            SelectedEntity = null;

            // Implementacija pretraživanja
            if (string.IsNullOrEmpty(SearchText))
            {
                // lupii poruku unesi kriteriju
            }
            else
            {
                Linije = new ObservableCollection<LinijaDTO>(ServiceProvider.LinijaService.Pretraga(poOdredistuRadio, searchText));
            }
        }

        public ObservableCollection<AutobusDTO> Autobusi
        {
            get { return autobusi; }
            set
            {
                if (autobusi != value)
                {
                    autobusi = value;
                    OnPropertyChanged("Autobusi");
                }
            }
        }

        public ObservableCollection<VozacDTO> Vozaci
        {
            get { return vozaci; }
            set
            {
                if (vozaci != value)
                {
                    vozaci = value;
                    OnPropertyChanged("Vozaci");
                }
            }
        }

        public ObservableCollection<LinijaDTO> Linije
        {
            get { return linije; }
            set
            {
                if (linije != value)
                {
                    linije = value;
                    OnPropertyChanged("Linije");
                }
            }
        }

        public object SelectedEntity
        {
            get { return selectedEntity; }
            set
            {
                if (selectedEntity != value)
                {
                    selectedEntity = value;
                    OnPropertyChanged("SelectedEntity");

                    // Podesi Id objekta trenutno odabranog
                    if (selectedEntity is LinijaDTO)
                        SelectedEntityId = (selectedEntity as LinijaDTO).Id;
                    else if (selectedEntity is AutobusDTO)
                        SelectedEntityId = (selectedEntity as AutobusDTO).Id;
                    else if (selectedEntity is VozacDTO)
                        SelectedEntityId = (selectedEntity as VozacDTO).Id;
                    else
                        SelectedEntityId = 0;
                }
            }
        }

        public string SearchText
        {
            get
            {
                return searchText;
            }

            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged("SearchText");
                }
            }
        }

        public bool PoPolazistuRadio
        {
            get
            {
                return poPolazistuRadio;
            }

            set
            {
                if (poPolazistuRadio != value)
                {
                    poPolazistuRadio = value;
                    OnPropertyChanged("PoPolazistuRadio");
                }
            }
        }

        public bool PoOdredistuRadio
        {
            get
            {
                return poOdredistuRadio;
            }

            set
            {
                if (poOdredistuRadio != value)
                {
                    poOdredistuRadio = value;
                    OnPropertyChanged("PoOdredistuRadio");
                }
            }
        }
    }
}
