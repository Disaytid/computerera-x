using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Dictionaries;
using System;
using System.Windows.Media;
using Computer_Era_X.DataTypes.Interfaces;
using Computer_Era_X.DataTypes.Objects;
using Newtonsoft.Json;  

namespace Computer_Era_X.Models
{
    public class BaseItem : IItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public DateTime ManufacturingDate { get; set; }

        public string GetIcon(ItemTypes type)
        {
            if (!DItems.ItemIcon.ContainsKey(type)) throw new ArgumentException($@"Operation {type} is invalid", nameof(type));
            return DItems.ItemIcon[type];
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
        protected Item(int uid, string name, string type, int price, DateTime manDate, TP properties)
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
}
