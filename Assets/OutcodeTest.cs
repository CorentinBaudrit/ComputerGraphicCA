using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutcodeTest : MonoBehaviour
{
    

    // Use this for initialization
    void Start()
    {
        Vector2 start = new Vector2(0.5f, 0.3f);
        Vector2 end = new Vector2(-0.5f, -0.3f);
        if (Line_Clip(ref start, ref end))
        {
            List<Vector2> vectorsRasterised = new List<Vector2>();
            vectorsRasterised = Rasterise(start, end);
            print("something");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Vector2> Rasterise(Vector2 start, Vector2 end)
    {
        print("drawing");
        List<Vector2> listOfVector = new List<Vector2>();

        float dy = end.y - start.y,
                dx = end.x - start.x,
                p = 2 * dy - dx,
                x = start.x,
                y = start.y;

        if (dx < 0)
        {
            return Rasterise(end, start);
        }

        if (dy < 0)
        {
            return NegateY(Rasterise(NegateY(start), NegateY(end)));
        }

        if (dy > dx)
        {
            return SwapXY(Rasterise(SwapXY(start), SwapXY(end)));
        }

        listOfVector.Add(new Vector2(x, y));
        while (x <= end.x && y <= end.y)
        {
            // X
            x++;

            // Y
            if (p > 0)
            {
                y++;
            }

            // P
            if (p < 0)
            {
                p += 2 * dy;
            }
            else
            {
                p += 2 * (dy - dx);
            }

            listOfVector.Add(new Vector2(x, y));
            //print("x: " + x + "; y: " + y);

        }

        return listOfVector;
    }

    public List<Vector2> SwapXY(List<Vector2> list)
    {
        List<Vector2> listToReturn = new List<Vector2>();
        foreach (Vector2 v in list)
            listToReturn.Add(SwapXY(v));

        return listToReturn;

    }

    public List<Vector2> NegateY(List<Vector2> list)
    {
        List<Vector2> listToReturn = new List<Vector2>();
        foreach (Vector2 v in list)
            listToReturn.Add(NegateY(v));

        return listToReturn;

    }

    public Vector2 NegateY(Vector2 vector)
    {
        Vector2 vectorToReturn = new Vector2(vector.x, -vector.y);
        return vectorToReturn;
    }

    public Vector2 SwapXY(Vector2 vector)
    {
        Vector2 vectorToReturn = new Vector2(vector.y, vector.x);
        return vectorToReturn;
    }

    public bool Line_Clip(ref Vector2 start, ref Vector2 end)
    {
        Outcode startOutcode = new Outcode(start);
        Outcode endOutcode = new Outcode(end);

        if (startOutcode == new Outcode() && endOutcode == new Outcode())
        {
            print("Trivially Accept");
            return true;

        }
        if ((startOutcode & endOutcode) != new Outcode())
        {
            print("Trivially Reject");
            return false;
        }

        start = Line_Intercept(start, end, startOutcode.getEdges());
        end = Line_Intercept(end, start, endOutcode.getEdges());

        return true;

    }


    public Vector2 Line_Intercept(Vector2 start, Vector2 end, Edge[] edgeToClip)
    {
        Vector2 vectorToReturn = start;
        for (int i = 0; i < 4; i++)
        {
            Edge edge = edgeToClip[i];
            float m = (end.y - vectorToReturn.y) / (end.x - vectorToReturn.x);
            switch (edge)
            {
                case Edge.UP:
                    vectorToReturn = new Vector2(vectorToReturn.x + (1 - vectorToReturn.y) / m, 1);
                    break;
                case Edge.DOWN:
                    vectorToReturn = new Vector2(-1, vectorToReturn.x + m * (-1 - vectorToReturn.y));
                    break;
                case Edge.LEFT:
                    vectorToReturn = new Vector2(-1, vectorToReturn.y + m * (-1 - vectorToReturn.x));
                    break;
                case Edge.RIGHT:
                    vectorToReturn = new Vector2(vectorToReturn.y + (1 - vectorToReturn.x) / m, 1);
                    break;
                default:
                    break;

            }
        }
        return vectorToReturn;
    }
}
