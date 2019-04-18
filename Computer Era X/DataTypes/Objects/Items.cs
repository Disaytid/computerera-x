using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Dictionaries;
using System;
using System.Windows.Media;
using Newtonsoft.Json;

namespace Computer_Era_X.DataTypes.Objects
{
    public class BaseItem
    {
        public int ID { get; set; }
        public ImageSource Image { get; set; }
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
    public abstract class Item<TP> : BaseItem
    {
        public TP Properties { get; set; }
        protected Item(int id, string name, string type, int price, DateTime manDate, TP properties)
        {
            ID = id;
            Name = name;
            Type = type;
            Price = price;
            ManufacturingDate = manDate;
            Properties = properties;
        }
        protected Item(int id, string name, string type, int price, DateTime manDate, string properties)
        {
            ID = id;
            Name = name;
            Type = type;
            Price = price;
            ManufacturingDate = manDate;
            Properties = JsonConvert.DeserializeObject<TP>(properties);
        }
        public string GetManufacturingYear()
        {
            return ManufacturingDate.Year.ToString();
        }
        public abstract string Info();
    }
}
