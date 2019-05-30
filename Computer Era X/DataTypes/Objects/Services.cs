using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Computer_Era_X.Properties;

namespace Computer_Era_X.DataTypes.Objects
{
    public class Service
    {
        public int ID { get; set; }
        public string SystemName { get; set; }
        public string Name { get; set; }
        public int TransactionType { get; set; }
        public double TotalMaxDebt { get; set; } //Indicated in UGC (Universal Game Currency)
        public double TotalMaxContribution { get; set; } //Indicated in UGC (Universal Game Currency)
        public bool IsSystem { get; set; }
        public virtual ObservableCollection<Tariff> Tariffs { get; set; }

        public Service()
        {
            Tariffs = new ObservableCollection<Tariff>();
        }
    }

    public class Tariff
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public BaseCurrency BaseCurrency { get; set; }
        public int Coefficient { get; set; }
        public double MinSum { get; set; }
        public double MaxSum { get; set; }
        public int Periodicity { get; set; }
        public int PeriodicityValue { get; set; }
        public int TermUnit { get; set; }
        public int MinTerm { get; set; }
        public int MaxTerm { get; set; }
        public bool SpecialOffer { get; set; }
        public virtual ObservableCollection<Service> Services { get; set; }

        public Tariff()
        {
            Services = new ObservableCollection<Service>();
        }

        public string Info()
        {
            string str = Name + Environment.NewLine +
                         Resources.Currency + ": " + BaseCurrency.Name + Environment.NewLine +
                         Resources.Under + ": " + Coefficient + "%" + Environment.NewLine +
                         Resources.Amount + ": ";
            if (MinSum == 0 & MaxSum == 0) { str += Resources.NotLimited.ToLower(); }
            else if (MinSum == 0 & MaxSum > 0) { str += Resources.Before.ToLower() + " " + MaxSum.ToString("N3") + " " + BaseCurrency.Abbreviation; }
            else if (MinSum > 0 & MaxSum == 0) { str += Resources.From.ToLower() + " " + MinSum.ToString("N3") + " " + BaseCurrency.Abbreviation; }
            else { str += Resources.From.ToLower() + " " + MinSum.ToString("N3") + " " + BaseCurrency.Abbreviation + " " + Resources.Before.ToLower() + " " + MaxSum.ToString("N3") + " " + BaseCurrency.Abbreviation; }
            return str;
        }
    }

    [NotMapped]
    public class PlayerTariff : Tariff
    {
        public Service Service { get; set; }
        public double Amount { get; set; }
        public int Term { get; set; }
        public DateTime StartDateOfService { get; set; }
        public object PropertyPledged { get; set; }
        public PlayerTariff(Tariff tariff, Service service, double amount, int term, DateTime start_date, bool spec_offer = false, object propertyPledged = null)
        {
            ID = tariff.ID;
            Name = tariff.Name;
            BaseCurrency = tariff.BaseCurrency;
            Coefficient = tariff.Coefficient;
            MinSum = tariff.MinSum;
            MaxSum = tariff.MaxSum;
            Periodicity = tariff.Periodicity;
            PeriodicityValue = tariff.PeriodicityValue;
            TermUnit = tariff.TermUnit;
            MinTerm = tariff.MinTerm;
            MaxTerm = tariff.MaxTerm;
            SpecialOffer = tariff.SpecialOffer;
            PropertyPledged = propertyPledged;
            Services = tariff.Services;

            Service = service;
            Amount = amount;
            Term = term;
            StartDateOfService = start_date;
        }

        public new string Info()
        {
            string str = Name + Environment.NewLine +
                         Resources.Currency + ": " + BaseCurrency.Name + Environment.NewLine +
                         Resources.Under + ": " + Coefficient + "%" + Environment.NewLine;
            if (Service.TransactionType == 0)
            {
                str += Resources.Invested + ": " + Amount.ToString("N3") + " " + BaseCurrency.Abbreviation;
            } else if (Service.TransactionType == 1) { str += Resources.Received + ": " + Amount.ToString("N3") + " " + BaseCurrency.Abbreviation; }
            str += Environment.NewLine +
                   Resources.DateOfConclusionOfTheService + ": " + StartDateOfService.ToString("dd.MM.yyyy HH:mm");
            return str;
        }
    }
}
