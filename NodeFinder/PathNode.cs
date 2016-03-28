using System;


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
    public PathNode(int h,int g)
    {
        EndH = h;
        StartG = g;
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
        return "PathNode_F:" + this.TotalF;
    }
    
}

public struct Postion
{
    int X;
    int Y;

    public Postion(int x, int y)
    {
        X = x;
        Y = y;
    }
}

