using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using textparser;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            TextParser parser = new TextParser(new Parameters(args, 
                (Exception e) =>
                {
                    Console.WriteLine("Parameters parse failed.");
                    Console.WriteLine("Format: TextFilePath DictFilePath OutputDir N");

                    Environment.Exit(-1);
                }));

            parser.Process();
            parser.Wait();
        }
    }
}
