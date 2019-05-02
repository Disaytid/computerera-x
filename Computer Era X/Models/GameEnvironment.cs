using System;
using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Interfaces;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models.Systems;

namespace Computer_Era_X.Models
{
    public class GameEnvironment
    {
        public GameEvents Events = new GameEvents();
        public Random Random = new Random(DateTime.Now.Millisecond);
        public ObservableCollection<Message> Messages = new ObservableCollection<Message>();
        public IScenario Scenario;
        public Collection<BaseCurrency> Currencies = new Collection<BaseCurrency>();
        public Player Player = new Player();
    }
}
