using System;
using Computer_Era_X.Models;
using Computer_Era_X.Properties;
using Computer_Era_X.DataTypes.Enums;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class OpticalDriveProperties
    {
        public OpticalDriveInterface Interface { get; set; }
        public Size Size { get; set; }
        public int[] MaxWritingSpeed { get; set; } //CD-R, CD-RW, DVD-R, DVD-R DL, DVD-RW, DVD+R, DVD+R DL, DVD+RW, DVD-RAM x
        public int MaxReadSpeedCD { get; set; }     //x
        public int MaxReadSpeedDVD { get; set; }    //x
        public int ReadAccessTimeCD { get; set; }   // Access time in read mode CD in milliseconds
        public int ReadAccessTimeDVD { get; set; }  // DVD access time in milliseconds
        public OpticalDisc OpticalDisc { get; set; }
        public string Letter { get; set; }
    }
    public class OpticalDrive : Item<OpticalDriveProperties>
    {
        public OpticalDrive(int uid, string name, string type, double price, DateTime man_date, OpticalDriveProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public OpticalDrive(Item item) : base(item) { }

        public override string Info()
        {
            string info = Resources.Name + ": " + Name + Environment.NewLine;
            return info;
        }
        public int Compatibility(MotherboardProperties motherboard)
        {
            if (Properties.Interface == OpticalDriveInterface.SATA)
            {
                return motherboard.SATA2_0 + motherboard.SATA3_0;
            } else if (Properties.Interface == OpticalDriveInterface.IDE) {
                return motherboard.IDE;
            } else {
                return 0;
            }
        }
    }

}
