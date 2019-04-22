using System.Collections.Generic;
using Computer_Era_X.DataTypes.Interfaces;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models;

namespace Computer_Era_X.Scenarios
{
    internal class Main : IScenario
    {
        public string Name { get; set; } = Properties.Resources.MainScenario;
        public List<Setting> Settings { get; set; } = new List<Setting>();

        GameEnvironment _gameEnvironment;

        public void Start(GameEnvironment gameEnvironment)
        {
            _gameEnvironment = gameEnvironment;

            _gameEnvironment.Player.Money.Add(new Currency(_gameEnvironment.Currencies[1]));
            _gameEnvironment.Player.Money.Add(new Currency(_gameEnvironment.Currencies[2]));
            _gameEnvironment.Player.Money[0].TopUp(Properties.Resources.MainScenarioPaymentName, Properties.Resources.MainScenarioPaymentInitiator, _gameEnvironment.Events.Timer.DateTime, 10000);

            _gameEnvironment.Events.Timer.DTimer.Start();
        }
        public void GameOver(string cause)
        {
            //_gameEnvironment.Main.CauseText.Text = "Вы проиграли!" + Environment.NewLine + "Причина: " + cause;
            //_gameEnvironment.Main.GameOver.Visibility = Visibility.Visible;
        }
    }
}
