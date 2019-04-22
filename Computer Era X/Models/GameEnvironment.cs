using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Objects;

namespace Computer_Era_X.Models
{
    public class GameEnvironment
    {
        public GameEvents Events = new GameEvents();
        public Collection<BaseCurrency> Currencies = new Collection<BaseCurrency>();
        public Player Player = new Player();
    }
}
