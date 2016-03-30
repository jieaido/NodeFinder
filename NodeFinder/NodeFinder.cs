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

        List<PathNode> CalcNodes = null;//这是寻找到的
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
                if (CalcNodes!=null&&CalcNodes.Count>0)
                {
                    CalcNodes.Clear();
                }
                FindGroundNodes(calcNode, out CalcNodes);
                for (int i = 0; i < CloseNodes.Count; i++)
                {
                    if (!OpenNodes.Exists(s => s.Position == CalcNodes[i].Position) &&
                       !CloseNodes.Exists(s => s.Position == CalcNodes[i].Position)) 
                    {
                        
                        //如果表不在open表中又不在close中,则加入open表中,这里就是计算这个postion在不在表中
                        OpenNodes.Add(CalcNodes[i]);
                        continue;


                    }
                    if (OpenNodes.Exists(s=>s.Position==CalcNodes[i].Position))
                    {
                        //如果已经在open表中,并且现在的代价值更低,就更新原来点的信息
                        var p= OpenNodes.Find(s => s.Position == CalcNodes[i].Position);
                        if (CalcNodes[i].TotalF<p.TotalF)
                        {
                            OpenNodes.Remove(p);
                            OpenNodes.Add(CalcNodes[i]);
                        }
                        continue;
                    }
                    if (CloseNodes.Exists(s=>s.Position==CalcNodes[i].Position))
                    {
                        //如果点在close表中,并且现在的代价比原来的低,就把原来的点从close表移除,加入open表
                        var p = OpenNodes.Find(s => s.Position == CalcNodes[i].Position);
                        if (CalcNodes[i].TotalF<p.TotalF)
                        {
                            CloseNodes.Remove(p);
                            OpenNodes.Add(CalcNodes[i]);
                        }
                        continue;
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
       //先找到周围的点
        pns=new List<PathNode>();
        for (int i = 0; i < pp.Length; i++)
        {
            if (pp[i] != null && pp[i].IsPass == true)
            {
                pp[i].ParentNode = pn;
                CalcTotalF(pp[i],_goalNode);
                pns.Add(pp[i]);
                //如果能找到点，并且该点不是障碍物，就把这个点加入临时需要计算的list里
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