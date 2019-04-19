using System;
using System.Collections.ObjectModel;

namespace Computer_Era_X.DataTypes.Objects
{
    public enum TransactionType
    {
        TopUp,
        Withdraw
    }
    public class Transaction
    {
        public string Name { get; set; }
        public string Initiator { get; set; }
        public DateTime DateTime { get; set; } //Date and time of the transaction
        public double Sum { get; set; }
        public TransactionType Type { get; set; }
        public Transaction(string name, string initiator, DateTime dateTime, double sum, TransactionType type)
        {
            Name = name;
            Initiator = initiator;
            DateTime = dateTime;
            Sum = sum;
            Type = type;
        }
    }

    public class Currency
    {
        public int Id { get; }
        public string SystemName { get;}
        public string Name { get; }
        public string Abbreviation { get;}
        public DateTime DateAppearance { get; }
        public double Course { get; }
        public Collection<Transaction> TransactionHistory { get; } = new Collection<Transaction>();
        public double Count { get; private set; }

        public bool Withdraw(string name, string initiator, DateTime dateTime, double amount)
        {
            if (!(amount <= Count & amount > 0)) return false;
            Count -= amount;
            TransactionHistory.Add(new Transaction(name, initiator, dateTime, amount, TransactionType.Withdraw));
            return true;
        }

        public bool TopUp(string name, string initiator, DateTime dateTime, double amount)
        {
            if (!(amount > 0)) return false;
            Count += amount;
            TransactionHistory.Add(new Transaction(name, initiator, dateTime, amount, TransactionType.TopUp));
            return true;
        }

        public Currency(int id, string systemName, string name, string abbreviation, DateTime dateAppearance, double course, double count)
        {
            Id = id;
            SystemName = systemName;
            Name = name;
            Abbreviation = abbreviation;
            DateAppearance = dateAppearance;
            Course = course;
            Count = count;
        }
    }
}
