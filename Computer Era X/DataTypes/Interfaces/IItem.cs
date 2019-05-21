using System;

namespace Computer_Era_X.DataTypes.Interfaces
{
    public interface IItem
    {
        int ID { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        double Price { get; set; }
        DateTime ManufacturingDate { get; set; }

        string ToString();
    }
}
