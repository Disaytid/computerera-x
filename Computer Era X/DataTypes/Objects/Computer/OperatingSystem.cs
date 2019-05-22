using System;
using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class OperatingSystemProperties
    {
        public string Description { get; set; }
        public string Author { get; set; }
        public int[] Programms { get; set; }
        public int Size { get; set; } //In kilobytes
        public Collection<FileSystem> FileSystems { get; set; } = new Collection<FileSystem>();
    }

    public class OperatingSystem : Item<OperatingSystemProperties>
    {
        public OperatingSystem(int uid, string name, string type, double price, DateTime man_date, OperatingSystemProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public OperatingSystem(Item item) : base(item) { }
        public override string Info()
        {
            return Properties.Description;
        }
    }
}
