using Client.Commands;
using Client.Commands.LinijaCommands;
using Client.Commands.Manager;
using Client.Provider;
using Client.Views;
using Common.DTO;
using Common.Interfaces;

namespace Client.ViewModel
{
    public class AddEditLinijaViewModel
    {
        private string actionButtonText;

        private readonly CommandManager commandManager = new CommandManager();
        private readonly ILinijaService linijaService = ServiceProvider.LinijaService;

        public AddEditLinijaViewModel()
        {
            // onda znas da je add
            //ActionButtonText = "Add";
        }

        public AddEditLinijaViewModel(LinijaDTO linija)
        {
            // polje klas eisto proerrty
            //TrenutnaLinija = linija;
        }

        public void AddLinija()
        {
            Command add = new AddLinijaCommand(linijaService, new LinijaDTO());
            commandManager.AddAndExecuteCommand(add);
            // resetuj sva polja
            // nalik Username = ""

        }

        public void Edit()
        {
            // edit komanda
        }

        // property
    }
}
