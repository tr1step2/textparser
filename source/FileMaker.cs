using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            mMaxLines = N;

            if (!Directory.Exists(mOutputDir))
                throw new FileNotFoundException("", mOutputDir);

            initNewFile();
            
        }

        void IOutputMaker.append(string str)
        {
            mCurrentLinesInFile += str.Count(s => s == '\n');
            mCurrentStream.Write(str);

            if (mCurrentLinesInFile >= mMaxLines)
                initNewFile();
        }

        private void initNewFile()
        {
            if (mCurrentStream != null)
                mCurrentStream.Close();

            string path = Path.Combine(mOutputDir, mDefaultFilename + ++mCurrentFileNum + mDefaultExtension);
            mCurrentStream = new StreamWriter(path, true, Encoding.UTF8);

            mCurrentLinesInFile = 0;
        }

        #region consts
        private const string mDefaultFilename = "Result";
        private const string mDefaultExtension = ".html";
        #endregion

        private string mOutputDir;
        private int mMaxLines;

        private int mCurrentFileNum = 0;
        private int mCurrentLinesInFile;
        private StreamWriter mCurrentStream;
    }
}
