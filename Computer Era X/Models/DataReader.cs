using System;
using System.Runtime.InteropServices;

namespace Computer_Era_X.Models
{
    [ComVisible(true)]
    public class MapReader
    {
        private Action<string> _func;

        public MapReader(Action<string> func)
        {
            _func = func;
        }

        public void ReadState(string obj)
        {
            _func(obj);
        }
    }
}
