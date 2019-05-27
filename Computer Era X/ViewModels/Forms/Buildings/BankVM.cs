using System.Collections.ObjectModel;
using System.Windows;
using Computer_Era_X.DataTypes.Objects;
using Prism.Commands;

namespace Computer_Era_X.ViewModels
{
    public partial class GameVM
    {
        partial void BankInit()
        {
            AddService = new DelegateCommand(AdditionsPanelServices_Show);
        }

        private void AdditionsPanelServices_Show()
        {
            InformationPanelForTheService = Visibility.Collapsed;
            AdditionsPanelServices = Visibility.Visible;
            if (Services.Count >= 1) { BankSelectedService = Services[0]; }
        }

        private void ServiceSelection()
        {
            if (BankSelectedService == null) { return; }
            BankTariffs = BankSelectedService.Tariffs;
            if (BankTariffs.Count >= 1) { BankSelectedTariff = BankTariffs[0]; }
        }

        private Visibility _additionsPanelServices = Visibility.Collapsed;
        private Visibility _informationPanelForTheService = Visibility.Visible;
        private Service _selectedService;
        public ObservableCollection<Tariff> _bankTariffs;
        private Tariff _selectedTariff;

        public Visibility AdditionsPanelServices
        {
            get => _additionsPanelServices;
            set => SetProperty(ref _additionsPanelServices, value);
        }
        public Visibility InformationPanelForTheService
        {
            get => _informationPanelForTheService;
            set => SetProperty(ref _informationPanelForTheService, value);
        }
        public Service BankSelectedService
        {
            get => _selectedService;
            set
            {
                SetProperty(ref _selectedService, value);
                ServiceSelection();
            }
        }

        public ObservableCollection<Tariff> BankTariffs
        {
            get => _bankTariffs;
            set => SetProperty(ref _bankTariffs, value);
        }

        public Tariff BankSelectedTariff
        {
            get => _selectedTariff;
            set
            {
                SetProperty(ref _selectedTariff, value);
            }
        }

        public DelegateCommand AddService { get; private set; }
    }
}
