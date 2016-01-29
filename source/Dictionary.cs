using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace html
{
    interface IDictionary
    {
        bool find(string word, bool caseSensitive);
    };

    class Dictionary : IDictionary
    {
        public Dictionary(string dictFilePath)
        {
            if (!File.Exists(dictFilePath))
                throw new FileNotFoundException("", dictFilePath);

            StreamReader stream = new StreamReader(dictFilePath, Encoding.Unicode);
            string allText = stream.ReadToEnd();
            string[] words = allText.Split(new char[] { '\r', '\n' }, 
                StringSplitOptions.RemoveEmptyEntries);

            mSet = new HashSet<string>(words);

            stream.Close();
        }

        bool IDictionary.find(string word, bool caseSensitive)
        {
            return mSet.Contains(word, caseSensitive ? 
                StringComparer.CurrentCulture : 
                StringComparer.CurrentCultureIgnoreCase);
        }

        HashSet<string> mSet;
    }
}
