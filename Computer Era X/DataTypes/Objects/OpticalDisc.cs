using System;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using Computer_Era_X.Properties;

namespace Computer_Era_X.DataTypes.Objects
{
    public class OpticalDiscProperties
    {
        public OpticalDiscType Type { get; set; }
        public bool Rewritable { get; set; }
        public int Volume { get; set; }     // In kilobytes
        public int ReadSpeed { get; set; }  //In x, where, for CD, x = 150 kb / s, and for DVD, 1,352 kb / s (kilobytes per second)
        public int WriteSpeed { get; set; } //In x, where, for CD, x = 150 kb / s, and for DVD, 1,352 kb / s (kilobytes per second)
        public string CoverName { get; set; }
        public int OperatingSystem { get; set; }
        public int[] Programs { get; set; }
    }
    public class OpticalDisc : Item<OpticalDiscProperties>
    {
        public OpticalDisc(int uid, string name, string type, double price, DateTime man_date, OpticalDiscProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public OpticalDisc(Item item) : base(item) { }

        public override string Info()
        {
            string str = Resources.Type + ": " + Properties.Type + Environment.NewLine +
                         Resources.ReadSpeed + ": x" + Properties.ReadSpeed;
            return str;
        }
    }
}
