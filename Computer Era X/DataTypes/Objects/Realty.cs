using System;

namespace Computer_Era_X.DataTypes.Objects
{
    public class House
    {
        public int UId { get; set; }
        public string Name { get; set; }
        public int Area { get; set; }
        public int StorageSize { get; set; }
        public double Rent { get; set; }
        public double Price { get; set; }
        public double CommunalPayments { get; set; }
        public string Location { get; set; }
        public int Distance { get; set; } //In meters
        public bool IsPurchase { get; set; }
        public bool IsRent { get; set; }
        public bool IsCreditPurchase { get; set; }
        public string Image { get; set; }

        public House(int id, string name, int area, int storage_size, double rent, double price, double communal_payments, string location, int distance, bool is_purchase, bool is_rent, bool is_credit_purchase, string image_name)
        {
            UId = id;
            Name = name;
            Area = area;
            StorageSize = storage_size;
            Rent = rent;
            Price = price;
            CommunalPayments = communal_payments;
            Location = location;
            Distance = distance;
            IsPurchase = is_purchase;
            IsRent = is_rent;
            IsCreditPurchase = is_credit_purchase;
            Image = image_name;
        }

        public override string ToString()
        {
            string str = Properties.Resources.Area + " " + Area + " м²" + Environment.NewLine +
                          Properties.Resources.PantrySize + ": " + StorageSize + " " + Properties.Resources.Cells.ToLower() + Environment.NewLine +
                          Properties.Resources.Location + ": " + Location + Environment.NewLine +
                          Properties.Resources.DistanceToCityCenter + ": " + Distance + "м";
            return str;
        }
    }

    //public class PlayerHouse : House
    //{
        //public bool IsRentedOut { get; set; }
        //public bool IsPurchased { get; set; }
        //public bool IsPurchasedOnCredit { get; set; }
        //public PlayerTariff PlayerRent { get; set; }
        //public PlayerTariff PlayerCredit { get; set; }
        //public PlayerTariff PlayerCommunalPayments { get; set; }
        //public PlayerHouse(int id, string name, int area, int storage_size, double rent, double price, double communal_payments, string location, int distance, bool is_purchase, bool is_rent, bool is_credit_purchase, string image_name, PlayerTariff player_communal_payments, bool isRentedOut = false, bool isPurchased = false, bool isPurchasedOnCredit = false, PlayerTariff tariff = null)
                         // : base(id, name, area, storage_size, rent, price, communal_payments, location, distance, is_purchase, is_rent, is_credit_purchase, image_name)
        //{
            //PlayerCommunalPayments = player_communal_payments;
            //IsRentedOut = isRentedOut;
            //IsPurchase = isPurchased;
           // IsPurchasedOnCredit = isPurchasedOnCredit;
            //if (IsRentedOut) { PlayerRent = tariff; }
            //else if (IsCreditPurchase) { PlayerCredit = tariff; }
        //}
    //}
}
