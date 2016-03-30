using System;
using System.Runtime.CompilerServices;


public class PathNode : IComparable<PathNode>
{

    public PathNode ParentNode { get; set; }
    public int EndH { get; set; }
    public int StartG { get; set; }

    public int TotalF
    {
        get { return EndH + StartG; }
    }

    public Postion Position { get; set; }
    public bool IsPass=true;//是否障碍物，能否穿过,要设置个默认值为true
    public PathNode(int h,int g)
    {
        EndH = h;
        StartG = g;
        IsPass = true;
    }

    public PathNode(Postion postion)
    {
        Position = postion;
    }


    public int CompareTo(PathNode other)
    {
        if (other == null) return 1;

        PathNode otherPathNode = other as PathNode;
        if (otherPathNode != null)
            return this.TotalF.CompareTo(otherPathNode.TotalF);
        else
            throw new ArgumentException("Object is not a Temperature");

    }

    public override string ToString()
    {
        return "PathNode--F:" + this.TotalF+"--G:"+this.StartG+"--H:"+this.EndH;
    }

    public static int CalcEndH(Postion startPostion, Postion goalpPostion)
    {
        return Math.Abs(goalpPostion.Y - startPostion.Y) + Math.Abs(goalpPostion.X - startPostion.X);
    }
}

public class Postion
{
    public int X;
    public int Y;

    public Postion(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static bool operator ==(Postion p1, Postion p2)
    {
        if (p1.X==p2.X&&p1.Y==p2.Y)
        {
            return true;
        }
        return false;
    }

    public static bool operator !=(Postion p1, Postion p2)
    {
        return !(p1 == p2);
    }

    public override string ToString()
    {
        return "Pos:"+ X.ToString() +","+ Y.ToString();
    }
}

