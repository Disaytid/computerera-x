using System;
using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using Computer_Era_X.Properties;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class HDDProperties
    {
        public HDDFormFactor FormFactor { get; set; }
        public int Volume { get; set; }         // In kilobytes
        public int WriteSpeed { get; set; }     // In kilobytes per second
        public int ReadSpeed { get; set; }      // In kilobytes per second
        public int BufferCapacity { get; set; }
        public HDDInterface Interface { get; set; }
        public int MaximumTemperature { get; set; } // Degrees centigrade
        public Collection<Partition> Partitions { get; set; } = new Collection<Partition>();
    }

    public class HDD : Item<HDDProperties>
    {
        public HDD(int uid, string name, string type, double price, DateTime man_date, HDDProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public HDD(Item item) : base(item) { }

        public override string Info()
        {
            string info = Resources.Name + ": " + Name + Environment.NewLine;
            info += Resources.FormFactor + ": " + (Properties.FormFactor == HDDFormFactor.ThreeFive ? "3.5" : "2.5") + Environment.NewLine;
            info += Resources.Volume + ": " + Properties.Volume + " " + Resources.Kbyte + Environment.NewLine;
            info += Resources.WriteSpeed + ": " + Properties.WriteSpeed + " " + Resources.KBs + Environment.NewLine;
            info += Resources.ReadSpeed + ": " + Properties.ReadSpeed + " " + Resources.KBs + Environment.NewLine;
            info += Resources.Interface + ": " + (Properties.Interface == HDDInterface.Sata2_0 ? "SATA 2.0" : "SATA 3.0") + Environment.NewLine;
            info += Resources.MaximumWorkingTemperature + ": " + Properties.MaximumTemperature + " °C";
            return info;
        }

        public int Compatibility(MotherboardProperties motherboard)
        {
            if (Properties.Interface == HDDInterface.Sata2_0 || Properties.Interface == HDDInterface.Sata3_0)
            {
                return motherboard.SATA2_0 + motherboard.SATA3_0;
            }
            else if (Properties.Interface == HDDInterface.IDE)
            {
                return motherboard.IDE;
            }
            else
            {
                return 0;
            }
        }
    }
}
