using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Objects;

namespace Computer_Era_X.Models
{
    public class Player
    {
        public string Name { get; set; }
        public Collection<Currency> Money { get; set; } = new Collection<Currency>();
        public House House;
    }
}
