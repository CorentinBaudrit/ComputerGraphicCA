using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outcode 
{

    public bool up,
                down,
                left,
                right;
    public Vector2 vector;

    public Outcode(Vector2 v)
    {
        up = v.y > 1;
        down = v.y < -1;
        left = v.x < -1;
        right = v.x > 1;

        vector = v;
    }

    public Outcode()
    {
        up = false;
        down = false;
        left = false;
        right = false;
    }

    public Outcode(bool up, bool down, bool left, bool right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public static bool operator ==(Outcode out1, Outcode out2)
    {
        return (out1.down == out2.down
                && out1.left == out2.left
                && out1.up == out2.up
                && out1.right == out2.right);
    }

    public static bool operator !=(Outcode out1, Outcode out2)
    {
        return (out1.down != out2.down
                || out1.left != out2.left
                || out1.up != out2.up
                || out1.right != out2.right);
    }

    public static Outcode operator &(Outcode out1, Outcode out2)
    {
        return (
            new Outcode(out1.up && out2.up, out1.down && out2.down, out1.left && out2.left, out1.right && out2.right)
        );
    }

    public Edge[] getEdges()
    {
        int i = 0;
        Edge[] edges = new Edge[4];
        if (up)
        {
            edges[i] = Edge.UP;
            i++;
        }
        if (down)
        {
            edges[i] = Edge.DOWN;
            i++;
        }
        if (left)
        {
            edges[i] = Edge.LEFT;
            i++;
        }
        if (right)
        {
            edges[i] = Edge.RIGHT;
            i++;
        }

        return edges;
    }
}

public enum Edge
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}
