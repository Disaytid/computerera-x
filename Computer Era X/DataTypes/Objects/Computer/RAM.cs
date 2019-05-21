using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using System;
using Computer_Era_X.Properties;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class RAMProperties
    {
        public RAMTypes RAMType { get; set; }
        public int ClockFrequency { get; set; } //MHz frequency
        public int Volume { get; set; }         //Volume in megabytes
        public double SupplyVoltage { get; set; }
    }

    public class RAM : Item<RAMProperties>
    {
        public RAM(int uid, string name, string type, double price, DateTime man_date, RAMProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public RAM(Item item) : base(item) { }
        public override string Info()
        {
            string info = Resources.Name + ": " + Name + Environment.NewLine;
            info += Resources.MemoryType + ": " + Properties.RAMType + Environment.NewLine;
            info += Resources.Frequency + ": " + Properties.ClockFrequency + Environment.NewLine;
            info += Resources.Volume + ": " + Properties.Volume + Environment.NewLine;
            info += Resources.Voltage + ": " + Properties.SupplyVoltage;
            return info;
        }
    }
}
