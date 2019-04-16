using Computer_Era_X.DataTypes.Interfaces;
using Computer_Era_X.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Computer_Era_X.Scenarios
{
    public class Main : IScenario
    {
        public string Name { get; set; } = Properties.Resources.MainScenario;
        public List<Setting> Settings { get; set; } = new List<Setting>();

        public Main()
        {
            Settings.Add(new Setting("Денег", DataTypes.Enums.TypeSettingsData.Integer, "100"));
            Settings.Add(new Setting("Текст", DataTypes.Enums.TypeSettingsData.String, "Ла ла ла"));
        }

        GameEnvironment GameEnvironment;
        public void Start(GameEnvironment gameEnvironment)
        {
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
