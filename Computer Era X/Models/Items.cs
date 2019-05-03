using System;
using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Objects.Computer;

namespace Computer_Era_X.Models
{
    public class Items
    {
        public ObservableCollection<Case> CaseCollection = new ObservableCollection<Case>();
        public ObservableCollection<CPU> CPUCollection = new ObservableCollection<CPU>();

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
                        break;
                    case ItemTypes.RAM:
                        break;
                    case ItemTypes.PSU:
                        break;
                    case ItemTypes.CPU:
                        CPUCollection.Add(new CPU(item));
                        break;
                    case ItemTypes.CPUCooler:
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
