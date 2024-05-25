using Client.Commands.Manager;
using Client.Provider;
using Common.DTO;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using System.Collections.ObjectModel;

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

        private bool poPolazistuRadio, poOdredistu;

        public MyICommand EditCommand { get; private set; }
        public MyICommand RefreshCommand { get; private set; }
        public MyICommand LogOutCommand { get; private set; }
        public MyICommand AddCommand { get; private set; }
        public MyICommand DeleteCommand { get; private set; }
        public MyICommand UndoCommand { get; private set; }
        public MyICommand RedoCommand { get; private set; }
        public MyICommand CloneCommand { get; private set; }
        public MyICommand SearchCommand { get; private set; }

        public GSPViewModel()
        {
            EditCommand = new MyICommand(OnEdit);
            RefreshCommand = new MyICommand(OnRefresh);
            LogOutCommand = new MyICommand(OnLogOut);
            AddCommand = new MyICommand(OnAdd);
            DeleteCommand = new MyICommand(OnDelete);
            UndoCommand = new MyICommand(OnUndo);
            RedoCommand = new MyICommand(OnRedo);
            CloneCommand = new MyICommand(OnClone);
            SearchCommand = new MyICommand(OnSearch);

            // Inicijalizacija metode za osvezavanje podataka
            Messenger.Default.Register<char>(this, RefreshData);

            LoadData(); //  učitavanje podataka prilikom inicijalizacije pogleda
        }

        private void RefreshData(char mode = 'c')
        {
            LoadData();
        }

        private void LoadData()
        {
            Linije = new ObservableCollection<LinijaDTO>(ServiceProvider.LinijaService.ProcitajSve());
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
            Messenger.Default.Send(("main", ""));
        }

        private void OnAdd()
        {
            string mode = "ADD";
            Messenger.Default.Send(("addEditLinija", mode));
        }


        private void OnEdit()
        {
            if (SelectedEntityId == 0) // TODO: DODAJ PORUKU NA UI niste odabrali entitet!!!!!!!!!!!
                return;

            string mode = "EDIT";
            Messenger.Default.Send(("addEditLinija", mode));
        }



        private void OnDelete()
        {
            if (SelectedEntity is LinijaDTO)
            {
                // onda znas
            }
            var s = SelectedEntity as LinijaDTO;
            // Implementacija brisanja odabranog elementa
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

        private void OnClone()
        {
            // Implementacija kloniranja podataka
        }

        private void OnSearch()
        {
            // Implementacija pretraživanja
            if (string.IsNullOrEmpty(SearchText))
            {
                // lupii poruku unesi kriteriju
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
    }
}
