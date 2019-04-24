using System.Runtime.InteropServices;
using Computer_Era_X.Views;

namespace Computer_Era_X.Models
{
    [ComVisible(true)]
    public class MapReader
    {
        object map;

        public MapReader(object sender)
        {
            map = sender;
        }

        public void ReadState(string obj)
        {
            if (map is Map map1)
            {
                map1.TransitionProcessing(obj);
            }
        }
    }
}
