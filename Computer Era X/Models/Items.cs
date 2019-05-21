using System;
using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Objects.Computer;

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
                        break;
                    case ItemTypes.Monitor:
                        break;
                    case ItemTypes.VideoCard:
                        break;
                    case ItemTypes.OpticalDrive:
                        break;
                    case ItemTypes.Mouse:
                        break;
                    case ItemTypes.Keyboard:
                        break;
                    case ItemTypes.OpticalDisc:
                        break;
                    case ItemTypes.OS:
                        break;
                    case ItemTypes.Program:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
