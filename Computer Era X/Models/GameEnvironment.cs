using System;
using System.Collections.ObjectModel;
using Computer_Era_X.DataTypes.Interfaces;
using Computer_Era_X.DataTypes.Objects;
using Computer_Era_X.Models.Systems;

namespace Computer_Era_X.Models
{
    public class GameEnvironment
    {
        public GameEvents Events { get; } = new GameEvents();
        public Random Random { get; } = new Random(DateTime.Now.Millisecond);
        public ObservableCollection<Message> Messages { get; } = new ObservableCollection<Message>();
        public IScenario Scenario { get; set; }
        public Items Items { get; } = new Items();
        public ObservableCollection<BaseCurrency> Currencies { get; set; } = new ObservableCollection<BaseCurrency>();
        public Player Player { get; } = new Player();
        public ObservableCollection<Service> Services { get; set; } = new ObservableCollection<Service>();
        public GameValues GameValues = new GameValues();
        public Collection<Profession> Professions = new Collection<Profession>();
        public Collection<House> Houses = new Collection<House>();
    }
}
