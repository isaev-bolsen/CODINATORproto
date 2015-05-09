using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace CODINATOR
    {
    class Program
        {
        static void Main(string[] args)
            {
            ClassBuilder CB = new ClassBuilder("Sample", "Sample1");
            CB.addField<Int32>("Count").InitExpression = new CodePrimitiveExpression(3);

            CodePrimitiveExpression[] dateParameters=new CodePrimitiveExpression[3];
            dateParameters[0] = new CodePrimitiveExpression(DateTime.Now.Year);
            dateParameters[1] = new CodePrimitiveExpression(DateTime.Now.Month);
            dateParameters[2] = new CodePrimitiveExpression(DateTime.Now.Day);

            CB.addField<DateTime>("Date").InitExpression = new CodeObjectCreateExpression(typeof(DateTime), dateParameters);
            CB.CreateValuesEqualsMethod();
            CB.SerializeCs();
            CB.SerializeVb();
            }
        }
    }
