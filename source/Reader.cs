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

            mStream = new StreamReader(textFilePath, Encoding.Unicode);
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
            loadData();

            mCurrentIndex = -1;
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
            if (mCurrentIndex < mSentenceBuffer.Length)
                return true;

            return loadData();
        }

        private string getSentence()
        {
            if (mSentenceBuffer.Length == 0)
                return "";

            return mSentenceBuffer[mCurrentIndex];
        }

        private bool loadData()
        {
            char[] separators = { '.', '!', '?' };

            if (mStream.EndOfStream)
                return false;

            string line = mStream.ReadLine();

            mCurrentIndex = 0;
            mSentenceBuffer = Regex.Split(line, @"(?<=[.!\?])", RegexOptions.Compiled);
            mSentenceBuffer = Array.FindAll(mSentenceBuffer, s => s != "");
            return true;
        }

        private StreamReader mStream;
        private string[] mSentenceBuffer = {};
        private int mCurrentIndex;
    }
}
