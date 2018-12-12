using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutcodeTest : MonoBehaviour
{
    

    // Use this for initialization
    void Start()
    {
        LineClip LC = new LineClip();
        Rasterisation rasterisation = new Rasterisation();
        Vector2 start = new Vector2(0.5f, 0.3f);
        Vector2 end = new Vector2(-0.5f, -0.3f);
        if (LC.Line_Clip(ref start, ref end))
        {
            List<Vector2> vectorsRasterised = new List<Vector2>();
            vectorsRasterised = rasterisation.Rasterise(start, end);
            print("Rasterise");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    

  
}
