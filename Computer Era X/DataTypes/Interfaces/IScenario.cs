using Computer_Era_X.Models;

namespace Computer_Era_X.DataTypes.Interfaces
{
    public interface IScenario
    {
        string Name { get; set; }
        void Start(object sender, GameEnvironment gameEnvironment);
        void GameOver(string cause);
    }
}
