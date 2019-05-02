using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using Computer_Era_X.Models.Systems;
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
            ShowHideMessageBar = new DelegateCommand(ShowHideMessages);
            RemoveMessages = new DelegateCommand(RemoveAllMessages);
            Messages.CollectionChanged += QuantityСhange;
        }

        private void QuantityСhange(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("MessagesCount");
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                BubbleMessage = e.NewItems[0] as Message;
                BubbleMessageVisibility = Visibility.Visible;
                GameEnvironment.Events.Events.Add(new GameEvent("message", GameEnvironment.Events.Timer.DateTime.AddHours(2), Periodicity.Hour, 2, HideBubbleMessage));
            }
        }
        private void HideBubbleMessage(GameEvent @event)
        {
            BubbleMessageVisibility = Visibility.Collapsed;
        }

        private static void GamePause()
        {
            GameEnvironment.Events.Timer.DTimer.Stop();
        }
        private static void GamePlay()
        {
            GameEnvironment.Events.Timer.DTimer.Interval = GameEnvironment.Events.Timer.TimeSpanPlay;
            GameEnvironment.Events.Timer.DTimer.Start();
        }
        private static void GameFastPlay()
        {
            GameEnvironment.Events.Timer.DTimer.Interval = GameEnvironment.Events.Timer.TimeSpanFastPlay;
            GameEnvironment.Events.Timer.DTimer.Start();
        }
        private static void GameVeryFastPlay()
        {
            GameEnvironment.Events.Timer.DTimer.Interval = GameEnvironment.Events.Timer.TimeSpanVeryFastPlay;
            GameEnvironment.Events.Timer.DTimer.Start();
        }
        private void ShowPurse() => Form = new Purse();
        private void ShowMap() => Form = new Map();
        private void ShowHideMessages()
        {
            MessageBarVisibility = MessageBarVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }
        private void RemoveAllMessages()
        {
            Messages.Clear();
            RaisePropertyChanged("MessagesCount");
        }
        public string GameTime => GameEnvironment.Events.Timer.DateTime.ToString("HH:mm \r\n dd.MM.yyyy");
        private UserControl _form = new UserControl();
        private Visibility _messageBarVisibility = Visibility.Collapsed;
        private Message _bubbleMessage;
        private Visibility _bubbleMessageVisibility = Visibility.Collapsed;
        public ObservableCollection<Message> Messages => GameEnvironment.Messages;

        public UserControl Form
        {
            get => _form;
            set => SetProperty(ref _form, value);
        }
        public Visibility MessageBarVisibility
        {
            get => _messageBarVisibility;
            set => SetProperty(ref _messageBarVisibility, value);
        }
        public Message BubbleMessage
        {
            get => _bubbleMessage;
            set => SetProperty(ref _bubbleMessage, value);
        }
        public Visibility BubbleMessageVisibility
        {
            get => _bubbleMessageVisibility;
            set => SetProperty(ref _bubbleMessageVisibility, value);
        }
        public string MessagesCount
        {
            get
            {
                if (GameEnvironment.Messages.Count == 0)
                    return string.Empty;
                return GameEnvironment.Messages.Count < 100 ? GameEnvironment.Messages.Count.ToString() : "99+";
            }
        }

        public DelegateCommand Pause { get; private set; }
        public DelegateCommand Play { get; private set; }
        public DelegateCommand FastPlay { get; private set; }
        public DelegateCommand VeryFastPlay { get; private set; }
        public DelegateCommand Purse { get; private set; }
        public DelegateCommand Map { get; private set; }
        public DelegateCommand ShowHideMessageBar { get; private set; }
        public DelegateCommand RemoveMessages { get; set; }
    }
}
