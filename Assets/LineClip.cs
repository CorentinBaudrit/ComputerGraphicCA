using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineClip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
