using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Computer_Era_X.DataTypes.Dictionaries;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models;
using Computer_Era_X.Views;
using Prism.Commands;

namespace Computer_Era_X.ViewModels
{
    public partial class GameVM
    {
        private const int StoreComponentPercentage = 20; //Shop margin percentage
        private const int StorePeripheralsPercentage = 25;

        partial void ComponentStoreInit()
        {

        }
        private void SelectionCategory()
        {
            if (string.IsNullOrEmpty(SelectedCategory.Content.ToString())) return;
            var types = DItems.LocalizedItemTypes.Where(type => type.Value == SelectedCategory.Content.ToString()).ToArray();
            if (types.Length != 1) return;
            var itemType = types[0].Key;
            ListFormation(itemType);
        }
        private void ItemSelection()
        {
            if (SelectedItem == null) return;
        }
        private void ListFormation(ItemTypes itemTypes, bool yearFiltering = false)
        {
            var year = 0;
            if (yearFiltering)
                year = GameEnvironment.Events.Timer.DateTime.Year;

            ItemsCollection.Clear();
            switch (itemTypes)
            {
                case ItemTypes.Case:
                    foreach (var @case in GameEnvironment.Items.CaseCollection.Where(item => item.ManufacturingDate.Year >= year))
                        ItemsCollection.Add(new Product(@case,
                            (@case.Price + @case.Price / 100 * StoreComponentPercentage) *
                            GameEnvironment.Player.Money[0].Course, GameEnvironment.Player.Money[0], @case.Info()));
                    break;
                case ItemTypes.Motherboard:
                    foreach (var motherboard in GameEnvironment.Items.MotherboardCollection.Where(item => item.ManufacturingDate.Year >= year))
                        ItemsCollection.Add(new Product(motherboard,
                            (motherboard.Price + motherboard.Price / 100 * StoreComponentPercentage) *
                            GameEnvironment.Player.Money[0].Course, GameEnvironment.Player.Money[0], motherboard.Info()));
                    break;
                case ItemTypes.RAM:
                    foreach (var ram in GameEnvironment.Items.RAMCollection.Where(item => item.ManufacturingDate.Year >= year))
                        ItemsCollection.Add(new Product(ram,
                            (ram.Price + ram.Price / 100 * StoreComponentPercentage) *
                            GameEnvironment.Player.Money[0].Course, GameEnvironment.Player.Money[0], ram.Info()));
                    break;
                case ItemTypes.PSU:
                    foreach (var psu in GameEnvironment.Items.PSUCollection.Where(item => item.ManufacturingDate.Year >= year))
                        ItemsCollection.Add(new Product(psu,
                            (psu.Price + psu.Price / 100 * StoreComponentPercentage) *
                            GameEnvironment.Player.Money[0].Course, GameEnvironment.Player.Money[0], psu.Info()));
                    break;
                case ItemTypes.CPU:
                    foreach (var cpu in GameEnvironment.Items.CPUCollection.Where( item => item.ManufacturingDate.Year >= year))
                        ItemsCollection.Add(new Product(cpu,
                            (cpu.Price + cpu.Price / 100 * StoreComponentPercentage) *
                            GameEnvironment.Player.Money[0].Course, GameEnvironment.Player.Money[0], cpu.Info()));      
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
                default:
                    return;
            }
        }

        public Currency Currency => GameEnvironment.Player.Money[0];
        private ObservableCollection<Product> _itemsCollection = new ObservableCollection<Product>();
        private Product _selectedItem;
        private ComboBoxItem _selectedCategory;

        public ObservableCollection<Product> ItemsCollection
        {
            get => _itemsCollection;
            set => SetProperty(ref _itemsCollection, value);
        }
        public Product SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                ItemSelection();
            } 
        }
        public ComboBoxItem SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SetProperty(ref _selectedCategory, value);
                SelectionCategory();
            }
        }
    }
} 

