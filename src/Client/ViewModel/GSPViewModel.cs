using Client.Commands;
using Client.Commands.AutobusCommands;
using Client.Commands.LinijaCommands;
using Client.Commands.Manager;
using Client.Commands.VozacCommands;
using Client.Provider;
using Common.DTO;
using MVVMLight.Messaging;
using NetworkService.Helpers;
using System.Collections.ObjectModel;
using System.Windows;

namespace Client.ViewModel
{
    public class GSPViewModel : BindableBase
    {
        private ObservableCollection<string> logEntries { get; set; }

        private CommandManager CommandManager = new CommandManager();

        private ObservableCollection<AutobusDTO> autobusi = new ObservableCollection<AutobusDTO>();
        private ObservableCollection<VozacDTO> vozaci = new ObservableCollection<VozacDTO>();
        private ObservableCollection<LinijaDTO> linije = new ObservableCollection<LinijaDTO>();

        private object selectedEntity;

        public static VozacDTO odabraoVozaca = null;
        public static AutobusDTO odabraoAutobus = null;
        public static LinijaDTO odabraoLinija = new LinijaDTO();

        private string searchText;

        public static int SelectedEntityId { get; set; } = 0;

        private bool poPolazistuRadio, poOdredistuRadio;

        private Visibility isAdmin { get; set; }


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
            Messenger.Default.Register<string>(this, DodajULog);

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

            PoPolazistuCommand = new MyICommand(PoPolazistuPromena);
            PoOdredistuCommand = new MyICommand(PoOdredistuPromena);

            LogEntries = new ObservableCollection<string>();

            Messenger.Default.Register<char>(this, RefreshData);
            Messenger.Default.Register<VozacDTO>(this, PodesiUlogovanog);

            LoadData();       
        }

        public void PodesiUlogovanog(VozacDTO vozacDTO)
        {
            if (vozacDTO.Id != 0)
                IsAdmin = vozacDTO.Role == Common.Enums.UserRole.Admin ? Visibility.Visible : Visibility.Hidden;
        }

        public void DodajULog(string log)
        {
            LogEntries.Add(log);
            OnPropertyChanged("LogEntries");
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
        }
        #region
        #endregion
        private void OnRefresh()
        {
            LoadData();   
        }

        private void OnLogOut()
        {
            SelectedEntityId = 0;
            SelectedEntity = null;
            Messenger.Default.Send(("main", ""));
        }

        private void OnAdd()
        {
            SelectedEntityId = 0;
            SelectedEntity = null;

            if (odabraoLinija != null)
            {
                string mode = "ADD";
                Messenger.Default.Send(("addEditLinija", mode));
            }
            else if (odabraoAutobus != null)
            {
                string mode = "ADD";
                Messenger.Default.Send(("addEditAutobus", mode));
            }
            else if (odabraoVozaca != null)
            {
                string mode = "ADD";
                Messenger.Default.Send(("addEditVozac", mode));
            }
            else
            {
                SelectedEntityId = 0;
                SelectedEntity = null;
            }

        }


        private void OnEdit()
        {
            if (SelectedEntityId == 0)
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
                SelectedEntityId = 0;    
            }
            else if (SelectedEntity is AutobusDTO)
            {
                DeleteAutobusCommand add = new DeleteAutobusCommand(ServiceProvider.AutobusService, SelectedEntityId);
                add.Execute();
                LoadData();
                SelectedEntityId = 0;    
            }
            else if (SelectedEntity is VozacDTO)
            {
                DeleteVozacCommand add = new DeleteVozacCommand(ServiceProvider.VozacService, SelectedEntityId);
                add.Execute();
                LoadData();
                SelectedEntityId = 0;    
            }
            else
            {
            }

            SelectedEntityId = 0;
            SelectedEntity = null;
        }

        private void OnUndo()
        {
            CommandManager.Undo();
            LoadData();
        }

        private void OnRedo()
        {
            CommandManager.Redo();
            LoadData();
        }

        private void OnClone()
        {
            if(SelectedEntity is LinijaDTO)
            {
                LinijaDTO oroginalnaLinija = ServiceProvider.LinijaService.Procitaj(SelectedEntityId);

                Command add = new DuplicateLinija(ServiceProvider.LinijaService, oroginalnaLinija);
                new CommandManager().AddAndExecuteCommand(add);
            }
            else if(SelectedEntity is VozacDTO)
            {
                VozacDTO origninal = ServiceProvider.VozacService.Procitaj(SelectedEntityId);
                var service = new DuplicateVozac(ServiceProvider.VozacService, origninal);
                var cloned = service.Clone() as VozacDTO;
                new DuplicateVozac(ServiceProvider.VozacService, cloned).Execute();
            }
            else if(SelectedEntity is AutobusDTO)
            {
                AutobusDTO autobusDTO = ServiceProvider.AutobusService.Procitaj(SelectedEntityId);
                var service = new DuplicateAutobus(ServiceProvider.AutobusService, autobusDTO);
                var cloned = service.Clone() as AutobusDTO;
                new DuplicateAutobus(ServiceProvider.AutobusService, cloned).Execute();
            }
            else
            {
            }
            LoadData();
        }

        private void OnSearch()
        {
            SelectedEntityId = 0;
            SelectedEntity = null;

            if (string.IsNullOrEmpty(SearchText))
            {
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

        public ObservableCollection<string> LogEntries
        {
            get
            {
                return logEntries;
            }

            set
            {
                if (logEntries != value)
                {
                    logEntries = value;
                    OnPropertyChanged("LogEntries");
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

        public Visibility IsAdmin
        {
            get
            {
                return isAdmin;
            }

            set
            {
                if (isAdmin != value)

                {
                    isAdmin = value;
                    OnPropertyChanged("IsAdmin");

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
