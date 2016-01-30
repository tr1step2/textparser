using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace textparser
{
    interface IReader : IEnumerable
    {
    }

    class SentenceReader : IReader
    {
        public SentenceReader(string textFilePath)
        {
            if (!File.Exists(textFilePath))
                throw new FileNotFoundException("", textFilePath);

            mStream = new StreamReader(textFilePath, Encoding.UTF8);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SentenceEnumerator(mStream);
        }

        private StreamReader mStream;
    }

    class SentenceEnumerator : IEnumerator
    {
        public SentenceEnumerator(StreamReader stream)
        {
            mStream = stream;
        }

        #region IEnumerable implementation
        object IEnumerator.Current
        {
            get
            {
                return getSentence();
            }
        }

        bool IEnumerator.MoveNext()
        {
            return increasePointer();
        }

        void IEnumerator.Reset()
        {
        }
        #endregion

        private bool increasePointer()
        {
            mCurrentIndex += 1;
            if (mCurrentIndex >= mSentenceBuffer.Length)
                return loadData();

            return true;
        }

        private string getSentence()
        {
            if (!Regex.IsMatch(mSentenceBuffer[mCurrentIndex], @"[.!?]"))
                loadData();

            return mSentenceBuffer[mCurrentIndex];
        }
        
        private bool loadData()
        {
            if (mStream.EndOfStream)
                return false;

            string line = "";
            if (mCurrentIndex < mSentenceBuffer.Length)
                line = mSentenceBuffer[mCurrentIndex] + mStream.ReadLine();

            while(!mStream.EndOfStream &&
                !Regex.IsMatch(line, @"[.?!]")) 
            {
                line += "\r\n";
                line += mStream.ReadLine();
            }

            mCurrentIndex = 0;
            string expr = @"(?<=[.!?])";

            mSentenceBuffer = Regex.Split(line, expr, RegexOptions.Compiled);
            mSentenceBuffer = Array.FindAll(mSentenceBuffer, s => s != "");

            return true;
        }

        private StreamReader mStream;
        private string[] mSentenceBuffer = { "" };
        private int mCurrentIndex = -1;
    }
}
