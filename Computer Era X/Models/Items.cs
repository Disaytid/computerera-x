using System;
using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.DataTypes.Objects.Computer;
using OperatingSystem = Computer_Era_X.DataTypes.Objects.Computer.OperatingSystem;

namespace Computer_Era_X.Models
{
    public class Items
    {
        public ObservableCollection<Case> CaseCollection = new ObservableCollection<Case>();
        public ObservableCollection<Motherboard> MotherboardCollection = new ObservableCollection<Motherboard>();
        public ObservableCollection<RAM> RAMCollection = new ObservableCollection<RAM>();
        public ObservableCollection<PowerSupplyUnit> PSUCollection = new ObservableCollection<PowerSupplyUnit>();
        public ObservableCollection<CPU> CPUCollection = new ObservableCollection<CPU>();
        public ObservableCollection<CPUCooler> CPUCoolerCollection = new ObservableCollection<CPUCooler>();
        public ObservableCollection<HDD> HDDCollection = new ObservableCollection<HDD>();
        public ObservableCollection<Monitor> MonitorCollection = new ObservableCollection<Monitor>();
        public ObservableCollection<VideoCard> VideoCardCollection = new ObservableCollection<VideoCard>();
        public ObservableCollection<OpticalDrive> OpticalDriveCollection = new ObservableCollection<OpticalDrive>();
        public ObservableCollection<Mouse> MouseCollection = new ObservableCollection<Mouse>();
        public ObservableCollection<Keyboard> KeyboardCollection = new ObservableCollection<Keyboard>();
        public ObservableCollection<OpticalDisc> OpticalDiscCollection = new ObservableCollection<OpticalDisc>();
        public ObservableCollection<OperatingSystem> OperatingSystemCollection = new ObservableCollection<OperatingSystem>();
        public ObservableCollection<Program> ProgramCollection = new ObservableCollection<Program>();

        public void LoadingItems(Collection<Item> items)
        {
            foreach (var item in items)
            {
                var type = (ItemTypes) Enum.Parse(typeof(ItemTypes), item.Type);
                switch (type)
                {
                    case ItemTypes.Case:
                        CaseCollection.Add(new Case(item));
                        break;
                    case ItemTypes.Motherboard:
                        MotherboardCollection.Add(new Motherboard(item));
                        break;
                    case ItemTypes.RAM:
                        RAMCollection.Add(new RAM(item));
                        break;
                    case ItemTypes.PSU:
                        PSUCollection.Add(new PowerSupplyUnit(item));
                        break;
                    case ItemTypes.CPU:
                        CPUCollection.Add(new CPU(item));
                        break;
                    case ItemTypes.CPUCooler:
                        CPUCoolerCollection.Add(new CPUCooler(item));
                        break;
                    case ItemTypes.HDD:
                        HDDCollection.Add(new HDD(item));
                        break;
                    case ItemTypes.Monitor:
                        MonitorCollection.Add(new Monitor(item));
                        break;
                    case ItemTypes.VideoCard:
                        VideoCardCollection.Add(new VideoCard(item));
                        break;
                    case ItemTypes.OpticalDrive:
                        OpticalDriveCollection.Add(new OpticalDrive(item));
                        break;
                    case ItemTypes.Mouse:
                        MouseCollection.Add(new Mouse(item));
                        break;
                    case ItemTypes.Keyboard:
                        KeyboardCollection.Add(new Keyboard(item));
                        break;
                    case ItemTypes.OpticalDisc:
                        OpticalDiscCollection.Add(new OpticalDisc(item));
                        break;
                    case ItemTypes.OS:
                        OperatingSystemCollection.Add(new OperatingSystem(item));
                        break;
                    case ItemTypes.Program:
                        ProgramCollection.Add(new Program(item));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
