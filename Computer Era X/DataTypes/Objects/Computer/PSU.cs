using Computer_Era_X.DataTypes.Enums;
using System;
using Computer_Era_X.Properties;
using Computer_Era_X.Models;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class PowerSupplyUnitProperties
    {
        public PSUTypes PSUType { get; set; }   //Form factor
        public int Power { get; set; }          //Power W
        public TypeConnectorMotherboard TypeCM { get; set; }    //Type of power connector to the motherboard

        public int Pin4plus4CPU { get; set; }   //Number of pins 4 + 4 CPU
        public int Pin6plus2PCIE { get; set; }  //Number of pins 6+2 PCI-E
        public int Pin6PCIE { get; set; }       //The number of PCI-E pins 6
        public int Pin8PCIE { get; set; }       //The number of PCI-E pins 8
        public int Pin15SATA { get; set; }      //Number of 15 pins SATA
        public int Pin4IDE { get; set; }        //Number of 4 pins IDE
        public int Pin4Floppy { get; set; }     //Number of 4 pins Floppy

        public int MinNoiseLevel { get; set; }  //Minimum noise Level dBA
        public int MaxNoiseLevel { get; set; }  //Maximum noise level dBA

        public bool OvervoltageProtection { get; set; }
        public bool OverloadProtection { get; set; }
        public bool ShortCircuitProtection { get; set; }
    }

    public class PowerSupplyUnit : Item<PowerSupplyUnitProperties>
    {
        public PowerSupplyUnit(int uid,
                               string name,
                               string type,
                               double price,
                               DateTime man_date,
                               PowerSupplyUnitProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public PowerSupplyUnit(Item item) : base(item) { }

        public override string Info()
        {
            string info = Resources.Name + ": " + Name + Environment.NewLine;
            info += Resources.FormFactor + ": " + Properties.PSUType + Environment.NewLine;
            info += Resources.Power + ": " + Properties.Power + " W" + Environment.NewLine;
            info += Resources.PowerToTheMotherboard + ": " + Properties.TypeCM + Environment.NewLine;
            info += Resources.NumberOfPinsCPU4plus4 + ": " + Properties.Pin4plus4CPU + Environment.NewLine;
            info += Resources.NumberOfPCIE6plus2Pins + ": " + Properties.Pin6plus2PCIE + Environment.NewLine;
            info += Resources.TheNumberOfPinsPCIE6 + ": " + Properties.Pin6PCIE + Environment.NewLine;
            info += Resources.TheNumberOfPinsPCIE8 + ": " + Properties.Pin8PCIE + Environment.NewLine;
            info += Resources.NumberOfSATA15Pins + ": " + Properties.Pin15SATA + Environment.NewLine;
            info += Resources.NumberOfIDE4Pins + ": " + Properties.Pin4IDE + Environment.NewLine;
            info += Resources.NumberOfPinsIDE4Floppy + ": " + Properties.Pin4Floppy + Environment.NewLine;
            info += Resources.NoiseLevel + ": " + Properties.MinNoiseLevel + " - " + Properties.MaxNoiseLevel + Environment.NewLine;
            info += Resources.OvervoltageProtection + ": " + (Properties.OvervoltageProtection ? Resources.Yes : Resources.No) + Environment.NewLine;
            info += Resources.OvervoltageProtection + ": " + (Properties.OverloadProtection ? Resources.Yes : Resources.No) + Environment.NewLine;
            info += Resources.ShortCircuitProtection + ": " + (Properties.ShortCircuitProtection ? Resources.Yes : Resources.No);
            return info;
        }

        public bool CheckCompatibility(CaseProperties @case)
        {
            foreach (PSUTypes type in @case.FormFactorPSU)
            {
                if (type == Properties.PSUType) { return true; }
            }

            return false;
        }
    }

}
