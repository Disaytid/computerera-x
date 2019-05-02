using System;
using System.Collections.Generic;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Interfaces;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models;

namespace Computer_Era_X.Scenarios
{
    public class Creative : IScenario
    {
        public string Name { get; set; } = Properties.Resources.CreativeScenario;
        public List<Setting> Settings { get; set; } = new List<Setting>();

        GameEnvironment _gameEnvironment;

        public Creative()
        {
            Settings.Add(new Setting(Properties.Resources.Money, TypeSettingsData.Double, "0"));
        }

        public void Start(GameEnvironment gameEnvironment)
        {           
            _gameEnvironment = gameEnvironment;
            double money = Convert.ToDouble(Settings[0].Value);

            _gameEnvironment.Player.Money.Add(new Currency(_gameEnvironment.Currencies[1]));
            _gameEnvironment.Player.Money.Add(new Currency(_gameEnvironment.Currencies[2]));
            _gameEnvironment.Player.Money[0].TopUp(Properties.Resources.CreativeScenarioPaymentName, Properties.Resources.CreativeScenarioPaymentInitiator, _gameEnvironment.Events.Timer.DateTime, money);

            _gameEnvironment.Events.Timer.DTimer.Start();
        }
        public void GameOver(string cause)
        {

        }
    }
}
