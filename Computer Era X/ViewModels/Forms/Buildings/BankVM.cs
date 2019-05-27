using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Computer_Era_X.Converters;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models;
using Computer_Era_X.Properties;
using Prism.Commands;

namespace Computer_Era_X.ViewModels
{
    public partial class GameVM
    {
        partial void BankInit()
        {
            AddService = new DelegateCommand(AdditionsPanelServices_Show);
            AcceptTerms = new DelegateCommand(BankAcceptTerms);
        }

        private void AdditionsPanelServices_Show()
        {
            InformationPanelForTheService = Visibility.Collapsed;
            AdditionsPanelServices = Visibility.Visible;
            if (Services.Count >= 1) { BankSelectedService = Services[0]; }
        }

        private void ServiceSelection()
        {
            if (BankSelectedService == null) { return; }
            BankTariffs = BankSelectedService.Tariffs;
            if (BankTariffs.Count >= 1) { BankSelectedTariff = BankTariffs[0]; }
        }

        private void TariffSelection()
        {
            if (BankSelectedTariff == null) { return; }
            TariffDescription = BankSelectedTariff.Info();
            TariffDescriptionVisibility = Visibility.Visible;
            Periodicity period = (Periodicity)Enum.GetValues(typeof(Periodicity)).GetValue(BankSelectedTariff.TermUnit);
            LabelTariffPeriod = PeriodicityConverter.ToLocalizedString(period);
            TariffPeriods = BankSelectedTariff.MinTerm.ToString();
        }

        private void BankAcceptTerms()
        {
            if (BankSelectedService == null) { return; }
            if (BankSelectedTariff == null) { return; }

            if (double.TryParse(TariffAmount, out double sum))
            {
                if (BankSelectedTariff.MinSum != 0 & sum < BankSelectedTariff.MinSum || BankSelectedTariff.MaxSum != 0 & sum > BankSelectedTariff.MaxSum) { MessageBox.Show(Resources.GameMessage5); return; }
                double sum_tarrifs = 0;
                foreach (PlayerTariff p_tariff in GameEnvironment.Player.Tariffs)
                    if (p_tariff.Service.ID == BankSelectedService.ID) { sum_tarrifs += p_tariff.Amount; }

                BaseCurrencies currency  = new BaseCurrencies(BankSelectedTariff.BaseCurrency);
                foreach (var curr in GameEnvironment.Player.Money)
                    if (curr.ID == BankSelectedTariff.BaseCurrency.ID) { currency = curr; break; }
                if (BankSelectedService.TransactionType == 0) //TopUp
                {
                    if (BankSelectedService.TotalMaxContribution != 0 & BankSelectedService.TotalMaxContribution < ((sum_tarrifs + sum) / currency.Course))
                    { MessageBox.Show(string.Format(Resources.GameMessage6, sum_tarrifs + sum - (BankSelectedService.TotalMaxContribution * currency.Course), currency.Abbreviation)); return; }
                    if (currency.Withdraw(BankSelectedService.Name, Resources.BankName, GameEnvironment.Events.Timer.DateTime, sum) == false)
                    { MessageBox.Show(Resources.GameMessage7); return; }
                }
                if (BankSelectedService.TransactionType == 1) //Withdraw
                {
                    if (BankSelectedService.TotalMaxDebt != 0 & BankSelectedService.TotalMaxDebt < ((sum_tarrifs + sum) / currency.Course))
                    { MessageBox.Show(string.Format(Resources.GameMessage6, ((sum_tarrifs + sum) - (BankSelectedService.TotalMaxDebt * currency.Course)), currency.Abbreviation)); return; }
                    if (currency.TopUp(BankSelectedService.Name, Resources.BankName,
                                              GameEnvironment.Events.Timer.DateTime, sum) == false) { MessageBox.Show(Resources.GameMessage8); return; }
                }
                GameEnvironment.Player.Tariffs.Add(new PlayerTariff(BankSelectedTariff, BankSelectedService, sum,
                                                            Convert.ToInt32(TariffPeriods),
                                                            GameEnvironment.Events.Timer.DateTime, BankSelectedTariff.SpecialOffer));

                GameEnvironment.Events.Events.Add(new GameEvent(BankSelectedService.ID + ":" + BankSelectedTariff.ID + ":" + sum,
                                                    PeriodicityConverter.GetDateTimeFromPeriodicity(GameEnvironment.Events.Timer.DateTime, BankSelectedTariff.Periodicity, BankSelectedTariff.PeriodicityValue),
                                                    (Periodicity)Enum.GetValues(typeof(Periodicity)).GetValue(BankSelectedTariff.Periodicity), BankSelectedTariff.PeriodicityValue, ProcessingServices, true));
                MessageBox.Show(string.Format(Resources.GameMessage9, BankSelectedService.Name.ToLower(), BankSelectedTariff.Name));
                AdditionsPanelServices = Visibility.Collapsed;
                InformationPanelForTheService = Visibility.Visible;
            } else { MessageBox.Show(string.Format(Resources.GameMessage10, BankSelectedService.Name.ToLower())); }
        }

        public void ProcessingServices(GameEvent @event)
        {
            string[] keys = @event.Name.Split(new char[] { ':' });
            List<PlayerTariff> tariffs = GameEnvironment.Player.Tariffs.Where(t => t.Service.ID.ToString() == keys[0] & t.ID.ToString() == keys[1] & t.Amount.ToString() == keys[2]).ToList();
            if (tariffs.Count == 1)
            {
                PlayerTariff tariff = tariffs[0];
                BaseCurrencies currency = new BaseCurrencies(BankSelectedTariff.BaseCurrency);
                foreach (var curr in GameEnvironment.Player.Money)
                    if (curr.ID == BankSelectedTariff.BaseCurrency.ID) { currency = curr; break; }
                if (tariff.Service.TransactionType == 0) //TopUp
                {
                    currency.TopUp("Выплата по услуге \"" + tariff.Service.Name + "\" (" + tariff.Name + ")", Resources.BankName, GameEnvironment.Events.Timer.DateTime, (tariff.Amount * tariff.Coefficient / 100));
                    if (DateTime.Compare(PeriodicityConverter.GetDateByPeriod(tariff.StartDateOfService, tariff.TermUnit, tariff.Term), @event.ResponseTime) <= 0)
                    {
                        GameEnvironment.Events.Events.Remove(@event);
                        GameEnvironment.Player.Tariffs.Remove(tariff);
                        currency.TopUp("Возврат средств в связи с истечением периода оказания услуги \"" + tariff.Service.Name + "\" (" + tariff.Name + ")", Properties.Resources.BankName, GameEnvironment.Events.Timer.DateTime, tariff.Amount);
                    }
                }
                else if (tariff.Service.TransactionType == 1) //Withdraw
                {
                    int per_s = PeriodicityConverter.GetNumberOfPeriods(tariff.Periodicity, tariff.PeriodicityValue, tariff.StartDateOfService, PeriodicityConverter.GetDateByPeriod(tariff.StartDateOfService, tariff.TermUnit, tariff.Term));
                    if (!currency.Withdraw("Взыскание по услуге\"" + tariff.Service.Name + "\" (" + tariff.Name + ")", Properties.Resources.BankName, GameEnvironment.Events.Timer.DateTime, (tariff.Amount + (tariff.Amount * tariff.Coefficient / 100) * tariff.Term) / per_s))
                    {
                        if (tariff.PropertyPledged > 0)
                        {
                            //if (tariff.PropertyPledged is House)
                            //{
                                //GameEnvironment.Services.PlayerTariffs.Remove(GameEnvironment.Player.House.PlayerCommunalPayments);
                                //GameEnvironment.Player.House = null;
                                //GameEnvironment.Services.PlayerTariffs.Remove(tariff);
                                //GameEnvironment.GameEvents.Events.Remove(@event);
                                //GameEnvironment.Messages.NewMessage("Банк", "У вас изъяли недвижимость в связи с отсутствием средств для выплаты задолженности!", GameMessages.Icon.Info);
                            //}
                        }
                        else { GameEnvironment.Scenario.GameOver("Нет денег для выплаты по услуге \"" + tariff.Service.Name + "\" тарифный план \"" + tariff.Name + "\""); }
                    } //ВЫЗОВ СОБЫТИЯ GAME_OVER если не хватает денег (Игрок банкрот), исключение если есть залог
                    if (DateTime.Compare(PeriodicityConverter.GetDateByPeriod(tariff.StartDateOfService, tariff.TermUnit, tariff.Term), @event.ResponseTime) <= 0)
                    {
                        if (tariff.PropertyPledged > 0)
                        {
                            //if (tariff.PropertyPledged is House)
                            //{
                               // GameEnvironment.Player.House.IsPurchasedOnCredit = false;
                               // GameEnvironment.Player.House.IsPurchased = true;
                            //}
                        }
                        GameEnvironment.Events.Events.Remove(@event);
                        GameEnvironment.Player.Tariffs.Remove(tariff);
                    }
                }
            } else {
                MessageBox.Show("Обработка выплат и взысканий", "Что-то пошло не так, тариф не найден!");
            }
        }



        private void BankRejectСonditions() => AdditionsPanelServices = Visibility.Collapsed;

        private Visibility _additionsPanelServices = Visibility.Collapsed;
        private Visibility _informationPanelForTheService = Visibility.Visible;
        private Service _selectedService;
        public ObservableCollection<Tariff> _bankTariffs;
        private Tariff _selectedTariff;
        private Visibility _tariffDescriptionVisibility = Visibility.Collapsed;
        private string _tariffDescription;
        private string _labelTariffPeriod = Resources.Periods;
        private string _tariffPeriods;
        private string _tariffAmount;
        private string _totalService;

        public Visibility AdditionsPanelServices
        {
            get => _additionsPanelServices;
            set => SetProperty(ref _additionsPanelServices, value);
        }
        public Visibility InformationPanelForTheService
        {
            get => _informationPanelForTheService;
            set => SetProperty(ref _informationPanelForTheService, value);
        }
        public Service BankSelectedService
        {
            get => _selectedService;
            set
            {
                SetProperty(ref _selectedService, value);
                ServiceSelection();
            }
        }

        public ObservableCollection<Tariff> BankTariffs
        {
            get => _bankTariffs;
            set => SetProperty(ref _bankTariffs, value);
        }

        public Tariff BankSelectedTariff
        {
            get => _selectedTariff;
            set
            {
                SetProperty(ref _selectedTariff, value);
                TariffSelection();
            }
        }

        public Visibility TariffDescriptionVisibility
        {
            get => _tariffDescriptionVisibility;
            set => SetProperty(ref _tariffDescriptionVisibility, value);
        }

        public string TariffDescription
        {
            get => _tariffDescription;
            set => SetProperty(ref _tariffDescription, value);
        }

        public string LabelTariffPeriod
        {
            get => _labelTariffPeriod;
            set => SetProperty(ref _labelTariffPeriod, value);
        }

        public string TariffPeriods
        {
            get => _tariffPeriods;
            set
            {
                if (BankSelectedTariff == null) { return; }
                if (int.TryParse(value, out int _num))
                {
                    if (_num > BankSelectedTariff.MaxTerm) { SetProperty(ref _tariffPeriods, BankSelectedTariff.MaxTerm.ToString()); }
                    else if (_num < BankSelectedTariff.MinTerm) { SetProperty(ref _tariffPeriods, BankSelectedTariff.MinTerm.ToString()); }
                    else { SetProperty(ref _tariffPeriods, value); }   
                } else { SetProperty(ref _tariffPeriods, BankSelectedTariff.MinTerm.ToString()); }
                TariffAmount = TariffAmount;
            }
        }

        public string TariffAmount
        {
            get => _tariffAmount;
            set
            {
                if (BankSelectedTariff == null) { return; }
                if (int.TryParse(value, out int _num))
                {
                    if (BankSelectedTariff.MaxSum != 0 && _num > BankSelectedTariff.MaxSum) { SetProperty(ref _tariffAmount, BankSelectedTariff.MaxSum.ToString()); }
                    else if (_num < BankSelectedTariff.MinSum) { SetProperty(ref _tariffAmount, BankSelectedTariff.MinSum.ToString()); }
                    else { SetProperty(ref _tariffAmount, value); }

                    if (int.TryParse(TariffPeriods, out int _tariff))
                    {
                        if (BankSelectedService.TransactionType == 0) //Deposit
                        {
                            TotalService = string.Format(Resources.TotalСhargesWillBe, ((double)_num * BankSelectedTariff.Coefficient / 100 * _tariff).ToString("N3"), BankSelectedTariff.BaseCurrency.Abbreviation);
                        } else if (BankSelectedService.TransactionType == 1) { //Credit
                            TotalService = string.Format(Resources.TheTotalAmountOfPaymentsWillBe, ((double)_num + (_num * BankSelectedTariff.Coefficient / 100) * _tariff).ToString("N3"), BankSelectedTariff.BaseCurrency.Abbreviation);
                        }
                    }
                } else { SetProperty(ref _tariffAmount, BankSelectedTariff.MinTerm.ToString()); }
            }
        }

        public string TotalService
        {
            get => _totalService;
            set => SetProperty(ref _totalService, value);
        }

        public DelegateCommand AddService { get; private set; }
        public DelegateCommand AcceptTerms { get; private set; }
        public DelegateCommand RejectСonditions { get; set; }
    }
}
