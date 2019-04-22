using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

    public class BaseCurrency
    {
        public int ID { get; set; }
        public string SystemName { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public DateTime DateAppearance { get; set; }
        public double Course { get; set; }
    }

    public class Currency
    {
        public int ID { get; }
        public ImageSource Icon { get; }
        public string SystemName { get; }
        public string Name { get; }
        public string Abbreviation { get; }
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
            ID = id;
            SystemName = systemName;
            Name = name;
            Abbreviation = abbreviation;
            DateAppearance = dateAppearance;
            Course = course;
            Count = count;
        }
        public Currency(BaseCurrency currency)
        {
            ID = currency.ID;
            SystemName = currency.SystemName;
            Name = currency.Name;
            Abbreviation = currency.Abbreviation;
            DateAppearance = currency.DateAppearance;
            Course = currency.Course;

            string path = "Assets/Icons/" + SystemName + ".png";
            Uri uri = new Uri("pack://application:,,,/" + path);

            if (System.IO.File.Exists(System.IO.Path.GetFullPath(path)) == false)
            {
                uri = new Uri("pack://application:,,,/Assets/Icons/coin.png");
            }

            Icon = new BitmapImage(uri);
        }
    }
}
