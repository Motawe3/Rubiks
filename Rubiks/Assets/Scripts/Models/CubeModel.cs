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
        faces[0].SetFaceColor(CellColor.White);
        faces[1].SetFaceColor(CellColor.Red);
        faces[2].SetFaceColor(CellColor.Blue);
        faces[3].SetFaceColor(CellColor.Orange);
        faces[4].SetFaceColor(CellColor.Green);
        faces[5].SetFaceColor(CellColor.Yellow);
    }
}