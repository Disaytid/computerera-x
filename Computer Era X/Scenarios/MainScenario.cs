using Computer_Era_X.DataTypes.Interfaces;
using Computer_Era_X.Models;
using System;

namespace Computer_Era_X.Scenarios
{
    class Main : IScenario
    {
        public string Name { get; set; } = Properties.Resources.MainScenario;

        private object main;
        GameEnvironment GameEnvironment;
        public void Start(object sender, GameEnvironment gameEnvironment)
        {
            main = sender;
            GameEnvironment = gameEnvironment;

            //GameEnvironment.Money.PlayerCurrency[0].TopUp(Properties.Resources.MainScenarioPaymentName, Properties.Resources.MainScenarioPaymentInitiator, GameEnvironment.GameEvents.GameTimer.DateAndTime, 10000);

            //GameEnvironment.GameEvents.GameTimer.Timer.Start();
        }
        public void GameOver(string cause)
        {
            //GameEnvironment.Main.CauseText.Text = "Вы проиграли!" + Environment.NewLine + "Причина: " + cause;
            //GameEnvironment.Main.GameOver.Visibility = Visibility.Visible;
        }
    }
}
