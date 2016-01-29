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

            mTask = new Task(() =>
            {
                foreach (string sentence in mSreader)
                    mFmaker.append(mMarker.mark(sentence));
                mFmaker.close();
            });
        }

        public void Process()
        {
            mTask.Start(TaskScheduler.Current);
        }

        public void Wait()
        {
            mTask.Wait();   
        }

        private Task mTask;
        private IReader mSreader;
        private IMarker mMarker;
        private IOutputMaker mFmaker;
    }
}
