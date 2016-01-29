using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace html
{
    class Parameters
    {
        public Parameters(string[] args, Action<Exception> errorDelegate)
        {
            try
            {
                TextFilePath = Path.GetFullPath(args[0]);
                DictFilePath = Path.GetFullPath(args[1]);
                OutputDir = args.Length > 2 ? args[2] : ".";
                N = args.Length > 3 ? Convert.ToInt32(args[3], 10) : 10;
            }
            catch (Exception e)
            {
                errorDelegate(e);
            }
        }

        #region GettersSetters

        public string TextFilePath
        { get; private set; }

        public string DictFilePath
        { get; private set; }

        public string OutputDir
        { get; private set; }

        public int N
        { get; private set; }

        #endregion
    }
}
