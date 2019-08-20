using Computer_Era_X.Converters;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models;
using Computer_Era_X.Models.Systems;
using Computer_Era_X.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Computer_Era_X.ViewModels
{
    partial class GameVM
    {
        partial void RealEstateAgencyInit()
        {
            REABuy = new DelegateCommand<HousingSale>(BuyHouse);
            REARent = new DelegateCommand<HousingSale>(RentHouse);
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
                if (GameEnvironment.Player.House.IsRent == 1 & GameEnvironment.Player.House.IsRentedOut & house.Id == GameEnvironment.Player.House.Id) { _ = MessageBox.Show("Аренда", "Вы уже арендовали данное жилье", MessageBoxType.Warning); return; }
                if (GameEnvironment.Player.House.IsPurchased) { if (MessageBox.Show("Аренда", "У вас куплено жилье, вы действительно хотите его продать?", MessageBoxType.ConfirmationWithYesNo) == System.Windows.MessageBoxResult.No) { return; } }
                if (GameEnvironment.Player.House.IsPurchasedOnCredit) { _ = MessageBox.Show("Аренда", "Нельзя арендовать если есть купленное в кредит жилье!", MessageBoxType.Information); return; }
            }

            Service rentService = null;
            Service communalService = null;
            for (int i = 0; i <= GameEnvironment.Services.Count - 1; i++)
            {
                if (GameEnvironment.Services[i].SystemName == "rent") { rentService = GameEnvironment.Services[i]; }
                else if (GameEnvironment.Services[i].SystemName == "communal_payments") { communalService = GameEnvironment.Services[i]; }
                if (rentService != null && communalService != null) { break; }
            }
            if (rentService == null) { MessageBox.Show("Аренда", "Не найдена услуга аренды, убедитесь в целостности базы данных!", MessageBoxType.Error); return; }
            if (communalService == null) { MessageBox.Show("Аренда", "Не найдена услуга коммунальных платежей, убедитесь в целостности базы данных!", MessageBoxType.Error); return; }
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
            GameEnvironment.Messages.Add(new Message("Агенство недвижимости", "Вы арендовали: " + house.Name + ". Поздравляем Вас!", Icon.Info));
        }

        private void BuyHouse(HousingSale house)
        {
            if (GameEnvironment.Player.House != null)
            {
                if (GameEnvironment.Player.House.IsRent == 1 & GameEnvironment.Player.House.IsRentedOut & house.Id == GameEnvironment.Player.House.Id) { if (MessageBox.Show("Покупка", "Данное жилье арендовано вами, вы действительно хотите расторгнуть аренду?", MessageBoxType.ConfirmationWithYesNo) == System.Windows.MessageBoxResult.No) { return; } }
                if ((GameEnvironment.Player.House.IsPurchased || GameEnvironment.Player.House.IsPurchasedOnCredit) && house.Id == GameEnvironment.Player.House.Id) { _ = MessageBox.Show("Покупка", "У вас уже куплено данное жилье!", MessageBoxType.Information); return; }
                if (GameEnvironment.Player.House.IsPurchasedOnCredit && house.Id != GameEnvironment.Player.House.Id) { _ = MessageBox.Show("Покупка", "Нельзя купить новое жилье пока не погасите кредит за текущее!", MessageBoxType.Information); return; }
                if (GameEnvironment.Player.House.IsPurchased && house.Id != GameEnvironment.Player.House.Id) { if (MessageBox.Show("Покупка", "У вас есть другое купленно жилье, вы действительно хотите его продать?", MessageBoxType.ConfirmationWithYesNo) == System.Windows.MessageBoxResult.No) { return; } }
            }

            Service communalService = null;
            for (int i = 0; i <= GameEnvironment.Services.Count - 1; i++)
            {
                if (GameEnvironment.Services[i].SystemName == "communal_payments") { communalService = GameEnvironment.Services[i]; break; }
            }
            if (communalService == null) { MessageBox.Show("Покупка", "Не найдена услуга коммунальных платежей, убедитесь в целостности базы данных!", MessageBoxType.Error); return; }
            PropertyForSale();

            double price = house.Price * GameEnvironment.Player.Money[0].Course;
            if (GameEnvironment.Player.Money[0].Withdraw("Покупка " + house.Name, "Агенство недвижимости \"Крыша над головой\"", GameEnvironment.Events.Timer.DateTime, price))
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
                MessageBox.Show("Благодарим за покупку, с Вами приятно иметь дело!");
                GameEnvironment.Messages.Add(new Message("Агенство недвижимости", "Вы купили: " + house.Name + ". Поздравляем Вас с покупкой!", Icon.Info));
            }
            else { MessageBox.Show("К сожалению на вашем счету недостаточно средств!"); }
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
                            if (reaCurrencies != null && reaCurrencies.Withdraw("Выплата аренды", "Агенство недвижимости \"Крыша над головой\"", GameEnvironment.Events.Timer.DateTime, sum)) { MessageBox.Show("Аренда", "Вам не хватает средств на выплату задолженности по текущей аренде!", MessageBoxType.Information); return false; }
                            GameEnvironment.Events.Events.Remove(@event);
                            if (isBreak) { break; }
                            isBreak = true;
                        }
                    }
                    else if (GameEnvironment.Events.Events[i].Name == (GameEnvironment.Player.House.PlayerCommunalPayments.Service.ID + ":" + GameEnvironment.Player.House.PlayerCommunalPayments.ID + ":" + GameEnvironment.Player.House.CommunalPayments))
                    {
                        @event = GameEnvironment.Events.Events[i];
                        double sum = (@event.ResponseTime - GameEnvironment.Events.Timer.DateTime).TotalDays * GameEnvironment.Player.House.PlayerCommunalPayments.Amount;
                        if (reaCurrencies != null && reaCurrencies.Withdraw("Оплата коммунальных платежей", "Жилищно-комуунальное хозяйство", GameEnvironment.Events.Timer.DateTime, sum)) { MessageBox.Show("Аренда", "Вам не хватает средств на выплату задолженности по коммунальным платежам!", MessageBoxType.Information); return false; }
                        GameEnvironment.Events.Events.Remove(@event);
                        if (isBreak) { break; }
                        isBreak = true;
                    }
                }

                if (GameEnvironment.Player.House.IsPurchased)
                {
                    GameEnvironment.Player.Money[0].TopUp("Продажа недвижимости", GameEnvironment.Player.Name, GameEnvironment.Events.Timer.DateTime, (GameEnvironment.Player.House.Price * 90 / 100) * GameEnvironment.Player.Money[0].Course);
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
                if (reaCurrencies != null && reaCurrencies.Withdraw("Выплата аренды", "Агенство недвижимости \"Крыша над головой\"", GameEnvironment.Events.Timer.DateTime, sum) == false)
                {
                    GameEnvironment.Player.House = null;
                    GameEnvironment.Events.Events.Remove(@event);
                    GameEnvironment.Messages.Add(new Message("Агенство недвижимости \"Крыша над головой\"", "Вы были выселены за неуплату аренды!", Icon.Info));
                }
            } else { MessageBox.Show("Обработка выплат и взысканий", "Что-то пошло не так, тариф не найден!", MessageBoxType.Error); }
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
                if (reaCurrencies != null && reaCurrencies.Withdraw("Оплата коммунальных платежей", "Жилищно-коммунальное хозяйство", GameEnvironment.Events.Timer.DateTime, sum) == false)
                {  GameEnvironment.Scenario.GameOver("Вы не оплатили коммунальные платежи"); }
            }
            else { MessageBox.Show("Обработка выплат и взысканий", "Что-то пошло не так, тариф не найден!", MessageBoxType.Error); }
        }

        public Collection<HousingSale> Houses { get; } = new Collection<HousingSale>();
        public DelegateCommand<HousingSale> REABuy { get; private set; }
        public DelegateCommand<HousingSale> REARent { get; private set; }
    }
}
