using System;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;

[Serializable]
public class CubeModel
{
    public int cubicalSize;
    public FaceModel[] faces;

    public CubeModel(int cubicalSize)
    {
        this.cubicalSize = cubicalSize;
        CreateFaces();
    }

    private void CreateFaces()
    {
        faces = new FaceModel[6];
        
        for (int i = 0; i < faces.Length; i++)
        {
            faces[i] = new FaceModel(cubicalSize);
        }
    }

    public void ResetCubeColors()
    {
        faces[0].SetColor(CellColor.White);
        faces[1].SetColor(CellColor.Red);
        faces[2].SetColor(CellColor.Blue);
        faces[3].SetColor(CellColor.Orange);
        faces[4].SetColor(CellColor.Green);
        faces[5].SetColor(CellColor.Yellow);
    }
}