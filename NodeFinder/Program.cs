using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeFinder
{
    class Program
    {
        static void Main(string[] args)
        {
           PathFinder pf=new PathFinder();
            pf.InitPath(12,5,15);
            string msg;
            pf.SerachGoal(new Postion(0, 1), new Postion(11, 4), out msg);
            Console.WriteLine("11");
            Console.WriteLine(msg);
            Console.ReadKey();
        }
    }
}
