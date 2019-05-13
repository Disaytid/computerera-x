using Prism.Commands;
using System.Windows;

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

        public DelegateCommand UpdatePantryPanel { get; private set; }
    }
}
