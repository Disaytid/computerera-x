using System.Collections.ObjectModel;

namespace Computer_Era_X.DataTypes.Objects
{
    public class GameValue
    {
        public int ID { get; set; }
        public string Values { get; set; }
    }

    public class GameValues
    {
        public GameValue[] gameValues;

        public ObservableCollection<Company> Companies = new ObservableCollection<Company>();
    }
}
