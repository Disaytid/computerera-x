using Computer_Era_X.DataTypes.Interfaces;
using Computer_Era_X.DataTypes.Enums;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Computer_Era_X.Models;

namespace Computer_Era_X.ViewModels
{
    public class GameVM : BindableBase
    {
        public IScenario[] Scenarios => (from t in Assembly.GetExecutingAssembly().GetTypes()
                                         where t.GetInterfaces().Contains(typeof(IScenario))
                                                  && t.GetConstructor(Type.EmptyTypes) != null
                                         select Activator.CreateInstance(t) as IScenario).ToArray();
        public GameEnvironment GameEnvironment { get; } = new GameEnvironment();
        public GameVM()
        {
            GameEnvironment.Events.Timer.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };

            #region Menu
            NewGame = new DelegateCommand(CreateNewGame);
            Exit = new DelegateCommand(ExitApp);
            #endregion

            #region NewGame
            StartGame = new DelegateCommand(StartNewGame);
            #endregion

            #region Desktop
            Pause = new DelegateCommand(GamePause);
            Play = new DelegateCommand(GamePlay);
            FastPlay = new DelegateCommand(GameFastPlay);
            VeryFastPlay = new DelegateCommand(GameVeryFastPlay);
            #endregion
        }

        private void CreateNewGame()
        {
            MainMenuVisibility = Visibility.Collapsed;
            NewGameVisibility = Visibility.Visible;
        }
        private void ScenarioSelection()
        {
            if (SelectedScenario?.Settings == null || SelectedScenario.Settings.Count == 0) { return; }
            var stackPanel = MenuModel.GetScenarioSettings(SelectedScenario);
            ScenarioSettings = stackPanel;
        }
        private static void ExitApp() => Application.Current.Shutdown();

        private void StartNewGame()
        {
            if (SelectedScenario == null) { Views.MessageBox.Show(Properties.Resources.NewGame, Properties.Resources.NoScenarioSelected, MessageBoxType.Warning); return; }
            if (string.IsNullOrEmpty(PlayerName)) { Views.MessageBox.Show(Properties.Resources.NewGame, Properties.Resources.NoPlayerNameEntered, MessageBoxType.Warning); return; }
            if (ScenarioSettings is StackPanel stackPanel) { MenuModel.SetScenarioSettings(SelectedScenario, stackPanel); }
            SelectedScenario.Start(GameEnvironment);
            ShowDesktop();
        }

        private void ShowDesktop()
        {
            NewGameVisibility = Visibility.Collapsed;
            DesktopVisibility = Visibility.Visible;
        }

        private Visibility _mainMenuVisibility = Visibility.Visible;
        private Visibility _newGameVisibility = Visibility.Collapsed;
        private Visibility _desktopVisibility = Visibility.Collapsed;
        private IScenario _selectedScenario;
        private object _scenarioSettings;
        private string _playerName;

        public Visibility MainMenuVisibility
        {
            get => _mainMenuVisibility;
            set => SetProperty(ref _mainMenuVisibility, value);
        }  
        public Visibility NewGameVisibility
        {
            get => _newGameVisibility;
            set => SetProperty(ref _newGameVisibility, value);
        }
        public Visibility DesktopVisibility
        {
            get => _desktopVisibility;
            set => SetProperty(ref _desktopVisibility, value);
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
        public object ScenarioSettings
        {
            get => _scenarioSettings;
            set => SetProperty(ref _scenarioSettings, value);
        }
        public string PlayerName
        {
            get => _playerName;
            set => SetProperty(ref _playerName, value);
        }
        public DelegateCommand NewGame { get; }
        public DelegateCommand Exit { get; }

        public DelegateCommand StartGame { get; }

        #region MyRegion
        public string GameTime => GameEnvironment.Events.Timer.DateTime.ToString("HH:mm \r\n dd.MM.yyyy");

        private void GamePause()
        {
            GameEnvironment.Events.Timer.DTimer.Stop();
        }
        private void GamePlay()
        {
            GameEnvironment.Events.Timer.DTimer.Interval = GameEnvironment.Events.Timer.TimeSpanPlay;
            GameEnvironment.Events.Timer.DTimer.Start();
        }
        private void GameFastPlay()
        {
            GameEnvironment.Events.Timer.DTimer.Interval = GameEnvironment.Events.Timer.TimeSpanFastPlay;
            GameEnvironment.Events.Timer.DTimer.Start();
        }
        private void GameVeryFastPlay()
        {
            GameEnvironment.Events.Timer.DTimer.Interval = GameEnvironment.Events.Timer.TimeSpanVeryFastPlay;
            GameEnvironment.Events.Timer.DTimer.Start();
        }
        public DelegateCommand Pause { get; }
        public DelegateCommand Play { get; }
        public DelegateCommand FastPlay { get; }
        public DelegateCommand VeryFastPlay { get; }
        #endregion
    }
}
