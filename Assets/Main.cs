using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Vector3[] cube = new Vector3[8];
    List<Vector2> listVectorToErase = new List<Vector2>();
    Rasterisation rast = new Rasterisation();
    LineClip lineClip = new LineClip();
    Matrices transformMatrix = new Matrices();
    Texture2D myTexture;
    Vector3 rotAx = new Vector3(0, 0, 0);
    Renderer myRenderer;
    int resX = 1920;
    int resY = 1080;
    Color32 resetColor = new Color32(255, 255, 255, 0);
    Color32[] resetColorArray = new Color32[64];




    // Use this for initialization
    void Start () {

        myTexture = new Texture2D(resX, resY);
        myRenderer = GetComponent<Renderer>();
        myRenderer.material.mainTexture = myTexture;
        
        
 
        cube[0] = new Vector3(1, 1, 1);
        cube[1] = new Vector3(1, 1, -1);
        cube[2] = new Vector3(1, -1, 1);
        cube[3] = new Vector3(1, -1, -1);
        cube[4] = new Vector3(-1, 1, 1);
        cube[5] = new Vector3(-1, 1, -1);
        cube[6] = new Vector3(-1,-1, 1);
        cube[7] = new Vector3(-1, -1,- 1);

        cube = transformMatrix.TransformMatrixViewing(cube);
        cube = transformMatrix.TransformMatrixProjection(cube);
        drawCube(cube);
        myTexture.Apply();
    }
    void drawCube(Vector3[] imageOfCube)
    {
        //Near
        LineDraw(imageOfCube[4], imageOfCube[0]);
        LineDraw(imageOfCube[0], imageOfCube[2]);
        LineDraw(imageOfCube[2], imageOfCube[6]);
        LineDraw(imageOfCube[6], imageOfCube[4]);

        //Far
        LineDraw(imageOfCube[5], imageOfCube[1]);
        LineDraw(imageOfCube[1], imageOfCube[3]);
        LineDraw(imageOfCube[3], imageOfCube[7]);
        LineDraw(imageOfCube[7], imageOfCube[5]);

        //Up
        LineDraw(imageOfCube[4], imageOfCube[5]);
        LineDraw(imageOfCube[0], imageOfCube[1]);

        //Down
        LineDraw(imageOfCube[7], imageOfCube[6]);
        LineDraw(imageOfCube[3], imageOfCube[2]);

    }

    private void LineDraw(Vector3 start, Vector3 end)
    {
        Vector2 start2 = new Vector2(start.x / start.z, start.y / start.z);
        Vector2 end2 = new Vector2(end.x / end.z, end.y / end.z);



        if (lineClip.Line_Clip(ref start2, ref end2))
        {
            start2.x = (int)(((start2.x + 1) / 2) * (resX - 1));
            start2.y = (int)(((start2.y + 1) / 2) * (resY - 1));
            end2.x = (int)(((end2.x + 1) / 2) * (resX - 1));
            end2.y = (int)(((end2.y + 1) / 2) * (resY - 1));

            List<Vector2> vectorsRasterised = new List<Vector2>();
            
            vectorsRasterised = rast.Rasterise(start2, end2);
            
            foreach (Vector2 v in vectorsRasterised)
            {
                //print(start2.x.ToString());
                //print(start2.y.ToString());
                myTexture.SetPixel((int)v.x, (int)v.y, Color.red);
                listVectorToErase.Add(v);
            }
            
        }
    }



	
	// Update is called once per frame
	void Update () {
        foreach (Vector2 v in listVectorToErase)
        {
            myTexture.SetPixel((int)v.x, (int)v.y, Color.white);
        }
        listVectorToErase.Clear();
        cube = transformMatrix.TransformMatrixRotating(cube, 1.0f);
        //cube = transformMatrix.TransformMatrixViewing(cube);
        //cube = transformMatrix.TransformMatrixProjection(cube);
        drawCube(cube);
        myTexture.Apply();

    }
}

