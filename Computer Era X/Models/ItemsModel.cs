using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Dictionaries;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.DataTypes.Interfaces;

namespace Computer_Era_X.Models
{
    public class BaseItem : IItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public DateTime ManufacturingDate { get; set; }

        public string GetIcon()
        {
            Uri path = new Uri("pack://application:,,,/Assets/Items/" + Type + "/" + ID + ".png");
            if (File.Exists(Path.GetFullPath("." + path.AbsolutePath))) { return path.ToString(); }
            ItemTypes ltype = (ItemTypes)Enum.Parse(typeof(ItemTypes), Type);
            if (!DItems.ItemIcon.ContainsKey(ltype)) throw new ArgumentException($@"Operation {ltype} is invalid", nameof(ltype));
            return DItems.ItemIcon[ltype];
        }
        public string GetLocalizedType()
        {
            var itemType = (ItemTypes)Enum.Parse(typeof(ItemTypes), Type);
            if (!DItems.LocalizedItemTypes.ContainsKey(itemType)) throw new ArgumentException($@"Operation {itemType} is invalid", $"op");
            return DItems.LocalizedItemTypes[itemType];
        }
        public override string ToString() => Name;
    }

    public class Item : BaseItem
    {
        public string Properties { get; set; }
    }

    public abstract class Item<TP> : BaseItem
    {
        public ImageSource Image { get; set; }
        public TP Properties { get; set; }
        protected Item(int uid, string name, string type, double price, DateTime manDate, TP properties)
        {
            ID = uid;
            Name = name;
            Type = type;
            Price = price;
            ManufacturingDate = manDate;
            Properties = properties;
        }
        protected Item(Item item)
        {
            ID = item.ID;
            Name = item.Name;
            Type = item.Type;
            Price = item.Price;
            ManufacturingDate = item.ManufacturingDate;
            Properties = JsonConvert.DeserializeObject<TP>(item.Properties);
        }

        public abstract string Info();
    }

    public class Product : BaseItem
    {
        public double ShopPrice { get; set; }
        public string LocalizedType { get; set; }
        public string Description { get; set; }
        public Currency Currency { get; set; }

        public Product(BaseItem item, double shopPrice, Currency currency, string description)
        {
            ID = item.ID;
            Name = item.Name;
            Type = item.Type;
            LocalizedType = item.GetLocalizedType();
            Price = item.Price;
            ManufacturingDate = item.ManufacturingDate;
            ShopPrice = shopPrice;
            Description = description;
            Currency = currency;
        }
    }

    public class InventoryItem : BaseItem
    {
        public ImageSource Image { get; set; }
        public string LocalizedType { get; set; }
        public string Description { get; set; }
        public Currency Currency { get; set; }
        public double PriceInCurrency { get; set; }
        public InventoryItem(BaseItem item, string description, Currency currency)
        {
            ID = item.ID;
            Name = item.Name;
            Type = item.Type;
            LocalizedType = item.GetLocalizedType();
            Price = item.Price;
            PriceInCurrency = item.Price * currency.Course;
            ManufacturingDate = item.ManufacturingDate;
            Description = description;
            Image = new BitmapImage(new Uri(item.GetIcon()));
            Currency = currency;
        }
    }
}
