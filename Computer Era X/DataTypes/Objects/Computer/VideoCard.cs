using System;
using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using Computer_Era_X.Properties;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class VideoCardProperties
    {
        public string GraphicsProcessor { get; set; }
        public Interface Interface { get; set; }
        public Resolution MaxResolution { get; set; }
        public int GPUFrequency { get; set; } //MHz
        public int VideoMemory { get; set; } //Kb
        public TypeVideoMemory TypeVideoMemory { get; set; }
        public int VideoMemoryFrequency { get; set; } //MHz
        public Collection<VideoInterface> VideoInterfaces { get; set; } = new Collection<VideoInterface>();
    }

    public class VideoCard : Item<VideoCardProperties>
    {
        public VideoCard(int uid, string name, string type, double price, DateTime man_date, VideoCardProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public VideoCard(Item item) : base(item) { }

        public bool IsCompatibility(MotherboardProperties motherboard)
        {
            if (Properties.Interface == Interface.PCI_E16x3_0 && motherboard.PCI_Ex16 >= 1 && motherboard.PCIE3_0 == true)
            {
                return true;
            } else {
                return false;
            }
        }
        public override string Info()
        {
            string info = Resources.Name + ": " + Name + Environment.NewLine;
            return info;
        }
        public int Compatibility(MotherboardProperties motherboard)
        {
            if (Properties.Interface == Interface.PCI_E16x3_0 && motherboard.PCI_Ex16 >= 1 && motherboard.PCIE3_0 == true)
            {
                return motherboard.PCI_Ex16;
            } else {
                return 0;
            }
        }
    }

}
