using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace textparser
{
    class HtmlParser
    {
        public HtmlParser(Parameters parameters)
        {
            IDictionary dict = new Dictionary(parameters.DictFilePath);
            mSreader = new SentenceReader(parameters.TextFilePath);
            mMarker = new Marker(dict);
            mFmaker = new FileMaker(parameters.OutputDir, parameters.N);
        }

        public void Process()
        {
            foreach (string sentence in mSreader)
                mFmaker.append(mMarker.mark(sentence));
        }

        public void Wait()
        {

        }

        IReader mSreader;
        IMarker mMarker;
        IOutputMaker mFmaker;
    }
}
