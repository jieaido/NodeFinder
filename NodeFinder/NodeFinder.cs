using System;
using System.Collections.Generic;
using System.Linq;

public class PathFinder
{
    public List<PathNode> CloseNodes = new List<PathNode>();
    public List<PathNode> Nodes = new List<PathNode>();

    public List<PathNode> OpenNodes = new List<PathNode>();

    public void InitPath(int xNum, int yNum, int rockNum)
    {
        for (var i = 0; i < xNum; i++)
        {
            for (var j = 0; j < yNum; j++)
            {
                var p = new PathNode(new Postion(i, j));
                Nodes.Add(p);
            }
        }
        if (rockNum > xNum*yNum)
        {
            rockNum = xNum*yNum/10;
        }
        var rd = new Random();
        for (var r = 0; r < rockNum; r++)
        {
            Nodes[rd.Next(Nodes.Count)].IsPass = false;
        }
    }

    public bool SerachGoal(Postion startPostion, Postion goalpostion)
    {
        
        List<PathNode> CalcNodes=new List<PathNode>();
       //todo   增加positon类的==操作符重写，判断startpostion==goalposition的情况 
        var startNode = Nodes.Find(p => p.Position == startPostion );
        var goalNode = Nodes.Find(p => p.Position == goalpostion);
        if (startNode==null||goalNode==null)
        {
            return false;
        }
        startNode.StartG = 0;
        startNode.EndH = PathNode.CalcEndH(startPostion, goalpostion);
        startNode.ParentNode = null;
        OpenNodes.Add(startNode);
        while (OpenNodes.Count>0)
        {
           var Calc= OpenNodes.Min(s => s.TotalF);
            FindGroundNodes();
        }



        return false;
    }

    private void FindGroundNodes(PathNode pn )
    {

        var p1=  Nodes.Find(p=>p.Position.X==pn.Position.X&&p.Position.Y==p.Position.Y+1);
        var p2=  Nodes.Find(p=>p.Position.X==pn.Position.X&&p.Position.Y==p.Position.Y-1);
        var p3=  Nodes.Find(p=>p.Position.X==pn.Position.X+1&&p.Position.Y==p.Position.Y);
        var p4=  Nodes.Find(p=>p.Position.X==pn.Position.X-1&&p.Position.Y==p.Position.Y);
    }
}