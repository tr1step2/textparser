using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace html
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
            string expr = @"(\w+)([\s,\.\?\!:;])";
            string[] seq = Array.FindAll(Regex.Split(str, expr, RegexOptions.Compiled), 
                s => s != "");

            StringBuilder sb = new StringBuilder();
            foreach(var s in seq)
            {
                sb.Append(mDict.find(s, false) ? "<b><i>" + s + "</b></i>" : s);
            }
            sb.Append("<br>");

            return sb.ToString();
        }

        private IDictionary mDict;
    }
}
