using System;

namespace Computer_Era_X.DataTypes.Objects
{
    public class Company
    {
        public string Name { get; }
        public DateTime OpeningYear { get; }

        public Company(string name, DateTime opening_year)
        {
            Name = name;
            OpeningYear = opening_year;
        }
    }
}
