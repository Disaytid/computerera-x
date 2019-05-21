using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using System;
using System.Collections.ObjectModel;
using Computer_Era_X.Properties;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class CPUCoolerProperties
    {
        public Collection<Sockets> Sockets { get; set; } = new Collection<Sockets>();
        public int MinRotationalSpeed { get; set; }
        public int MaxRotationalSpeed { get; set; }
        public int AirFlow { get; set; }            // Airflow in CFM
        public double MinNoiseLevel { get; set; }   // Minimum noise level dB
        public double MaxNoiseLevel { get; set; }   // Maximum noise level dB
        public bool SpeedController { get; set; }
        public Size Size { get; set; }
    }

    public class CPUCooler : Item<CPUCoolerProperties>
    {
        public CPUCooler(int uid, string name, string type, double price, DateTime man_date, CPUCoolerProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public CPUCooler(Item item) : base(item) { }
        public override string Info()
        {
            string info = Resources.Name + ": " + Name + Environment.NewLine;
            info += Resources.Socket + ": "; foreach (Sockets socket in Properties.Sockets) { info += socket + ", "; }
            info = info.Remove(info.Length - 2, 2); info += Environment.NewLine;
            info += Resources.MinimumRotationSpeed + ": " + Properties.MinRotationalSpeed + Environment.NewLine;
            info += Resources.MaximumRotationSpeed + ": " + Properties.MaxRotationalSpeed + Environment.NewLine;
            info += Resources.AirFlow + ": " + Properties.AirFlow + " CFM" + Environment.NewLine;
            info += Resources.NoiseLevel + ": " + Properties.MinNoiseLevel + " - " + Properties.MaxNoiseLevel + Environment.NewLine;
            info += Resources.SpeedControl + ": " + (Properties.SpeedController ? Resources.Yes : Resources.No) + Environment.NewLine;
            info += Resources.Size + ": " + Properties.Size.Width + "x" + Properties.Size.Height + "x" + Properties.Size.Depth;
            return info;
        }

        public bool CheckCompatibility(CPUProperties cpu)
        {
            foreach (Sockets type in Properties.Sockets)
            {
                if (type == cpu.Socket) { return true; }
            }

            return false;
        }
    }
}
