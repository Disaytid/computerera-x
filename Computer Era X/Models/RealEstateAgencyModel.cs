using Computer_Era_X.DataTypes.Objects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Computer_Era_X.Models
{
    [NotMapped]
    public class HousingSale : House
    {
        public BaseCurrencies Currency { get; set; }
        public double ConvertedValue { get; set; }
        public double ConvertedRentalValue { get; set; }
        public HousingSale(House house, BaseCurrencies currencies, double convertedValue, double convertedRentalValue)
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
            Currency = currencies;
            ConvertedValue = convertedValue;
            ConvertedRentalValue = convertedRentalValue;
        }
    }
}
