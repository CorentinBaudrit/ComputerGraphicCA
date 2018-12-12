using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rasterisation : MonoBehaviour
{

    

	// Use this for initialization
	void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Vector2> Rasterise(Vector2 start, Vector2 end)
    {
        print("Drawing");
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
}
