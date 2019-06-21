using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Computer_Era_X.DataTypes.Objects
{
    public class GameValue
    {
        public int ID { get; set; }
        public string Values { get; set; }
    }

    public class GameValues
    {

        public Collection<Company> Companies = new Collection<Company>();

        public void LoadingValues(Collection<GameValue> gameValueList)
        {
            foreach (GameValue value in gameValueList)
            {
                switch(value.ID)
                {
                    case 1:
                        Companies = JsonConvert.DeserializeObject<Collection<Company>>(value.Values);
                        break;
                }
            }
        }
    }
}
