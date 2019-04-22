using System.Windows.Controls;
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

        private void ShowPurse()
        {
            Form = new Purse();
        }

        public string GameTime => GameEnvironment.Events.Timer.DateTime.ToString("HH:mm \r\n dd.MM.yyyy");
        UserControl _form = new UserControl();

        public UserControl Form
        {
            get => _form;
            set => SetProperty(ref _form, value);
        }

        public DelegateCommand Pause { get; private set; }
        public DelegateCommand Play { get; private set; }
        public DelegateCommand FastPlay { get; private set; }
        public DelegateCommand VeryFastPlay { get; private set; }
        public DelegateCommand Purse { get; private set; }
    }
}
