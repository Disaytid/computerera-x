using Computer_Era_X.DataTypes.Enums;
using System;
using System.Collections.ObjectModel;

namespace Computer_Era_X.DataTypes.Objects
{
    public class Tariff
    {
        public int UId { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public int Coefficient { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public Periodicity Periodicity { get; set; }
        public int PeriodicityValue { get; set; }
        public Periodicity TermUnit { get; set; }
        public int MinTerm { get; set; }
        public int MaxTerm { get; set; }
        public bool SpecialOffer { get; set; }
        public object PropertyPledged { get; set; } //Property on bail

        public Tariff(int uid, string name, Currency currency, int coefficient, double min_sum, double max_sum, Periodicity periodicity, int periodicity_value, Periodicity term_unit, int min_term, int max_term, bool spec_offer = false)
        {
            UId = uid;
            Name = name;
            Currency = currency;
            Coefficient = coefficient;
            MinSum = min_sum;
            MaxSum = max_sum;
            Periodicity = periodicity;
            PeriodicityValue = periodicity_value;
            TermUnit = term_unit;
            MinTerm = min_term;
            MaxTerm = max_term;
            SpecialOffer = spec_offer;
        }
        public Tariff(int uid, string name, Currency currency, int coefficient, double min_sum, double max_sum, Periodicity periodicity, int periodicity_value, Periodicity term_unit, int min_term, int max_term, object property_pledged, bool spec_offer = false)
        {
            UId = uid;
            Name = name;
            Currency = currency;
            Coefficient = coefficient;
            MinSum = min_sum;
            MaxSum = max_sum;
            Periodicity = periodicity;
            PeriodicityValue = periodicity_value;
            TermUnit = term_unit;
            MinTerm = min_term;
            MaxTerm = max_term;
            PropertyPledged = property_pledged;
            SpecialOffer = spec_offer;
        }

        public override string ToString()
        {
            string str = Name + Environment.NewLine;
            str += Properties.Resources.Currency + ": " + Currency.Name + Environment.NewLine;
            str += Properties.Resources.Under + ": " + Coefficient + "%" + Environment.NewLine;
            str += Properties.Resources.Amount + ": ";
            if (MinSum == 0 & MaxSum == 0) { str += Properties.Resources.NotLimited.ToLower(); }
            else if (MinSum == 0 & MaxSum > 0) { str += Properties.Resources.Before.ToLower() + " " + MaxSum.ToString("N3") + " " + Currency.Abbreviation; }
            else if (MinSum > 0 & MaxSum == 0) { str += Properties.Resources.From.ToLower() + " " + MinSum.ToString("N3") + " " + Currency.Abbreviation; }
            else { str += Properties.Resources.From.ToLower() + " " + MinSum.ToString("N3") + " " + Currency.Abbreviation + " " + Properties.Resources.Before.ToLower() + " " + MaxSum.ToString("N3") + " " + Currency.Abbreviation; }
            return str;
        }
    }

    public class PlayerTariff : Tariff
    {
        public Service Service { get; set; }
        public double Amount { get; set; }
        public int Term { get; set; }
        public DateTime StartDateOfService { get; set; }
        public PlayerTariff(int uid, string name, Currency currency, int coefficient, double min_sum, double max_sum, Periodicity periodicity, int periodicity_value, Periodicity term_unit, int min_term, int max_term, Service service, double amount, int term, DateTime start_date, bool spec_offer = false)
                     : base(uid, name, currency, coefficient, min_sum, max_sum, periodicity, periodicity_value, term_unit, min_term, max_term, spec_offer)
        {
            Service = service;
            Amount = amount;
            Term = term;
            StartDateOfService = start_date;
        }

        public PlayerTariff(int uid, string name, Currency currency, int coefficient, double min_sum, double max_sum, Periodicity periodicity, int periodicity_value, Periodicity term_unit, int min_term, int max_term, Service service, double amount, int term, DateTime start_date, object property_pledged, bool spec_offer = false)
                        : base(uid, name, currency, coefficient, min_sum, max_sum, periodicity, periodicity_value, term_unit, min_term, max_term, property_pledged, spec_offer)
        {
            Service = service;
            Amount = amount;
            Term = term;
            StartDateOfService = start_date;
        }

        public override string ToString()
        {
            string str = Name + Environment.NewLine;
            str += Properties.Resources.Currency + ": " + Currency.Name + Environment.NewLine +
            Properties.Resources.Under + ": " + Coefficient + "%" + Environment.NewLine;
            if (Service.Type == TransactionType.TopUp)
            {
                str += Properties.Resources.Invested + ": " + Amount.ToString("N3") + " " + Currency.Abbreviation;
            }
            else if (Service.Type == TransactionType.Withdraw) { str += Properties.Resources.Received + ": " + Amount.ToString("N3") + " " + Currency.Abbreviation; }
            str += Environment.NewLine +
            Properties.Resources.DateOfServiceContract + ": " + StartDateOfService.ToString("dd.MM.yyyy HH:mm");
            return str;
        }
    }

    public class Service
    {
        public int UId { get; set; }
        public string SystemName { get; set; }
        public string Name { get; set; }
        public TransactionType Type { get; set; }
        public Collection<Tariff> Tariffs { get; set; } = new Collection<Tariff>();
        public double TotalMaxDebt { get; set; } //Indicated in UGC (Universal Game Currency)
        public double TotalMaxContribution { get; set; } //Indicated in UGC (Universal Game Currency)
        public bool IsSystem { get; set; }

        public Service(int uid, string system_name, string name, TransactionType type, Collection<Tariff> tariffs, bool isSystem, double tmd = 0, double tmc = 0)
        {
            UId = uid;
            SystemName = system_name;
            Name = name;
            Type = type;
            Tariffs = tariffs;
            TotalMaxDebt = tmd;
            TotalMaxContribution = tmc;
            IsSystem = isSystem;
        }
    }
}
