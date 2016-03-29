using System;
using System.Collections.Generic;
using System.Linq;

public class PathFinder
{
    public List<PathNode> CloseNodes = new List<PathNode>();
    public List<PathNode> Nodes = new List<PathNode>();

    public List<PathNode> OpenNodes = new List<PathNode>();
    private PathNode _startNode;
    private PathNode _goalNode;


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

        List<PathNode> CalcNodes;//这是寻找到的
       //todo   增加positon类的==操作符重写，判断startpostion==goalposition的情况 
        _startNode = Nodes.Find(p => p.Position == startPostion );
        _goalNode = Nodes.Find(p => p.Position == goalpostion);
        if (_startNode==null||_goalNode==null)
        {
            return false;
        }
        _startNode.StartG = 0;
        _startNode.EndH = PathNode.CalcEndH(startPostion, goalpostion);
        _startNode.ParentNode = null;
        OpenNodes.Add(_startNode);
        PathNode calcNode;//这是需要计算的点,就是计算这个点的周围
        while (OpenNodes.Count>0)
        {
            calcNode = OpenNodes.OrderBy(s=>s.TotalF).First();
            if (calcNode!=_goalNode)
            {
                FindGroundNodes(calcNode, out CalcNodes);
                for (int i = 0; i < CloseNodes.Count; i++)
                {
                    if (OpenNodes.Contains(CalcNodes[i]))
                    {

                    }
                }
               
            }
           
        }


        return false;
    }

    private void FindGroundNodes(PathNode pn,out List<PathNode> pns )
    {

        var p1=  Nodes.Find(p=>p.Position.X==pn.Position.X&&p.Position.Y==p.Position.Y+1);
        var p2=  Nodes.Find(p=>p.Position.X==pn.Position.X&&p.Position.Y==p.Position.Y-1);
        var p3=  Nodes.Find(p=>p.Position.X==pn.Position.X+1&&p.Position.Y==p.Position.Y);
        var p4=  Nodes.Find(p=>p.Position.X==pn.Position.X-1&&p.Position.Y==p.Position.Y);
        PathNode[] pp=new PathNode[]
        {
            p1,p2,p3,p4
        };
       
        pns=new List<PathNode>();
        for (int i = 0; i < pp.Length; i++)
        {
            if (pp[i] != null && pp[i].IsPass == true)
            {
                pp[i].ParentNode = pn;
                CalcTotalF(pp[i],_goalNode);
                pns.Add(pp[i]);
            }
        }
       
       
       

    }

    private void CalcTotalF(PathNode thisNode, PathNode goalNode)
    {
        if (thisNode.ParentNode!=null)
        {
            thisNode.StartG = thisNode.ParentNode.StartG + PathNode.CalcEndH(thisNode.Position, thisNode.ParentNode.Position);
        }
        thisNode.EndH = PathNode.CalcEndH(thisNode.Position, goalNode.Position);

    }
}