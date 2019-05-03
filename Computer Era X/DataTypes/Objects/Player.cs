using System.Collections.ObjectModel;

namespace Computer_Era_X.DataTypes.Objects
{
    public class Player
    {
        public string Name { get; set; }
        public ObservableCollection<Currency> Money { get; set; } = new ObservableCollection<Currency>();
        public House House;
    }
}
