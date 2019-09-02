using Computer_Era_X.Converters;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models;
using Computer_Era_X.Models.Systems;
using Computer_Era_X.Properties;
using Computer_Era_X.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using MessageBox = Computer_Era_X.Views.MessageBox;

namespace Computer_Era_X.ViewModels
{
    partial class GameVM
    {
        partial void RealEstateAgencyInit()
        {
            REABuy = new DelegateCommand<HousingSale>(BuyHouse);
            REARent = new DelegateCommand<HousingSale>(RentHouse);
            REABuyCredit = new DelegateCommand<HousingSale>(BuyCreditHouse);
        }

        private void RealEstateAgencyStartGame()
        {
            foreach (House house in GameEnvironment.Houses)
            {
                double convertedValue = house.Price * Currency.Course;
                double convertedRentalValue = house.Rent * Currency.Course;
                Houses.Add(new HousingSale(house, Currency, convertedValue, convertedRentalValue));
            }
        }

        private void RentHouse(HousingSale house)
        {
            if (GameEnvironment.Player.House != null)
            {
                if (GameEnvironment.Player.House.IsRent == 1 & GameEnvironment.Player.House.IsRentedOut & house.Id == GameEnvironment.Player.House.Id) { _ = MessageBox.Show(Resources.RentN, Resources.GameMessage22, MessageBoxType.Warning); return; }
                if (GameEnvironment.Player.House.IsPurchased) { if (MessageBox.Show(Resources.RentN, Resources.GameMessage21, MessageBoxType.ConfirmationWithYesNo) == MessageBoxResult.No) { return; } }
                if (GameEnvironment.Player.House.IsPurchasedOnCredit) { _ = MessageBox.Show(Resources.RentN, Resources.GameMessage23, MessageBoxType.Information); return; }
            }

            Service rentService = null;
            Service communalService = null;
            for (int i = 0; i <= GameEnvironment.Services.Count - 1; i++)
            {
                if (GameEnvironment.Services[i].SystemName == "rent") { rentService = GameEnvironment.Services[i]; }
                else if (GameEnvironment.Services[i].SystemName == "communal_payments") { communalService = GameEnvironment.Services[i]; }
                if (rentService != null && communalService != null) { break; }
            }
            if (rentService == null) { MessageBox.Show(Resources.RentN, Resources.ErroreCodeNoRentalServiceFound, MessageBoxType.Error); return; }
            if (communalService == null) { MessageBox.Show(Resources.RentN, Resources.ErroreCodeCommunalPaymentServiceNotFound, MessageBoxType.Error); return; }
            PropertyForSale();

            int id = GameEnvironment.Player.Tariffs.Count;
            Tariff playerRTariff = new Tariff
            {
                ID = id + 1,
                Name = house.Name,
                BaseCurrency = GameEnvironment.Currencies[1],
                Coefficient = 0,
                MinSum = house.Rent,
                MaxSum = house.Rent,
                Periodicity = 1, //enum Periodicity mounth id=1
                PeriodicityValue = 1,
                TermUnit = 1,
                MinTerm = 1,
                MaxTerm = 1,
            };
            Tariff playerCTariff = new Tariff
            {
                ID = id + 2,
                Name = house.Name,
                BaseCurrency = GameEnvironment.Currencies[1],
                Coefficient = 0,
                MinSum = house.CommunalPayments,
                MaxSum = house.CommunalPayments,
                Periodicity = 1, //enum Periodicity mounth id=1
                PeriodicityValue = 1,
                TermUnit = 1,
                MinTerm = 1,
                MaxTerm = 1,
            };

            PlayerTariff playerRentTariff = new PlayerTariff(playerRTariff, rentService, house.Rent * Currency.Course, 1, GameEnvironment.Events.Timer.DateTime);
            PlayerTariff playerCommunalTariff = new PlayerTariff(playerCTariff, communalService, house.CommunalPayments * Currency.Course, 1, GameEnvironment.Events.Timer.DateTime);
            GameEnvironment.Player.Tariffs.Add(playerRentTariff);
            GameEnvironment.Player.Tariffs.Add(playerCommunalTariff);
            GameEnvironment.Player.House = new PlayerHouse(house, playerCommunalTariff, true, false, false, playerRentTariff);

            Periodicity periodicity = (Periodicity)Enum.GetValues(typeof(Periodicity)).GetValue(playerRentTariff.Periodicity);
            GameEnvironment.Events.Events.Add(new GameEvent(rentService.ID + ":" + playerRentTariff.ID + ":" + (house.Rent * playerRentTariff.BaseCurrency.Course),
                                PeriodicityConverter.GetDateTimeFromPeriodicity(GameEnvironment.Events.Timer.DateTime, playerRentTariff.Periodicity, playerRentTariff.PeriodicityValue),
                                periodicity, playerRentTariff.PeriodicityValue, RentalPayment, true));
            DateTime date = GameEnvironment.Events.Timer.DateTime; date = new DateTime(date.Year, date.Month, 15, date.Hour, date.Minute, date.Second);
            periodicity = (Periodicity)Enum.GetValues(typeof(Periodicity)).GetValue(playerCommunalTariff.Periodicity);
            GameEnvironment.Events.Events.Add(new GameEvent(communalService.ID + ":" + playerCommunalTariff.ID + ":" + (house.CommunalPayments * playerCommunalTariff.BaseCurrency.Course),
                    PeriodicityConverter.GetDateTimeFromPeriodicity(date, playerCommunalTariff.Periodicity, playerCommunalTariff.PeriodicityValue),
                    periodicity, playerCommunalTariff.PeriodicityValue, CommunalPayment, true));
            GameEnvironment.Messages.Add(new Message(Resources.RealEstateAgencyFullName, string.Format(Resources.YouRentedX, house.Name), Icon.Info));
        }

        private void BuyHouse(HousingSale house)
        {
            if (GameEnvironment.Player.House != null)
            {
                if (GameEnvironment.Player.House.IsRent == 1 & GameEnvironment.Player.House.IsRentedOut & house.Id == GameEnvironment.Player.House.Id) { if (MessageBox.Show(Resources.Buying, Resources.GameMessage24, MessageBoxType.ConfirmationWithYesNo) == System.Windows.MessageBoxResult.No) { return; } }
                if ((GameEnvironment.Player.House.IsPurchased || GameEnvironment.Player.House.IsPurchasedOnCredit) && house.Id == GameEnvironment.Player.House.Id) { _ = MessageBox.Show(Resources.Buying, Resources.GameMessage25, MessageBoxType.Information); return; }
                if (GameEnvironment.Player.House.IsPurchasedOnCredit && house.Id != GameEnvironment.Player.House.Id) { _ = MessageBox.Show(Resources.Buying, Resources.GameMessage26, MessageBoxType.Information); return; }
                if (GameEnvironment.Player.House.IsPurchased && house.Id != GameEnvironment.Player.House.Id) { if (MessageBox.Show(Resources.Buying, Resources.GameMessage27, MessageBoxType.ConfirmationWithYesNo) == System.Windows.MessageBoxResult.No) { return; } }
            }

            Service communalService = null;
            for (int i = 0; i <= GameEnvironment.Services.Count - 1; i++)
            {
                if (GameEnvironment.Services[i].SystemName == "communal_payments") { communalService = GameEnvironment.Services[i]; break; }
            }
            if (communalService == null) { MessageBox.Show(Resources.Buying, Resources.ErroreCodeCommunalPaymentServiceNotFound, MessageBoxType.Error); return; }
            PropertyForSale();

            double price = house.Price * GameEnvironment.Player.Money[0].Course;
            if (GameEnvironment.Player.Money[0].Withdraw(string.Format(Resources.BuyingAX, house.Name), Resources.RealEstateAgencyFullName, GameEnvironment.Events.Timer.DateTime, price))
            {
                int id = GameEnvironment.Player.Tariffs.Count;
                Tariff playerTariff = new Tariff
                {
                    ID = id + 1,
                    Name = house.Name,
                    BaseCurrency = GameEnvironment.Currencies[1],
                    Coefficient = 0,
                    MinSum = house.CommunalPayments,
                    MaxSum = house.CommunalPayments,
                    Periodicity = 1, //enum Periodicity mounth id=1
                    PeriodicityValue = 1,
                    TermUnit = 1,
                    MinTerm = 1,
                    MaxTerm = 1,
                };

                PlayerTariff playerCommunalTariff = new PlayerTariff(playerTariff, communalService, house.CommunalPayments * Currency.Course, 1, GameEnvironment.Events.Timer.DateTime);
                GameEnvironment.Player.Tariffs.Add(playerCommunalTariff);

                DateTime date = GameEnvironment.Events.Timer.DateTime; date = new DateTime(date.Year, date.Month, 15, date.Hour, date.Minute, date.Second);
                Periodicity periodicity = (Periodicity)Enum.GetValues(typeof(Periodicity)).GetValue(playerCommunalTariff.Periodicity);
                GameEnvironment.Events.Events.Add(new GameEvent(communalService.ID + ":" + playerCommunalTariff.ID + ":" + (house.CommunalPayments * playerCommunalTariff.BaseCurrency.Course),
                                      PeriodicityConverter.GetDateTimeFromPeriodicity(date, playerCommunalTariff.Periodicity, playerCommunalTariff.PeriodicityValue),
                                      periodicity, playerCommunalTariff.PeriodicityValue, CommunalPayment, true));
                GameEnvironment.Player.House = new PlayerHouse(house, playerCommunalTariff, false, true, false);
                MessageBox.Show(Resources.GameMessage28, MessageBoxType.Information);
                GameEnvironment.Messages.Add(new Message(Resources.RealEstateAgencyFullName, string.Format(Resources.YouBoughtX, house.Name), Icon.Info));
            }
            else { MessageBox.Show(Resources.GameMessage29, MessageBoxType.Warning); }
        }

        Service communal_service = null;
        HousingSale housingSale;
        private void BuyCreditHouse(HousingSale house)
        {
            housingSale = house;

            if (GameEnvironment.Player.House != null)
            {
                if (GameEnvironment.Player.House.IsRent == 1 & GameEnvironment.Player.House.IsRentedOut & house.Id == GameEnvironment.Player.House.Id) { if (MessageBox.Show(Resources.CreditPurchase, Resources.GameMessage24, MessageBoxType.ConfirmationWithYesNo) == MessageBoxResult.No) { return; } }
                if ((GameEnvironment.Player.House.IsPurchased || GameEnvironment.Player.House.IsPurchasedOnCredit) && house.Id == GameEnvironment.Player.House.Id) { _ = MessageBox.Show(Resources.CreditPurchase, Resources.GameMessage25, MessageBoxType.Information); return; }
                if (GameEnvironment.Player.House.IsPurchasedOnCredit && house.Id != GameEnvironment.Player.House.Id) { _ = MessageBox.Show(Resources.CreditPurchase, Resources.GameMessage26, MessageBoxType.Information); return; }
                if (GameEnvironment.Player.House.IsPurchased && house.Id != GameEnvironment.Player.House.Id) { if (MessageBox.Show(Resources.CreditPurchase, Resources.GameMessage27, MessageBoxType.ConfirmationWithYesNo) == MessageBoxResult.No) { return; } }
            }

            Service service = null;
            for (int i = 0; i <= GameEnvironment.Services.Count - 1; i++)
            {
                if (GameEnvironment.Services[i].SystemName == "credit") { service = GameEnvironment.Services[i]; }
                else if (GameEnvironment.Services[i].SystemName == "communal_payments") { communal_service = GameEnvironment.Services[i]; }
                if (service != null && communal_service != null) { break; }
            }
            if (service == null) { MessageBox.Show(Resources.CreditPurchase, Resources.ErroreCodeCreditServiceNotFound, MessageBoxType.Error); return; }
            if (communal_service == null) { MessageBox.Show(Resources.CreditPurchase, Resources.ErroreCodeCommunalPaymentServiceNotFound, MessageBoxType.Error); return; }
            PropertyForSale();

            BankSelectedService = service;
            ServicesVisability = Visibility.Collapsed;
            TariffAmountEnabled = false;
            TariffAmount = house.ConvertedValue.ToString();
            RentPanelVisability = Visibility.Collapsed;
            AdditionsPanelServices = Visibility.Visible;
        }

        public void LoanIssued(PlayerTariff tariff)
        {
            if (Form is RealEstateAgency) {
                HideCreditPanel();
                housingSale.Currency.Withdraw(string.Format(Resources.BuyingAX, housingSale.Name), Resources.RealEstateAgencyFullName, GameEnvironment.Events.Timer.DateTime, housingSale.ConvertedValue);
                int id = GameEnvironment.Player.Tariffs.Count + 1;
                Tariff playerCTariff = new Tariff
                {
                    ID = id,
                    Name = housingSale.Name,
                    BaseCurrency = GameEnvironment.Currencies[1],
                    Coefficient = 0,
                    MinSum = housingSale.CommunalPayments,
                    MaxSum = housingSale.CommunalPayments,
                    Periodicity = 1, //enum Periodicity mounth id=1
                    PeriodicityValue = 1,
                    TermUnit = 1,
                    MinTerm = 1,
                    MaxTerm = 1,
                };
                PlayerTariff playerCommunalTariff = new PlayerTariff(playerCTariff, communal_service, housingSale.CommunalPayments * housingSale.Currency.Course, 1, GameEnvironment.Events.Timer.DateTime);
                GameEnvironment.Player.Tariffs.Add(playerCommunalTariff);

                GameEnvironment.Player.House = new PlayerHouse(housingSale, playerCommunalTariff, false, false, true, tariff);
                Periodicity periodicity = (Periodicity)Enum.GetValues(typeof(Periodicity)).GetValue(playerCommunalTariff.Periodicity);
                DateTime date = GameEnvironment.Events.Timer.DateTime; date = new DateTime(date.Year, date.Month, 15, date.Hour, date.Minute, date.Second);
                GameEnvironment.Events.Events.Add(new GameEvent(communal_service.ID + ":" + playerCommunalTariff.ID + ":" + (housingSale.CommunalPayments * housingSale.Currency.Course),
                      PeriodicityConverter.GetDateTimeFromPeriodicity(date, playerCommunalTariff.Periodicity, playerCommunalTariff.PeriodicityValue),
                      periodicity, playerCommunalTariff.PeriodicityValue, CommunalPayment, true));
                MessageBox.Show(Resources.GameMessage28, MessageBoxType.Information);
                GameEnvironment.Messages.Add(new Message(Resources.RealEstateAgencyFullName, string.Format(Resources.YouBoughtX, housingSale.Name), Icon.Info));
            }
        }

        public void CancellationLoan()
        {
            if (Form is RealEstateAgency)
            {
                HideCreditPanel();
            }
        }

        private void HideCreditPanel()
        {
            AdditionsPanelServices = Visibility.Collapsed;
            RentPanelVisability = Visibility.Visible;
            ServicesVisability = Visibility.Visible;
            TariffAmountEnabled = true;
            TariffAmount = "0";
        }

        private bool PropertyForSale()
        {
            if (GameEnvironment.Player.House != null)
            {
                GameEvent @event;
                bool isBreak = false;

                BaseCurrencies reaCurrencies = null;
                foreach (BaseCurrencies currency in GameEnvironment.Player.Money) { if (GameEnvironment.Player.House.PlayerRent.BaseCurrency.ID == currency.ID) reaCurrencies = currency; break; }

                for (int i = 0; i >= GameEnvironment.Events.Events.Count - 1; i++)
                {
                    if (GameEnvironment.Player.House.IsRentedOut)
                    {
                        if (GameEnvironment.Events.Events[i].Name == (GameEnvironment.Player.House.PlayerRent.Service.ID + ":" + GameEnvironment.Player.House.PlayerRent.ID + ":" + GameEnvironment.Player.House.Rent))
                        {
                            @event = GameEnvironment.Events.Events[i];
                            double sum = (@event.ResponseTime - GameEnvironment.Events.Timer.DateTime).TotalDays * GameEnvironment.Player.House.PlayerRent.Amount;
                            if (reaCurrencies != null && reaCurrencies.Withdraw(Resources.RentPayment, Resources.RealEstateAgencyFullName, GameEnvironment.Events.Timer.DateTime, sum)) { MessageBox.Show(Resources.RentN, Resources.GameMessage30, MessageBoxType.Information); return false; }
                            GameEnvironment.Events.Events.Remove(@event);
                            if (isBreak) { break; }
                            isBreak = true;
                        }
                    }
                    else if (GameEnvironment.Events.Events[i].Name == (GameEnvironment.Player.House.PlayerCommunalPayments.Service.ID + ":" + GameEnvironment.Player.House.PlayerCommunalPayments.ID + ":" + GameEnvironment.Player.House.CommunalPayments))
                    {
                        @event = GameEnvironment.Events.Events[i];
                        double sum = (@event.ResponseTime - GameEnvironment.Events.Timer.DateTime).TotalDays * GameEnvironment.Player.House.PlayerCommunalPayments.Amount;
                        if (reaCurrencies != null && reaCurrencies.Withdraw(Resources.PaymentUtilityBills, Resources.DepartmentHousingAndUtilities, GameEnvironment.Events.Timer.DateTime, sum)) { MessageBox.Show(Resources.RentN, Resources.GameMessage31, MessageBoxType.Information); return false; }
                        GameEnvironment.Events.Events.Remove(@event);
                        if (isBreak) { break; }
                        isBreak = true;
                    }
                }

                if (GameEnvironment.Player.House.IsPurchased)
                {
                    GameEnvironment.Player.Money[0].TopUp(Resources.PropertyForSale, GameEnvironment.Player.Name, GameEnvironment.Events.Timer.DateTime, (GameEnvironment.Player.House.Price * 90 / 100) * GameEnvironment.Player.Money[0].Course);
                }
            }
            return true;
        }

        private void RentalPayment(GameEvent @event)
        {
            string[] keys = @event.Name.Split(new char[] { ':' });

            List<PlayerTariff> playerTariffs = new List<PlayerTariff>(GameEnvironment.Player.Tariffs);
            PlayerTariff tariff = playerTariffs.Find(t => t.Service.ID.ToString() == keys[0] & t.ID.ToString() == keys[1] & t.Amount.ToString() == keys[2]);
            if (tariff != null)
            {
                DateTime dateTime;
                if (tariff.StartDateOfService.Year == GameEnvironment.Events.Timer.DateTime.Year && tariff.StartDateOfService.Month + 1 == GameEnvironment.Events.Timer.DateTime.Month)
                {
                    dateTime = tariff.StartDateOfService;
                }
                else { dateTime = @event.ResponseTime.AddMonths(-1); }
                double sum = (GameEnvironment.Events.Timer.DateTime - dateTime).TotalDays * tariff.Amount;
                BaseCurrencies reaCurrencies = null;
                foreach (BaseCurrencies currency in GameEnvironment.Player.Money) { if (tariff.BaseCurrency.ID == currency.ID) reaCurrencies = currency; break; }
                if (reaCurrencies != null && reaCurrencies.Withdraw(Resources.RentPayment, Resources.RealEstateAgencyFullName, GameEnvironment.Events.Timer.DateTime, sum) == false)
                {
                    GameEnvironment.Player.House = null;
                    GameEnvironment.Events.Events.Remove(@event);
                    GameEnvironment.Messages.Add(new Message(Resources.RealEstateAgencyFullName, Resources.GameMessage32, Icon.Info));
                }
            } else { MessageBox.Show(Resources.PaymentAndCollectionProcessing, Resources.GameMessage14, MessageBoxType.Error); }
        }

        private void CommunalPayment(GameEvent @event)
        {
            string[] keys = @event.Name.Split(new char[] { ':' });
            List<PlayerTariff> playerTariffs = new List<PlayerTariff>(GameEnvironment.Player.Tariffs);
            PlayerTariff tariff = playerTariffs.Find(t => t.Service.ID.ToString() == keys[0] & t.ID.ToString() == keys[1] & t.Amount.ToString() == keys[2]);
            if (tariff != null)
            {
                DateTime dateTime;
                if (tariff.StartDateOfService.Year == GameEnvironment.Events.Timer.DateTime.Year && tariff.StartDateOfService.Month + 1 == GameEnvironment.Events.Timer.DateTime.Month)
                {
                    dateTime = tariff.StartDateOfService;
                }
                else { dateTime = @event.ResponseTime.AddMonths(-1); }
                double sum = (GameEnvironment.Events.Timer.DateTime - dateTime).TotalDays * tariff.Amount;
                BaseCurrencies reaCurrencies = null;
                foreach(BaseCurrencies currency in GameEnvironment.Player.Money) { if (tariff.BaseCurrency.ID == currency.ID) reaCurrencies = currency; break; }
                if (reaCurrencies != null && reaCurrencies.Withdraw(Resources.PaymentUtilityBills, Resources.DepartmentHousingAndUtilities, GameEnvironment.Events.Timer.DateTime, sum) == false)
                {  GameEnvironment.Scenario.GameOver(Resources.GameMessage33); }
            }
            else { MessageBox.Show(Resources.PaymentAndCollectionProcessing, Resources.GameMessage14, MessageBoxType.Error); }
        }

        private Visibility _rentPanelVisability = Visibility.Visible;

        public Visibility RentPanelVisability
        {
            get => _rentPanelVisability;
            set => SetProperty(ref _rentPanelVisability, value);
        }

        public Collection<HousingSale> Houses { get; } = new Collection<HousingSale>();
        public DelegateCommand<HousingSale> REABuy { get; private set; }
        public DelegateCommand<HousingSale> REARent { get; private set; }
        public DelegateCommand<HousingSale> REABuyCredit { get; private set; }
    }
}
