using Computer_Era_X.DataTypes.Objects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Computer_Era_X.Models
{
    [NotMapped]
    public class HousingSale : House
    {
        public BaseCurrencies Currency { get; set; }
        public HousingSale(House house)
        {
            Id = house.Id;
            Name = house.Name;
            Area = house.Area;
            StorageSize = house.StorageSize;
            Rent = house.Rent;
            Price = house.Price;
            CommunalPayments = house.CommunalPayments;
            Location = house.Location;
            Distance = house.Distance;
            IsPurchase = house.IsPurchase;
            IsRent = house.IsRent;
            IsCreditPurchase = house.IsCreditPurchase;
            Image = house.Image;
        }
    }
}
