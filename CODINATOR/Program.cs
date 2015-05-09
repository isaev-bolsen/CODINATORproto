using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODINATOR
    {
    class Program
        {
        static void Main(string[] args)
            {
            ClassBuilder CB = new ClassBuilder("Sample", "Sample1");
            CB.addField(typeof(Int32), "Count");
            CB.addField(typeof(DateTime), "Date");
            CB.SerializeCs();
            }
        }
    }
