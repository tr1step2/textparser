using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textparser
{
    interface IMarker
    {
        string mark(string str);
    }

    class Marker : IMarker
    {
        public Marker(IDictionary dict)
        {
            mDict = dict;
        }

        string IMarker.mark(string str)
        {
            return str;
        }

        private IDictionary mDict;
    }
}
