using System.Collections.Generic;
using Computer_Era_X.DataTypes.Interfaces;
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

            //_gameEnvironment.Money.PlayerCurrency[0].TopUp(Properties.Resources.MainScenarioPaymentName, Properties.Resources.MainScenarioPaymentInitiator, _gameEnvironment.GameEvents.GameTimer.DateAndTime, 10000);

            //_gameEnvironment.GameEvents.GameTimer.Timer.Start();
        }
        public void GameOver(string cause)
        {
            //_gameEnvironment.Main.CauseText.Text = "Вы проиграли!" + Environment.NewLine + "Причина: " + cause;
            //_gameEnvironment.Main.GameOver.Visibility = Visibility.Visible;
        }
    }
}
