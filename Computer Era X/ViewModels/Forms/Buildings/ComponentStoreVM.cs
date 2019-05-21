using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Computer_Era_X.DataTypes.Dictionaries;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.DataTypes.Objects.Computer;
using Computer_Era_X.Models;
using Computer_Era_X.Properties;
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
            Buy = new DelegateCommand<Product>(BuyItem);
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
                    foreach (var cpuCooler in GameEnvironment.Items.CPUCoolerCollection.Where(item => item.ManufacturingDate.Year >= year))
                        ItemsCollection.Add(new Product(cpuCooler,
                            (cpuCooler.Price + cpuCooler.Price / 100 * StoreComponentPercentage) *
                            GameEnvironment.Player.Money[0].Course, GameEnvironment.Player.Money[0], cpuCooler.Info()));
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

        private void BuyItem(Product product)
        {
            if (product.Currency.Withdraw(string.Format(Resources.BuyingAX, product.Name), Resources.ComponentStoreFullName, GameEnvironment.Events.Timer.DateTime, product.ShopPrice))
            {
                switch ((ItemTypes)Enum.Parse((typeof(ItemTypes)), product.Type))
                {
                    case ItemTypes.Case:
                        Case @case = GameEnvironment.Items.CaseCollection.Where(i => i.ID == product.ID).ToList()[0];
                        GameEnvironment.Player.Items.CaseCollection.Add(new Case(@case.ID, @case.Name, @case.Type, @case.Price, @case.ManufacturingDate, @case.Properties));
                        break;
                    case ItemTypes.Motherboard:
                        Motherboard motherboard = GameEnvironment.Items.MotherboardCollection.Where(i => i.ID == product.ID).ToList()[0];
                        GameEnvironment.Player.Items.MotherboardCollection.Add(new Motherboard(motherboard.ID, motherboard.Name, motherboard.Type, motherboard.Price, motherboard.ManufacturingDate, motherboard.Properties));
                        break;
                    case ItemTypes.RAM:
                        RAM ram = GameEnvironment.Items.RAMCollection.Where(i => i.ID == product.ID).ToList()[0];
                        GameEnvironment.Player.Items.RAMCollection.Add(new RAM(ram.ID, ram.Name, ram.Type, ram.Price, ram.ManufacturingDate, ram.Properties));
                        break;
                    case ItemTypes.PSU:
                        PowerSupplyUnit psu = GameEnvironment.Items.PSUCollection.Where(i => i.ID == product.ID).ToList()[0];
                        GameEnvironment.Player.Items.PSUCollection.Add(new PowerSupplyUnit(psu.ID, psu.Name, psu.Type, psu.Price, psu.ManufacturingDate, psu.Properties));
                        break;
                    case ItemTypes.CPU:
                        CPU cpu = GameEnvironment.Items.CPUCollection.Where(i => i.ID == product.ID).ToList()[0];
                        GameEnvironment.Player.Items.CPUCollection.Add(new CPU(cpu.ID, cpu.Name, cpu.Type, cpu.Price, cpu.ManufacturingDate, cpu.Properties));
                        break;
                    case ItemTypes.CPUCooler:
                        CPUCooler cpuCooler = GameEnvironment.Items.CPUCoolerCollection.Where(i => i.ID == product.ID).ToList()[0];
                        GameEnvironment.Player.Items.CPUCoolerCollection.Add(new CPUCooler(cpuCooler.ID, cpuCooler.Name, cpuCooler.Type, cpuCooler.Price, cpuCooler.ManufacturingDate, cpuCooler.Properties));
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
                }
                MessageBox.Show(Resources.ComponentStoreFullName, string.Format(Resources.GameMessage3, product.Name), MessageBoxType.Information);
            }
            else { MessageBox.Show(Resources.ComponentStoreFullName, Resources.GameMessage2, MessageBoxType.Warning); }
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
        public DelegateCommand<Product> Buy { get; private set; }
    }
} 

