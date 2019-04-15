using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

namespace Computer_Era_X.ViewModels
{
    public class GameVM : BindableBase
    {
        public GameVM()
        {
            NewGame = new DelegateCommand(CreateNewGame);
            Exit = new DelegateCommand(ExitApp);
        }

        private void CreateNewGame()
        {
            MainMenuVisibility = Visibility.Collapsed;
            NewGameVisibility = Visibility.Visible;
        }
        private void ExitApp() => Application.Current.Shutdown();


        private Visibility _mainMenuVisibility = Visibility.Visible;
        private Visibility _newGameVisibility = Visibility.Collapsed;

        public Visibility MainMenuVisibility
        {
            get { return _mainMenuVisibility; }
            set { SetProperty(ref _mainMenuVisibility, value); }
        }
        public Visibility NewGameVisibility
        {
            get { return _newGameVisibility; }
            set { SetProperty(ref _newGameVisibility, value); }
        }

        public DelegateCommand NewGame { get; }
        public DelegateCommand Exit { get; }
    }
}
