using NetworkService.Helpers;
using Service.Database.Context;
using Service.Database.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace Client.ViewModel
{
    public class GSPViewModel : BindableBase
    {
        private ObservableCollection<Autobus> autobusi;
        private ObservableCollection<Vozac> vozaci;
        private ObservableCollection<Linija> linije;

        private DatabaseContext dbContext;
        private object selectedEntity;

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
            dbContext = new DatabaseContext();

            EditCommand = new MyICommand(OnEdit);
            RefreshCommand = new MyICommand(OnRefresh);
            LogOutCommand = new MyICommand(OnLogOut);
            AddCommand = new MyICommand(OnAdd);
            DeleteCommand = new MyICommand(OnDelete);
            UndoCommand = new MyICommand(OnUndo);
            RedoCommand = new MyICommand(OnRedo);
            CloneCommand = new MyICommand(OnClone);
            SearchCommand = new MyICommand(OnSearch);

            LoadData(); //  učitavanje podataka prilikom inicijalizacije pogleda
        }

        private void LoadData()
        {
            autobusi = new ObservableCollection<Autobus>(dbContext.Autobusi);
            vozaci = new ObservableCollection<Vozac>(dbContext.Vozaci);
            linije = new ObservableCollection<Linija>(dbContext.Linije);

            OnPropertyChanged(nameof(Autobusi));
            OnPropertyChanged(nameof(Vozaci));
            OnPropertyChanged(nameof(Linije));
        }

        private void OnEdit()
        {
            // Implementacija za uređivanje odabranog elementa
            if (SelectedEntity is Autobus autobus)
            {
                // Priprema za uređivanje autobusa
                // Na primer: SelectedEntity = odabrani autobus iz kolekcije
            }
            else if (SelectedEntity is Vozac vozac)
            {
                // Priprema za uređivanje vozača
                // Na primer: SelectedEntity = odabrani vozač iz kolekcije
            }
            else if (SelectedEntity is Linija linija)
            {
                // Priprema za uređivanje linije
                // Na primer: SelectedEntity = odabrana linija iz kolekcije
            }
            else
            {
                return;
            }
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
            // Implementacija odjave
            Window currentWindow = Application.Current.MainWindow;
            currentWindow.Close();


            Window mainWindow = new MainWindow();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }

        private void OnAdd()
        {
            // Implementacija dodavanja novog elementa
        }

        private void OnDelete()
        {
            // Implementacija brisanja odabranog elementa
        }

        private void OnUndo()
        {
            // Implementacija undo akcije
        }

        private void OnRedo()
        {
            // Implementacija redo akcije
        }

        private void OnClone()
        {
            // Implementacija kloniranja podataka
        }

        private void OnSearch()
        {
            // Implementacija pretraživanja
        }

        public ObservableCollection<Autobus> Autobusi
        {
            get { return autobusi; }
            set { SetProperty(ref autobusi, value); }
        }

        public ObservableCollection<Vozac> Vozaci
        {
            get { return vozaci; }
            set { SetProperty(ref vozaci, value); }
        }

        public ObservableCollection<Linija> Linije
        {
            get { return linije; }
            set { SetProperty(ref linije, value); }
        }

        public object SelectedEntity
        {
            get { return selectedEntity; }
            set { SetProperty(ref selectedEntity, value); }
        }
    }
}
