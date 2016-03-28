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
            Random rd=new Random();
            List<PathNode> openNodes=new List<PathNode>();
            for (int i = 0; i < 10; i++)
            {
                PathNode pn=new PathNode(rd.Next(10),rd.Next(10));
                openNodes.Add(pn);
            }
            //openNodes.Sort();
            var ss = openNodes.OrderBy(s => s.TotalF);
            foreach (var openNode in ss)
            {
                Console.WriteLine(openNode);
            }
            Console.ReadKey();
        }
    }
}
