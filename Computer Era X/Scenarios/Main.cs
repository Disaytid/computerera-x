using System.Collections.Generic;
using Computer_Era_X.DataTypes.Interfaces;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Views;

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

            _gameEnvironment.Player.Money.Add(new BaseCurrencies(_gameEnvironment.Currencies[1]));
            _gameEnvironment.Player.Money.Add(new BaseCurrencies(_gameEnvironment.Currencies[2]));
            _gameEnvironment.Player.Money[0].TopUp(Properties.Resources.MainScenarioPaymentName, Properties.Resources.MainScenarioPaymentInitiator, _gameEnvironment.Events.Timer.DateTime, 10000);

            _gameEnvironment.Events.Events.Add(new GameEvent("CourseСhange", _gameEnvironment.Events.Timer.DateTime.AddDays(7), Periodicity.Week, 1, CourseСhange, true));

            _gameEnvironment.Events.Timer.DTimer.Start();
        }

        private void CourseСhange(GameEvent @event)
        {
            foreach (BaseCurrency currency in _gameEnvironment.Currencies)
            {
                int _interest = _gameEnvironment.Random.Next(1, 4);
                int _direction = _gameEnvironment.Random.Next(0, 2);
                double _accruedInterest = currency.Course * _interest / 100;
                double _course = currency.Course + (_direction == 0 ? -_accruedInterest : _accruedInterest);
                currency.Course = _course;

                foreach (BaseCurrencies playerCurrency in _gameEnvironment.Player.Money)
                    if (playerCurrency.ID == currency.ID) { playerCurrency.Course = _course; break; }
            }
        }

        public void GameOver(string cause)
        {
            //_gameEnvironment.Main.CauseText.Text = "Вы проиграли!" + Environment.NewLine + "Причина: " + cause;
            //_gameEnvironment.Main.GameOver.Visibility = Visibility.Visible;
        }
    }
}
