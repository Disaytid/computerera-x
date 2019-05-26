using System.Collections.ObjectModel;

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
        public int PropertyPledged { get; set; } //Property on bail
        public virtual ObservableCollection<Service> Services { get; set; }
        public Tariff()
        {
            Services = new ObservableCollection<Service>();
        }       
    }
}
