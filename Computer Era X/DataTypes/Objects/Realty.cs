using Computer_Era_X.Properties;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Computer_Era_X.DataTypes.Objects
{
    public class House
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Area { get; set; }
        public int StorageSize { get; set; }
        public double Rent { get; set; }
        public double Price { get; set; }
        public double CommunalPayments { get; set; }
        public string Location { get; set; }
        public int Distance { get; set; } //In meters
        public int IsPurchase { get; set; }
        public int IsRent { get; set; }
        public int IsCreditPurchase { get; set; }
        public string Image { get; set; }

        public override string ToString()
        {
            string str = Resources.Area + " " + Area + " " + Resources.CutMeters + "²" + Environment.NewLine +
                         Resources.PantrySize + ": " + StorageSize + " " + Resources.Cells.ToLower() + Environment.NewLine +
                         Resources.Location + ": " + Location + Environment.NewLine +
                         Resources.DistanceToCityCenter + ": " + Distance + Resources.CutMeters;
            return str;
        }
    }

    [NotMapped]
    public class PlayerHouse : House
    {
        public bool IsRentedOut { get; set; }
        public bool IsPurchased { get; set; }
        public bool IsPurchasedOnCredit { get; set; }
        public PlayerTariff PlayerRent { get; set; }
        public PlayerTariff PlayerCredit { get; set; }
        public PlayerTariff PlayerCommunalPayments { get; set; }
        public PlayerHouse(House house, PlayerTariff player_communal_payments, bool isRentedOut = false, bool isPurchased = false, bool isPurchasedOnCredit = false, PlayerTariff tariff = null)
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
            PlayerCommunalPayments = player_communal_payments;
            IsRentedOut = isRentedOut;
            IsPurchased = isPurchased;
            IsPurchasedOnCredit = isPurchasedOnCredit;
            if (IsRentedOut) { PlayerRent = tariff; }
            else if (IsCreditPurchase == 1) { PlayerCredit = tariff; }
        }
    }
}
