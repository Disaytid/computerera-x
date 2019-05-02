using System.Collections.ObjectModel;

namespace Computer_Era_X.DataTypes.Objects
{
    public class Player
    {
        public string Name { get; set; }
        public Collection<Currency> Money { get; set; } = new Collection<Currency>();
        public House House;
    }
}
