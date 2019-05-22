using System.Collections.ObjectModel;
using Computer_Era_X.Models;

namespace Computer_Era_X.DataTypes.Objects
{
    public class Player
    {
        public string Name { get; set; }
        public ObservableCollection<Currency> Money { get; } = new ObservableCollection<Currency>();
        public House House { get; set; }
        public Items Items { get; } = new Items();
    }
}
