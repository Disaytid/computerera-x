using System;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class CPUProperties
    {
        public Sockets Socket { get; set; }
        public int NumberCores { get; set; }
        public int MinCPUFrequency { get; set; } //MHz
        public int MaxCPUFrequency { get; set; } //MHz
        public int MinHeatDissipation { get; set; }
        public int MaxHeatDissipation { get; set; }
        public int MaximumTemperature { get; set; } //degrees Celsius
    }
    public class CPU : Item<CPUProperties>
    {
        public CPU(int uid, string name, string type, int price, DateTime manDate, CPUProperties properties) : base(uid, name, type, price, manDate, properties) { }
        public CPU(Item item) : base (item) { }
        public override string Info()
        {
            var info = Computer_Era_X.Properties.Resources.Name + ": " + Name + Environment.NewLine;
            info += Computer_Era_X.Properties.Resources.Socket + ": " + Properties.Socket + Environment.NewLine;
            info += Computer_Era_X.Properties.Resources.NumberOfCores + ": " + Properties.NumberCores + Environment.NewLine;
            info += Computer_Era_X.Properties.Resources.CPUFrequency + ": " + Properties.MinCPUFrequency + " - " + Properties.MaxCPUFrequency + Environment.NewLine;
            info += Computer_Era_X.Properties.Resources.HeatDissipation + ": " + Properties.MinHeatDissipation + " - " + Properties.MaxHeatDissipation + Environment.NewLine;
            info += Computer_Era_X.Properties.Resources.MaximumWorkingTemperature + ": " + Properties.MaximumTemperature;
            return info;
        }
    }
}
