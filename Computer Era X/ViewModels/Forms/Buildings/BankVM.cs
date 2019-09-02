using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Computer_Era_X.Views;
using Computer_Era_X.Converters;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models;
using Computer_Era_X.Models.Systems;
using Computer_Era_X.Properties;
using Computer_Era_X.Validators;
using Prism.Commands;
using MessageBox = Computer_Era_X.Views.MessageBox;

namespace Computer_Era_X.ViewModels
{
    public partial class GameVM
    {
        partial void BankInit()
        {
            AddService = new DelegateCommand(AdditionsPanelServices_Show);
            AcceptTerms = new DelegateCommand(BankAcceptTerms);
            RejectСonditions = new DelegateCommand(BankRejectСonditions);
            Exchange = new DelegateCommand(CurrencyExchange);

            Money.ItemPropertyChanged += AddExchangeRate;
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
                PlayerTariff playerTariff = new PlayerTariff(BankSelectedTariff, BankSelectedService, sum,
                                                            Convert.ToInt32(TariffPeriods),
                                                            GameEnvironment.Events.Timer.DateTime, BankSelectedTariff.SpecialOffer);
                if (BankSelectedService.TransactionType == 1) LoanIssued(playerTariff); //We transfer the user rate to another window (used for a real estate agency)
                GameEnvironment.Player.Tariffs.Add(playerTariff);

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
                    currency.TopUp(Resources.PaymentOnPayment + " \"" + tariff.Service.Name + "\" (" + tariff.Name + ")", Resources.BankName, GameEnvironment.Events.Timer.DateTime, (tariff.Amount * tariff.Coefficient / 100));
                    if (DateTime.Compare(PeriodicityConverter.GetDateByPeriod(tariff.StartDateOfService, tariff.TermUnit, tariff.Term), @event.ResponseTime) <= 0)
                    {
                        GameEnvironment.Events.Events.Remove(@event);
                        GameEnvironment.Player.Tariffs.Remove(tariff);
                        currency.TopUp(Resources.GameMessage11 + " \"" + tariff.Service.Name + "\" (" + tariff.Name + ")", Resources.BankName, GameEnvironment.Events.Timer.DateTime, tariff.Amount);
                    }
                }
                else if (tariff.Service.TransactionType == 1) //Withdraw
                {
                    int per_s = PeriodicityConverter.GetNumberOfPeriods(tariff.Periodicity, tariff.PeriodicityValue, tariff.StartDateOfService, PeriodicityConverter.GetDateByPeriod(tariff.StartDateOfService, tariff.TermUnit, tariff.Term));
                    if (!currency.Withdraw(Resources.RecoveryByService + " \"" + tariff.Service.Name + "\" (" + tariff.Name + ")", Resources.BankName, GameEnvironment.Events.Timer.DateTime, (tariff.Amount + (tariff.Amount * tariff.Coefficient / 100) * tariff.Term) / per_s))
                    {
                        if (tariff.PropertyPledged != null)
                        {
                            if (tariff.PropertyPledged is House)
                            {
                                GameEnvironment.Player.Tariffs.Remove(GameEnvironment.Player.House.PlayerCommunalPayments);
                                GameEnvironment.Player.House = null;
                                GameEnvironment.Player.Tariffs.Remove(tariff);
                                GameEnvironment.Events.Events.Remove(@event);
                                GameEnvironment.Messages.Add(new Message(Resources.Bank, Resources.GameMessage12, Icon.Info));
                            }
                        }
                        else { GameEnvironment.Scenario.GameOver(string.Format(Resources.GameMessage13, tariff.Service.Name, tariff.Name)); }
                    } //Call events GAME_OVER if there is not enough money (player is bankrupt), an exception if there is a pledge
                    if (DateTime.Compare(PeriodicityConverter.GetDateByPeriod(tariff.StartDateOfService, tariff.TermUnit, tariff.Term), @event.ResponseTime) <= 0)
                    {
                        if (tariff.PropertyPledged != null)
                        {
                            if (tariff.PropertyPledged is House)
                            {
                               GameEnvironment.Player.House.IsPurchasedOnCredit = false;
                               GameEnvironment.Player.House.IsPurchased = true;
                            }
                        }
                        GameEnvironment.Events.Events.Remove(@event);
                        GameEnvironment.Player.Tariffs.Remove(tariff);
                    }
                }
            } else {
                MessageBox.Show(Resources.ProcessingPaymentsAndPenalties, Resources.GameMessage14);
            }
        }

        private void BankRejectСonditions()
        {
            AdditionsPanelServices = Visibility.Collapsed;
            CancellationLoan();
        }
        private void PlayerTariffSelection()
        {
            if (SelectedPlayerTarif == null || AdditionsPanelServices == Visibility.Visible) { return; }
            InformationPanelForTheService = Visibility.Visible;
            TariffInformation = SelectedPlayerTarif.Info();
        }
        private void AddExchangeRate(object sender, PropertyChangedEventArgs e)
        {
            ExchangeRates.Clear();
            if (GameEnvironment.Player.Money.Count < 2) { return; }
            for (int i= 1; i < GameEnvironment.Player.Money.Count; i++)
            {
                BaseCurrencies _firstCurrency = GameEnvironment.Player.Money[0];
                BaseCurrencies _secondCurrency = GameEnvironment.Player.Money[i];
                double _firstCurrencieUnit = 1 / _firstCurrency.Course; //Counts how much BGC we get per unit of base currency
                double _secondCurrencieUnit = 1 / _secondCurrency.Course; //Counts how much BGC we get per unit of second currency
                double _course = (_secondCurrencieUnit / _firstCurrencieUnit) - (_secondCurrencieUnit / _firstCurrencieUnit * 1 / 100);
                ExchangeRates.Add(new ExchangeRates(_secondCurrency, _firstCurrency, 1, _course));

                _course = (_secondCurrencieUnit / _firstCurrencieUnit) + (_secondCurrencieUnit / _firstCurrencieUnit * 1 / 100);
                ExchangeRates.Add(new ExchangeRates(_firstCurrency, _secondCurrency, _course, 1));
            }
        }

        private void CurrencyExchange()
        {
            if (LastCurrencyHighlighted == null) { return; }
            ExchangeRates rate = LastCurrencyHighlighted;
            double ammount = NumberValidator.GetDoubleFromString(AmountOfExchangeableCurrency);
            if (rate.FirstCurrency.Withdraw(Resources.BankName, Resources.CurrencyExchange, GameEnvironment.Events.Timer.DateTime, ammount))
            {
                double course;
                if (rate.FirstСurrencyCourse == 1)
                {
                    course = rate.SecondСurrencyCourse;
                    rate.SecondCurrency.TopUp(Resources.BankName, Resources.CurrencyExchange, GameEnvironment.Events.Timer.DateTime, ammount * course);
                } else {
                    course = rate.FirstСurrencyCourse;
                    rate.SecondCurrency.TopUp(Resources.BankName, Resources.CurrencyExchange, GameEnvironment.Events.Timer.DateTime, ammount / course);
                } //Definition of the dividend
            } else { MessageBox.Show(Resources.CurrencyExchange, Resources.YouDoNotHaveEnoughMoney, MessageBoxType.Warning); }
        }

        private void SelectionExchangeRates()
        {
            if (SelectedExchangeRates != null) LastCurrencyHighlighted = SelectedExchangeRates;
            if (LastCurrencyHighlighted != null) ExchangeAvailability = true;
        }

        private Visibility _additionsPanelServices = Visibility.Collapsed;
        private Visibility _informationPanelForTheService = Visibility.Visible;
        private Service _selectedService;
        private Visibility _servicesVisability = Visibility.Visible;
        public ObservableCollection<Tariff> _bankTariffs;
        private Tariff _selectedTariff;
        private Visibility _tariffDescriptionVisibility = Visibility.Collapsed;
        private string _tariffDescription;
        private string _labelTariffPeriod = Resources.Periods;
        private string _tariffPeriods;
        private string _tariffAmount;
        private bool _tariffAmountEnabled = true;
        private string _totalService;
        public ObservableCollection<PlayerTariff> PlayerTariffs => GameEnvironment.Player.Tariffs;
        private PlayerTariff _selectedPlayerTarif;
        private string _tariffInformation;
        private ObservableCollection<ExchangeRates> _exchangeRates = new ObservableCollection<ExchangeRates>();
        private string _amountOfExchangeableCurrency;
        private bool _exchangeAvailability = false;
        private ExchangeRates _selectedExchangeRates;
        private ExchangeRates LastCurrencyHighlighted { get; set; }

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

        public Visibility ServicesVisability
        {
            get => _servicesVisability;
            set => SetProperty(ref _servicesVisability, value);
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
                if (double.TryParse(value, out double _num))
                {
                    SetProperty(ref _tariffAmount, value);

                    if (int.TryParse(TariffPeriods, out int _tariff))
                    {
                        if (BankSelectedService.TransactionType == 0) //Deposit
                        {
                            TotalService = string.Format(Resources.TotalСhargesWillBe, (_num * BankSelectedTariff.Coefficient / 100 * _tariff).ToString("N3"), BankSelectedTariff.BaseCurrency.Abbreviation);
                        } else if (BankSelectedService.TransactionType == 1) { //Credit
                            TotalService = string.Format(Resources.TheTotalAmountOfPaymentsWillBe, (_num + (_num * BankSelectedTariff.Coefficient / 100) * _tariff).ToString("N3"), BankSelectedTariff.BaseCurrency.Abbreviation);
                        }
                    }
                } else { SetProperty(ref _tariffAmount, BankSelectedTariff.MinTerm.ToString()); }
            }
        }

        public bool TariffAmountEnabled
        {
            get => _tariffAmountEnabled;
            set => SetProperty(ref _tariffAmountEnabled, value);
        }

        public string TotalService
        {
            get => _totalService;
            set => SetProperty(ref _totalService, value);
        }

        public PlayerTariff SelectedPlayerTarif
        {
            get => _selectedPlayerTarif;
            set
            {
                SetProperty(ref _selectedPlayerTarif, value);
                PlayerTariffSelection();
            }
        }

        public string TariffInformation {
            get => _tariffInformation;
            set => SetProperty(ref _tariffInformation, value);
        }
        public ObservableCollection<ExchangeRates> ExchangeRates
        {
            get => _exchangeRates;
            set => SetProperty(ref _exchangeRates, value);
        }
        public string AmountOfExchangeableCurrency
        {
            get => _amountOfExchangeableCurrency;
            set
            {
                SetProperty(ref _amountOfExchangeableCurrency, NumberValidator.DoubleFromText(value));
            }
        }
        public bool ExchangeAvailability
        {
            get => _exchangeAvailability;
            set => SetProperty(ref _exchangeAvailability, value);
        }
        public ExchangeRates SelectedExchangeRates
        {
            get => _selectedExchangeRates;
            set
            {
                SetProperty(ref _selectedExchangeRates, value);
                SelectionExchangeRates();
            }
        }


        public DelegateCommand AddService { get; private set; }
        public DelegateCommand AcceptTerms { get; private set; }
        public DelegateCommand RejectСonditions { get; set; }
        public DelegateCommand Exchange { get; set; }
    }

    public class ExchangeRates
    {
        public BaseCurrencies FirstCurrency { get; set; }
        public BaseCurrencies SecondCurrency { get; set; }
        public double FirstСurrencyCourse { get; set; }
        public double SecondСurrencyCourse { get; set; }
        public string FirstСurrencyCourseToString { get; set; }
        public string SecondСurrencyCourseToString { get; set; }

        public ExchangeRates(BaseCurrencies firstCurrency, BaseCurrencies secondCurrency, double firstСurrencyCourse, double secondСurrencyCourse)
        {
            FirstCurrency = firstCurrency;
            SecondCurrency = secondCurrency;
            FirstСurrencyCourse = firstСurrencyCourse;
            SecondСurrencyCourse = secondСurrencyCourse;
            FirstСurrencyCourseToString = firstСurrencyCourse.ToString("N3") + " " + firstCurrency.Abbreviation;
            SecondСurrencyCourseToString = secondСurrencyCourse.ToString("N3") + " " + secondCurrency.Abbreviation;
        }
    }
}
