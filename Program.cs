using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLua;


namespace liveStreamOne
{
    class Program
    {
        private Lua lua = new Lua();

        static void Main(string[] args)
        {
            Program pgrm = new Program();
            ExtraFunctions extra = new ExtraFunctions();
            pgrm.addFunction("printOnConsole", pgrm);
            pgrm.addFunction("add", extra);
            Console.WriteLine("-- PBCode Script --");
            while (true)
            {
                Console.Write(":>");
                string cmd = Console.ReadLine();
                if(!String.IsNullOrEmpty(cmd))
                {
                    pgrm.runScript(cmd);
                }
            }
        }

        public void runScript(string cmd)
        {
            try
            {
                lua.DoString(cmd);
            }
            catch(Exception i)
            {
                Console.WriteLine(i.Message);
            }
        }

        public void printOnConsole(string text)
        {
            Console.WriteLine(text);
        }

        public bool addFunction(string functionName, object classType)
        {
            try
            {
                Type type = classType.GetType();
                MethodInfo[] MI = type.GetMethods();
                IEnumerable<MethodInfo> linq = from a in MI
                                               where a.Name == functionName
                                               select a;
                if (linq.Count() > 0) {
                    MethodInfo info = linq.ToList()[0];
                    lua.RegisterFunction(functionName, classType, info);
                    return true;
                }

                return false;
            }
            catch(Exception i)
            {
                Console.WriteLine(i);
                return false;
            }

        }

    }
}
