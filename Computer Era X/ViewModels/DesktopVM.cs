﻿using System.Windows.Controls;
using Computer_Era_X.Models;
using Computer_Era_X.Views;
using Prism.Commands;

namespace Computer_Era_X.ViewModels
{
    public partial class GameVM
    {
        partial void DesktopInit()
        {
            Pause = new DelegateCommand(GamePause);
            Play = new DelegateCommand(GamePlay);
            FastPlay = new DelegateCommand(GameFastPlay);
            VeryFastPlay = new DelegateCommand(GameVeryFastPlay);
            Purse = new DelegateCommand(ShowPurse);
            Map = new DelegateCommand(ShowMap);
        }

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

        private void ShowPurse() => Form = new Purse();
        private void ShowMap() => Form = new Map();

        public string GameTime => GameEnvironment.Events.Timer.DateTime.ToString("HH:mm \r\n dd.MM.yyyy");
        private UserControl _form = new UserControl();
        private StackPanel _messageBubble;

        public UserControl Form
        {
            get => _form;
            set => SetProperty(ref _form, value);
        }
        public StackPanel MessageBubble
        {
            get => _messageBubble;
            set => SetProperty(ref _messageBubble, value);
        }

        public DelegateCommand Pause { get; private set; }
        public DelegateCommand Play { get; private set; }
        public DelegateCommand FastPlay { get; private set; }
        public DelegateCommand VeryFastPlay { get; private set; }
        public DelegateCommand Purse { get; private set; }
        public DelegateCommand Map { get; private set; }
    }
}
