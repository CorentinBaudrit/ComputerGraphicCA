using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrices : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Vector3[] cube = new Vector3[8];
        cube[0] = new Vector3(1, 1, 1);
        cube[1] = new Vector3(-1, 1, 1);
        cube[2] = new Vector3(-1, -1, 1);
        cube[3] = new Vector3(1, -1, 1);
        cube[4] = new Vector3(1, 1, -1);
        cube[5] = new Vector3(-1, 1, -1);
        cube[6] = new Vector3(-1, -1, -1);
        cube[7] = new Vector3(1, -1, -1);

        //Rotation
        Vector3 startingAxis = new Vector3(15, -1, -1);
        startingAxis.Normalize();
        Quaternion rotation = Quaternion.AngleAxis(8, startingAxis);
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(Vector3.zero,
                            rotation,
                            Vector3.one);
        PrintMatrix(rotationMatrix);

        Vector3[] newImage =
            MatrixTransform(cube, rotationMatrix);
        PrintVerts(newImage);

        //Scaling
        Vector3 scaleV = new Vector3(15, 1, 1);
        Matrix4x4 scaleM = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scaleV);
        PrintMatrix(scaleM);

        Vector3[] newImage2 =
            MatrixTransform(newImage, scaleM);
        PrintVerts(newImage2);

        //Translation
        Vector3 TranslationV = new Vector3(3, 0, 0);
        Matrix4x4 TranslationM = Matrix4x4.TRS(TranslationV, Quaternion.identity, Vector3.one);
        print(TranslationM);

        Vector3[] newImage3 =
            MatrixTransform(newImage2, TranslationM);
        PrintVerts(newImage3);

        //Single Matrix

        Matrix4x4 allInM = TranslationM * scaleM * rotationMatrix;
        print(allInM);

        Vector3[] newImage4 =
            MatrixTransform(cube, allInM);
        PrintVerts(newImage4);

        //Vewing


        Vector3 camPos = new Vector3(17, 2, 49);
        Vector3 lookAt = new Vector3(-1, 15, 1);
        Vector3 camUp = new Vector3(0, -1, 15);
        Vector3 forward = lookAt - camPos;
        forward.Normalize();
        camUp.Normalize();

        Quaternion Viewing = Quaternion.LookRotation(forward, camUp);
        Matrix4x4 viewingM = Matrix4x4.TRS(-camPos, Viewing, Vector3.one);

        Vector3[] newImage5 =
            MatrixTransform(newImage4, viewingM);
        PrintVerts(newImage5);

        Matrix4x4 projectionM = Matrix4x4.Perspective(90, 1, 1, 1000);

        Vector3[] newImage6 =
            MatrixTransform(newImage5, projectionM);
        PrintVerts(newImage6);


        //Allin

        Matrix4x4 everythingM = projectionM* viewingM * TranslationM * scaleM * rotationMatrix;
        print(allInM);

        Vector3[] finalImage =
            MatrixTransform(cube, everythingM);
        PrintVerts(finalImage);

    }

    public Vector3[] TransformMatrixRotating(Vector3[] cube, float angle)
    {
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(angle, Vector3.one.normalized), Vector3.one);
        cube = MatrixTransform(cube, rotationMatrix);
        return cube;
    }

    public Vector3[] TransformMatrixViewing(Vector3[] cube)
    {
        //Viewing
        Vector3 cameraPosition = new Vector3(0, 0, 10);
        Vector3 cameraLookAt = new Vector3(0, 0, 0);
        Vector3 cameraUp = new Vector3(0, 1, 0);

        Vector3 forward = cameraLookAt - cameraPosition;
        forward.Normalize();
        cameraUp.Normalize();

        Quaternion lookRotation = Quaternion.LookRotation(forward, cameraUp);
        Matrix4x4 viewingMatrix = Matrix4x4.TRS(-cameraPosition, lookRotation, Vector3.one);

        Vector3[] viewingCube =
            MatrixTransform(cube, viewingMatrix);
        return viewingCube;
    }

    public Vector3[] TransformMatrixProjection(Vector3[] cube)
    {
        //Projection
        Matrix4x4 projectionMatrix = Matrix4x4.Perspective(90, 1, 1, 1000);

        Vector3[] projectionCube =
           MatrixTransform(cube, projectionMatrix);
        return projectionCube;
    }

    private void PrintVerts(Vector3[] newImage)
    {
        for (int i = 0; i < newImage.Length; i++)
            print(newImage[i].x + " , " +
                newImage[i].y + " , " +
                newImage[i].z);

    }

    private Vector3[] MatrixTransform(
        Vector3[] meshVertices,
        Matrix4x4 transformMatrix)
    {
        Vector3[] output = new Vector3[meshVertices.Length];
        for (int i = 0; i < meshVertices.Length; i++)
            output[i] = transformMatrix *
                new Vector4(
                meshVertices[i].x,
                meshVertices[i].y,
                meshVertices[i].z,
                    1);

        return output;
    }

    private void PrintMatrix(Matrix4x4 matrix)
    {
        for (int i = 0; i < 4; i++)
            print(matrix.GetRow(i).ToString());
    }



    // Update is called once per frame
    void Update()
    {

    }
}
