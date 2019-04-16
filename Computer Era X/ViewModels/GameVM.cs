using Computer_Era_X.DataTypes.Interfaces;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Computer_Era_X.ViewModels
{
    public class GameVM : BindableBase
    {
        public IScenario[] Scenarios => (from t in Assembly.GetExecutingAssembly().GetTypes()
                                         where t.GetInterfaces().Contains(typeof(IScenario))
                                                  && t.GetConstructor(Type.EmptyTypes) != null
                                         select Activator.CreateInstance(t) as IScenario).ToArray();
        public GameVM()
        {
            NewGame = new DelegateCommand(CreateNewGame);
            Exit = new DelegateCommand(ExitApp);
            StartGame = new DelegateCommand(StartNewGame);
        }

        private void CreateNewGame()
        {
            MainMenuVisibility = Visibility.Collapsed;
            NewGameVisibility = Visibility.Visible;
        }
        private void ScenarioSelection()
        {
            if (_selectedScenario == null) { return; }
            if (_selectedScenario.Settings == null || _selectedScenario.Settings.Count == 0) { return; }
            StackPanel stackPanel = new StackPanel();
            for (int i = 0; _selectedScenario.Settings.Count > i; i++)
            {
                Setting setting = _selectedScenario.Settings[i];
                switch (setting.Type)
                {
                    case TypeSettingsData.Bool:
                        {
                            CheckBox checkBox = new CheckBox()
                            {
                                Content = setting.Name,
                                IsChecked = bool.Parse(setting.Value),
                                Tag = i,
                            };
                            stackPanel.Children.Add(checkBox);
                            break;
                        }
                    case TypeSettingsData.Integer:
                        {
                            Label label = new Label()
                            {
                                Content = setting.Name + ": ",
                            };
                            stackPanel.Children.Add(label);
                            TextBox textBox = new TextBox()
                            {
                                Text = setting.Value,
                                Tag = i,
                            };
                            stackPanel.Children.Add(textBox);
                            break;
                        }
                    case TypeSettingsData.Double:
                        goto case TypeSettingsData.Integer;
                    case TypeSettingsData.String:
                        goto case TypeSettingsData.Integer;
                    case TypeSettingsData.List:
                        {
                            Label label = new Label()
                            {
                                Content = setting.Name + ": ",
                            };
                            stackPanel.Children.Add(label);
                            ComboBox comboBox = new ComboBox()
                            {
                                ItemsSource = setting.Values,
                                Tag = i,
                            };
                            stackPanel.Children.Add(comboBox);
                            break;
                        }
                    default:
                        break;
                }
            }
            ScenarioSettings = stackPanel;
        }
        private void ExitApp() => Application.Current.Shutdown();

        private void StartNewGame()
        {
            if (SelectedScenario == null) { Views.MessageBox.Show(Properties.Resources.NewGame, Properties.Resources.NoScenarioSelected, MessageBoxType.Warning); return; }
        }

        private Visibility _mainMenuVisibility = Visibility.Visible;
        private Visibility _newGameVisibility = Visibility.Collapsed;
        private IScenario _selectedScenario;
        private object _scenarioSettings;

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
        public object ScenarioSettings
        {
            get { return _scenarioSettings; }
            set { SetProperty(ref _scenarioSettings, value); }
        }
        public DelegateCommand NewGame { get; }
        public DelegateCommand Exit { get; }
        public DelegateCommand StartGame { get; }
    }
}
