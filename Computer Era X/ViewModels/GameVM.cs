using Computer_Era_X.DataTypes.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Linq;
using System.Reflection;
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
        private void ScenarioSelection() { }
        private void ExitApp() => Application.Current.Shutdown();


        private Visibility _mainMenuVisibility = Visibility.Visible;
        private Visibility _newGameVisibility = Visibility.Collapsed;
        private IScenario _selectedScenario;

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
        public IScenario SelectedScenario
        {
            get => _selectedScenario;
            set
            {
                SetProperty(ref _selectedScenario, value);
                ScenarioSelection();
            }
        }
        public IScenario[] Scenarios => (from t in Assembly.GetExecutingAssembly().GetTypes()
                                         where t.GetInterfaces().Contains(typeof(IScenario))
                                                  && t.GetConstructor(Type.EmptyTypes) != null
                                         select Activator.CreateInstance(t) as IScenario).ToArray();
        public DelegateCommand NewGame { get; }
        public DelegateCommand Exit { get; }
    }
}
