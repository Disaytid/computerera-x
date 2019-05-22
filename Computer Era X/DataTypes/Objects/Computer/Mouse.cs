using System;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using Computer_Era_X.Properties;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class MouseProperties
    {
        public InputInterfaces Interface { get; set; }
    }
    public class Mouse : Item<MouseProperties>
    {
        public Mouse(int uid, string name, string type, double price, DateTime man_date, MouseProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public Mouse(Item item) : base(item) { }

        public override string Info()
        {
            string info = Resources.Name + ": " + Name + Environment.NewLine;
            return info;
        }
        public int Compatibility(MotherboardProperties motherboard)
        {
            if (Properties.Interface == InputInterfaces.USB)
            {
                return motherboard.USB2_0 + motherboard.USB3_0;
            } else if (Properties.Interface == InputInterfaces.PSby2 && motherboard.PS2Mouse) {
                return 1;
            } else {
                return 0;
            }
        }

        public int Compatibility(MotherboardProperties motherboard, CaseProperties @case)
        {
            if (Properties.Interface == InputInterfaces.USB)
            {
                return motherboard.USB2_0 + motherboard.USB3_0 + @case.USB2_0 + @case.USB3_0;
            } else if (Properties.Interface == InputInterfaces.PSby2 && motherboard.PS2Mouse) {
                return 1;
            } else {
                return 0;
            }
        }
    }
}
