﻿using Computer_Era_X.DataTypes.Interfaces;
using Computer_Era_X.DataTypes.Enums;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Computer_Era_X.Models;
using Computer_Era_X.DataTypes.Objects;
using System.Collections.Generic;

namespace Computer_Era_X.ViewModels
{
    public partial class GameVM : BindableBase
    {
        public IScenario[] Scenarios => (from t in Assembly.GetExecutingAssembly().GetTypes()
                                         where t.GetInterfaces().Contains(typeof(IScenario))
                                                  && t.GetConstructor(Type.EmptyTypes) != null
                                         select Activator.CreateInstance(t) as IScenario).ToArray();
        public static GameEnvironment GameEnvironment { get; set; } = new GameEnvironment();
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

            CloseForm = new DelegateCommand(CloseCurrentForm);

            DesktopInit();
            PurseInit();
            MapInit();
            ComponentStoreInit();
            PantryInit();
            BankInit();
            LaborExchangeInit();
            RealEstateAgencyInit();
        }
        
        partial void DesktopInit();
        partial void PurseInit();
        partial void MapInit();
        partial void ComponentStoreInit();
        partial void PantryInit();
        partial void BankInit();
        partial void LaborExchangeInit();
        partial void RealEstateAgencyInit();

        private void CreateNewGame()
        {
            MainMenuVisibility = Visibility.Collapsed;
            NewGameVisibility = Visibility.Visible;
        }
        private void ScenarioSelection()
        {
            if (SelectedScenario?.Settings == null || SelectedScenario.Settings.Count == 0) { ScenarioSettings = null; return; }
            var stackPanel = MenuModel.GetScenarioSettings(SelectedScenario);
            ScenarioSettings = stackPanel;
        }
        private static void ExitApp() => Application.Current.Shutdown();

        private void StartNewGame()
        {
            if (SelectedScenario == null) { Views.MessageBox.Show(Properties.Resources.NewGame, Properties.Resources.NoScenarioSelected, MessageBoxType.Warning); return; }
            if (string.IsNullOrEmpty(PlayerName)) { Views.MessageBox.Show(Properties.Resources.NewGame, Properties.Resources.NoPlayerNameEntered, MessageBoxType.Warning); return; }
            if (ScenarioSettings is StackPanel stackPanel) { MenuModel.SetScenarioSettings(SelectedScenario, stackPanel); }

            //LOAD BASE
            ApplicationContext db = new ApplicationContext();
            db.Items.Load();
            db.BaseCurrencies.Load();
            db.Tariffs.Load();
            db.Services.Load();
            db.Values.Load();
            db.Professions.Load();
            db.Houses.Load();
            GameEnvironment.Items.LoadingItems(db.Items.Local);
            GameEnvironment.Currencies = db.BaseCurrencies.Local;
            GameEnvironment.Services = db.Services.Local;
            GameEnvironment.GameValues.LoadingValues(db.Values.Local);
            GameEnvironment.Professions = db.Professions.Local;
            GameEnvironment.Houses = db.Houses.Local;

            //START GAME
            GameEnvironment.Player.Name = PlayerName;
            GameEnvironment.Scenario = SelectedScenario;
            SelectedScenario.Start(GameEnvironment);
            ShowDesktop();

            //We inform the beginning of the game to other components
            LaborExchangeStartGame();
            RealEstateAgencyStartGame();
        }

        private void ShowDesktop()
        {
            NewGameVisibility = Visibility.Collapsed;
            DesktopVisibility = Visibility.Visible;
        }

        public BaseCurrencies Currency => GameEnvironment.Player.Money[0];
        public List<Service> Services => GameEnvironment.Services.Where(service=> service.IsSystem == false).ToList();
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

        private void CloseCurrentForm() => Form = null;
        public DelegateCommand CloseForm { get; }
    }
}
