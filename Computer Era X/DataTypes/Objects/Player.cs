using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Objects.Computer;
using Computer_Era_X.Models;

namespace Computer_Era_X.DataTypes.Objects
{
    public class Player
    {
        public string Name { get; set; }
        public ObservableCollectionExtended<BaseCurrencies> Money { get; set; } = new ObservableCollectionExtended<BaseCurrencies>();
        public PlayerHouse House { get; set; }
        public Items Items { get; } = new Items();
        public ObservableCollection<PlayerTariff> Tariffs { get; set; } = new ObservableCollection<PlayerTariff>();
        public JobCard Job;
    }
}
