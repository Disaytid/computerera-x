using System.Collections.ObjectModel;
using Prism.Commands;
using System.Windows;
using Computer_Era_X.Models;

namespace Computer_Era_X.ViewModels
{
    public partial class GameVM
    {
        partial void PantryInit()
        {
            UpdatePantryPanel = new DelegateCommand(ShowOrHideUpdatePantryPanel);
        }

        private void ShowOrHideUpdatePantryPanel()
        {
            if (UpdatePantryPanelWidth == "Auto") //Show
            {
                UpdatePantryPanelWidth = "*";
                UpdatePantryPanelButton = ">";
                UpdatePantryPanelVisibility = Visibility.Visible;

            }
            else
            { //Hide
                UpdatePantryPanelWidth = "Auto";
                UpdatePantryPanelButton = "<";
                UpdatePantryPanelVisibility = Visibility.Collapsed;
            }
        }

        private string _updatePantryPanelWidth = "Auto";
        private Visibility _updatePantryPanelVisibility = Visibility.Collapsed;
        private string _updatePantryPanelButton = "<";
        private ObservableCollection<InventoryItem> _inventoryItems  = new ObservableCollection<InventoryItem>();

        public string UpdatePantryPanelWidth
        {
            get => _updatePantryPanelWidth;
            set => SetProperty(ref _updatePantryPanelWidth, value);
        }
        public Visibility UpdatePantryPanelVisibility
        {
            get => _updatePantryPanelVisibility;
            set => SetProperty(ref _updatePantryPanelVisibility, value);
        }
        public string UpdatePantryPanelButton
        {
            get => _updatePantryPanelButton;
            set => SetProperty(ref _updatePantryPanelButton, value);
        }

        public ObservableCollection<InventoryItem> InventoryItems
        {
            get
            {
                LoadItemsToCollection();
                return _inventoryItems;
            }
            set => SetProperty(ref _inventoryItems, value);
        }

        private void LoadItemsToCollection()
        {
            
            foreach (var @case in GameEnvironment.Player.Items.CaseCollection)
                _inventoryItems.Add(new InventoryItem(@case, @case.Info(), GameEnvironment.Player.Money[0]));
            foreach (var motherboard in GameEnvironment.Player.Items.MotherboardCollection)
                _inventoryItems.Add(new InventoryItem(motherboard, motherboard.Info(), GameEnvironment.Player.Money[0]));
            foreach (var ram in GameEnvironment.Player.Items.RAMCollection)
                _inventoryItems.Add(new InventoryItem(ram, ram.Info(), GameEnvironment.Player.Money[0]));
            foreach (var psu in GameEnvironment.Player.Items.PSUCollection)
                _inventoryItems.Add(new InventoryItem(psu, psu.Info(), GameEnvironment.Player.Money[0]));
            foreach (var cpu in GameEnvironment.Player.Items.CPUCollection)
                _inventoryItems.Add(new InventoryItem(cpu, cpu.Info(), GameEnvironment.Player.Money[0]));
            foreach (var cpuCooler in GameEnvironment.Player.Items.CPUCoolerCollection)
                _inventoryItems.Add(new InventoryItem(cpuCooler, cpuCooler.Info(), GameEnvironment.Player.Money[0]));
        }

        public DelegateCommand UpdatePantryPanel { get; private set; }
    }
}
