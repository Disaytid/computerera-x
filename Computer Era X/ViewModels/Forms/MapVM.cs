using System;
using System.Windows;
using System.Windows.Controls;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using Computer_Era_X.Models.Systems;
using Computer_Era_X.Views;
using Prism.Commands;
using MessageBox = Computer_Era_X.Views.MessageBox;

namespace Computer_Era_X.ViewModels
{
    public partial class GameVM
    {
        private string _target;
        partial void MapInit()
        {
            _map = new WebBrowser();
            GameMap.Navigate(System.IO.Path.GetFullPath("." + new Uri("pack://application:,,,/Map/index.html").AbsolutePath));
            var mapReader = new MapReader(Move);
            GameMap.ObjectForScripting = mapReader;

            GoTo = new DelegateCommand<string>(GoToPlace);
        }
        private void Move(string obj)
        {
            _target = obj;
            MapVisibility = Visibility.Collapsed;
            MovingMap = new MovingMap();
            MovingVisibility = Visibility.Visible;
        }

        // PROPERTY'S
        private WebBrowser _map;
        private Visibility _mapVisibility = Visibility.Visible;
        private UserControl _movingMap;
        private Visibility _movingVisibility = Visibility.Collapsed;
        private Visibility _moveVisibility = Visibility.Collapsed;
        private double _progressMinimum;
        private double _progressMaximum;
        private double _progressValue;
        private Visibility _transportVisibility = Visibility.Visible;
        public WebBrowser GameMap
        {
            get => _map;
            set => SetProperty(ref _map, value);
        }
        public Visibility MapVisibility
        {
            get => _mapVisibility;
            set => SetProperty(ref _mapVisibility, value);
        }
        public UserControl MovingMap
        {
            get => _movingMap;
            set => SetProperty(ref _movingMap, value);
        }
        public Visibility MovingVisibility
        {
            get => _movingVisibility;
            set => SetProperty(ref _movingVisibility, value);
        }
        public Visibility MoveVisibility
        {
            get => _moveVisibility;
            set => SetProperty(ref _moveVisibility, value);
        }
        public double ProgressMinimum
        {
            get => _progressMinimum;
            set => SetProperty(ref _progressMinimum, value);
        }
        public double ProgressMaximum
        {
            get => _progressMaximum;
            set => SetProperty(ref _progressMaximum, value);
        }
        public double ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        public Visibility TransportVisibility
        {
            get => _transportVisibility;
            set => SetProperty(ref _transportVisibility, value);
        }

        // METHOD'S
        TransitionType _transitionType;
        private void GoToPlace(string transport)
        {
            TransportVisibility = Visibility.Collapsed;
            MoveVisibility = Visibility.Visible;

            switch (transport)
            {
                case "on_foot":
                    _transitionType = TransitionType.OnFoot;
                    Walk();
                    break;
                case "public_transport":
                    _transitionType = TransitionType.ByPublicTransport;
                    GoByPublicTransport();
                    break;
                default:
                    Form = null;
                    break;
            }
        }
        private void Walk()
        {
            var transitionTime = 15;
            const int speed = 6000 / 60; //Meters per minute where 6000 is the pedestrian speed in meters / h, and 60 is the number of minutes per hour
            if (GameEnvironment.Player.House != null) { transitionTime += Convert.ToInt32(Math.Floor(GameEnvironment.Player.House.Distance / (double)speed)); }
            Transition(transitionTime);
        }

        private bool _payment;
        private void GoByPublicTransport()
        {
            var transitionTime = 3;
            const int speed = 40000 / 60; //meters per minute where 40,000 is the speed of transport in meters/h, and 60 is the number of minutes per hour
            var price = Convert.ToInt32(0.15 * GameEnvironment.Player.Money[0].Course);
            if (GameEnvironment.Player.House != null)
            {
                const double fare = 0.1;
                transitionTime += Convert.ToInt32(Math.Floor(GameEnvironment.Player.House.Distance / (double)speed));
                price += Convert.ToInt32(GameEnvironment.Player.House.Distance / 1000.0 * fare * GameEnvironment.Player.Money[0].Course);
            }
            if (MessageBox.Show(Properties.Resources.FarePayment, Properties.Resources.YouWantToBuyATicketFor + " " + price + " " + GameEnvironment.Player.Money[0].Abbreviation + "?", MessageBoxType.ConfirmationWithYesNo) == MessageBoxResult.Yes)
            {
                if (!GameEnvironment.Player.Money[0].Withdraw(Properties.Resources.Pay, GameEnvironment.Player.Name, GameEnvironment.Events.Timer.DateTime, price))
                { MessageBox.Show(Properties.Resources.FarePayment, Properties.Resources.YouDoNotHaveEnoughMoney, MessageBoxType.Information); return; }
                _payment = true;
            } else { _payment = false; }
            Transition(transitionTime);
        }

        private void ShowBuilding()
        {
            switch (_target)
            {
                case "labor_exchange":
                    break;
                case "computer_parts_store":
                    break;
                case "bank":
                    break;
                case "disc_stand":
                    break;
                case "estate_agency":
                    break;
                default:
                    MessageBox.Show(_target);
                    break;
            }
        }

        private double _max = 100;
        private void Transition(int time) //Time in minutes
        {
            time *= 2;

            var periodicity = 1;
            if (time < 100)
            {
                _max = time;
            } else {
                _max = 100;
                periodicity = Convert.ToInt32(Math.Round((double)time / 100));
            }

            ProgressMinimum = 0;
            ProgressMaximum = _max;
            ProgressValue = 0;

            GameEnvironment.Events.Events.Add(new GameEvent("Move", GameEnvironment.Events.Timer.DateTime.AddMinutes(periodicity), Periodicity.Minute, periodicity, Move, true));
        }

        private void Move(GameEvent @event)
        {
            if (Equals(ProgressValue, _max))
            {
                GameEnvironment.Events.Events.Remove(@event);
                EndTransition();
                ShowBuilding();

                MapVisibility = Visibility.Visible;
                MovingVisibility = Visibility.Collapsed;
                TransportVisibility = Visibility.Visible;
                MoveVisibility = Visibility.Collapsed;
            } else {
                ProgressValue += 1;
            }
        }

        private void EndTransition()
        {
            switch (_transitionType)
            {
                case TransitionType.OnFoot:
                    if (GameEnvironment.Random.Next(1, 101) <= 10)
                    {
                        int money = Convert.ToInt32(GameEnvironment.Random.Next(1, 21) / (double)100 * GameEnvironment.Player.Money[0].Course);
                        GameEnvironment.Player.Money[0].TopUp(Properties.Resources.FoundOnTheRoad, GameEnvironment.Player.Name, GameEnvironment.Events.Timer.DateTime, money);
                        GameEnvironment.Messages.Add(new Message(Properties.Resources.ReceiptOfFunds, string.Format(Properties.Resources.GameMessage1, money, GameEnvironment.Player.Money[0].Abbreviation), Icon.Money));
                    }
                    break;
                case TransitionType.ByPublicTransport:
                    if (GameEnvironment.Random.Next(0, 2) == 1)
                    {
                        if (!_payment)
                        {
                            var _fine = 15; //The size of the penalty in the universal game currency
                            if (GameEnvironment.Player.Money[0].Withdraw(Properties.Resources.PenaltyForUnpaidFare, Properties.Resources.BusParkNumberOne, GameEnvironment.Events.Timer.DateTime, _fine * GameEnvironment.Player.Money[0].Course))
                            {
                                GameEnvironment.Messages.Add(new Message(Properties.Resources.BusParkNumberOne, Properties.Resources.YouWereChargedForUnpaidFare, Icon.Info));
                            } else { GameEnvironment.Scenario.GameOver(Properties.Resources.YouCouldNotPayAFineForAnUnpaidFare); }
                        }
                    }
                    break;
            }
        }

        public DelegateCommand<string> GoTo { get; private set; }
    }
}
