using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.Models;
using System;
using System.Collections.ObjectModel;
using Computer_Era_X.Properties;

namespace Computer_Era_X.DataTypes.Objects.Computer
{
    public class MotherboardProperties
    {
        public MotherboardTypes MotherboardType;
        public Sockets Socket;
        public bool MultiCoreProcessor;
        public string Chipset;
        public MotherboardBIOS BIOS;
        public bool EFI; //EFI support

        public RAMTypes RamType; //Memory type supported
        public int RAMSlots; //Number of memory slots
        public int MinFrequency;
        public int MaxFrequency;
        public int RAMVolume; //Maximum supported memory

        public int IDE;         //Number of IDE sockets
        public int SATA2_0;     //Number of SATA 2.0 sockets
        public int SATA3_0;     //Number of SATA 3.0 sockets
        public int PCI;         //The number of PCI buses
        public int PCI_Ex1;     //The number of PCI-Ex1 buses
        public int PCI_Ex4;     //The number of PCI-Ex4 buses
        public int PCI_Ex8;     //The number of PCI-Ex8 buses
        public int PCI_Ex16;    //The number of PCI-Ex16 buses
        public bool PCIE2_0;    //PCI-Express 2.0 support
        public bool PCIE3_0;    //PCI-Express 3.0 support
        public bool EmbeddedGraphics; //Embedded Graphics Support
        public Collection<VideoInterface> VideoInterfaces = new Collection<VideoInterface>();  //Monitor sockets     
        public bool Sound;          //Availability of built-in sound card
        public int EthernetSpeed;   //Network card speed (if 0 card is missing or out of order) kilobits per second
        public bool PS2Keyboard;    //PS/2 for keyboard
        public bool PS2Mouse;       //PS/2 for mouse
        public int USB2_0;          //Number of USB 2.0 sockets
        public int USB3_0;          //Number of USB sockets 3.0
    }

    public class Motherboard : Item<MotherboardProperties>
    {
        public Motherboard(int uid, string name, string type, int price, DateTime man_date, MotherboardProperties properties) : base(uid, name, type, price, man_date, properties) { }
        public Motherboard(Item item) : base(item) { }

        public override string Info()
        {
            string info = Resources.Name + ": " + Name + Environment.NewLine;
            info += Resources.MotherboardType + ": " + Properties.MotherboardType + Environment.NewLine;
            info += Resources.Socket + ": " + Properties.Socket + Environment.NewLine;
            info += Resources.MultiCoreProcessorSupport + ": " + (Properties.MultiCoreProcessor ? Resources.Yes : Resources.No) + Environment.NewLine;
            info += Resources.Chipset + ": " + Properties.Chipset + Environment.NewLine;
            info += Resources.BIOS + ": " + Properties.BIOS + Environment.NewLine;
            info += Resources.EFISupport + ": " + (Properties.EFI ? Resources.Yes : Resources.No) + Environment.NewLine;

            info += Environment.NewLine + Resources.Memory + Environment.NewLine;
            info += Resources.Type + ": " + Properties.RamType + Environment.NewLine;
            info += Resources.NumberOfSlots + ": " + Properties.RAMSlots + Environment.NewLine;
            info += Resources.Frequency + ": " + Properties.MinFrequency + " - " + Properties.MaxFrequency + Environment.NewLine;
            info += Resources.MaximumVolume + ": " + Properties.RAMVolume + Environment.NewLine + Environment.NewLine;

            info += Resources.NumberOfIDESockets + ": " + Properties.IDE + Environment.NewLine;
            info += Resources.NumberOfSATA20Sockets + ": " + Properties.SATA2_0 + Environment.NewLine;
            info += Resources.NumberOfSATA30Sockets + ": " + Properties.SATA3_0 + Environment.NewLine + Environment.NewLine;

            info += Resources.TheNumberOfPCIBuses + ": " + Properties.PCI + Environment.NewLine;
            info += Resources.NumberOfPCIExpressX1Buses + ": " + Properties.PCI_Ex1 + Environment.NewLine;
            info += Resources.NumberOfPCIExpressX4Buses + ": " + Properties.PCI_Ex4 + Environment.NewLine;
            info += Resources.NumberOfPCIExpressX8Buses + ": " + Properties.PCI_Ex8 + Environment.NewLine;
            info += Resources.NumberOfPCIExpressX16Buses + ": " + Properties.PCI_Ex16 + Environment.NewLine + Environment.NewLine;

            info += Resources.PCIExpress20Support + ": " + (Properties.PCIE2_0 ? Resources.Yes : Resources.No) + Environment.NewLine;
            info += Resources.PCIExpress30Support + ": " + (Properties.PCIE3_0 ? Resources.Yes : Resources.No) + Environment.NewLine;
            info += Resources.ThePresenceOfASoundCard + ": " + (Properties.PCIE3_0 ? Resources.Yes : Resources.No) + Environment.NewLine;
            info += Resources.NetworkCardSpeed + ": " + Properties.EthernetSpeed + Environment.NewLine + Environment.NewLine;

            info += Resources.PS2ForKeyboard + ": " + (Properties.PS2Keyboard ? Resources.Yes : Resources.No) + Environment.NewLine;
            info += Resources.PS2ForMouse + ": " + (Properties.PS2Mouse ? Resources.Yes : Resources.No) + Environment.NewLine;
            info += Resources.USB20Sockets + ": " + Properties.USB2_0 + Environment.NewLine;
            info += Resources.USB30Sockets + ": " + Properties.USB3_0;
            return info;
        }

        public bool CheckCompatibility(CaseProperties @case)
        {
            foreach (MotherboardTypes type in @case.FormFactor) if (type == Properties.MotherboardType) { return true; }
            return false;
        }
        public int GetCountCompatibleSlots(HDDFormFactor formFactor)
        {
            if (formFactor == HDDFormFactor.TwoFive || formFactor == HDDFormFactor.ThreeFive) return Properties.SATA2_0 + Properties.SATA3_0;
            return 0;
        }
    }
}
