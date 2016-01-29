using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace html
{
    interface IOutputMaker
    {
        void append(string str);
    }

    class FileMaker : IOutputMaker
    {
        public FileMaker(string outputDir, int N)
        {
            mOutputDir = outputDir;
            mN = N;
        }

        void IOutputMaker.append(string str)
        {
        }

        string mOutputDir;
        int mN;
    }
}
