using Brack.Data;
using Brack.Data.Operations;
using Brack.Interpretation;
using System;
using System.IO;

namespace Brack_CS_Test
{
    class Program
    {
        static void Main()
        {
            var curRam = new RAM(new OperationSet(new BrackOperatorBase[]{
                    new BrackParamOperator<float>("add",(r, args) =>
                    {
                        var ret = 0f;
                        foreach(var f in args)
                        {
                            ret+=f;
                        }
                        return ret;
                    }),
                    new BrackOperator<string>("print",(r, str) =>
                    {
                        Console.WriteLine(str);
                        return str;
                    }),
                }));

            object[][] brack = BrackParser.ParseString(File.ReadAllText("TestScript.brak"));

            BrackInterpreter.Execute(curRam, brack);

            Console.ReadKey();
        }
    }
}
