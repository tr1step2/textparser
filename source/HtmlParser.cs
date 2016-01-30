using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace textparser
{
    class TextParser
    {
        public TextParser(Parameters parameters)
        {
            mSreader = new SentenceReader(parameters.TextFilePath);
            mMarker = new Marker(new Dictionary(parameters.DictFilePath));
            mFmaker = new FileMaker(parameters.OutputDir, parameters.N);
        }

        public void Process()
        {
            foreach (string sentence in mSreader)
                mFmaker.append(mMarker.mark(sentence));
            mFmaker.close();
        }

        public void Wait()
        {

        }

        private IReader mSreader;
        private IMarker mMarker;
        private IOutputMaker mFmaker;
    }
}
