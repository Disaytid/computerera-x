using System.Windows;
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
        }

        private Visibility _additionsPanelServices = Visibility.Collapsed;
        private Visibility _informationPanelForTheService = Visibility.Visible;

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

        public DelegateCommand AddService { get; private set; }
    }
}
