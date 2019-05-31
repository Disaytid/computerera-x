using System.Collections.ObjectModel;
using System.Windows;
using Computer_Era_X.DataTypes.Objects;
using Prism.Commands;

namespace Computer_Era_X.ViewModels
{
    public partial class GameVM
    {
        partial void PurseInit()
        {
            HistoryPanel = new DelegateCommand(ShowOrHideHistoryPanel);
        }

        private void PrintHistory()
        {
            if (SelectedScenario == null) { return; }
            Transactions = SelectedCurrency.TransactionHistory;
        }

        public ObservableCollection<BaseCurrencies> Money => GameEnvironment.Player.Money;
        private BaseCurrencies _selectedCurrency;
        private  Collection<Transaction> _transactions;
        private string _historyPanelWidth = "Auto";
        private Visibility _historyPanelVisibility = Visibility.Collapsed;
        private string _historyPanelButton = "<";

        public BaseCurrencies SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                SetProperty(ref _selectedCurrency, value);
                PrintHistory();
            } 
        }
        public Collection<Transaction> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }
        public string HistoryPanelWidth
        {
            get => _historyPanelWidth;
            set => SetProperty(ref _historyPanelWidth, value);
        }
        public Visibility HistoryPanelVisibility
        {
            get => _historyPanelVisibility;
            set => SetProperty(ref _historyPanelVisibility, value);
        }
        public string HistoryPanelButton
        {
            get => _historyPanelButton;
            set => SetProperty(ref _historyPanelButton, value);
        }

        private void ShowOrHideHistoryPanel()
        {
            if (HistoryPanelWidth == "Auto") //Show
            {
                HistoryPanelWidth = "*";
                HistoryPanelButton = ">";
                HistoryPanelVisibility = Visibility.Visible;

            } else { //Hide
                HistoryPanelWidth = "Auto";
                HistoryPanelButton = "<";
                HistoryPanelVisibility = Visibility.Collapsed;
            }
        }

        public DelegateCommand HistoryPanel { get; private set; } 
    }
}
