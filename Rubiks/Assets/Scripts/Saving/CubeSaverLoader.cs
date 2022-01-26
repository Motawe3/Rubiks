using System.IO;
using UnityEngine;

public static class CubeSaverLoader
{
    private static void SaveCubeModel(Cube3D cube3D)
    {
        string cubeModel = JsonUtility.ToJson(GameManager.Instance.currentCube3D.cubeModel);
        File.WriteAllText(Application.persistentDataPath + "/CubeInfo.json", cubeModel);
    }
    
    public static CubeModel LoadLastGame()
    {
        return File.Exists(Application.persistentDataPath + "/CubeInfo.json") ? LoadCubeModel() : null;
    }

    public static bool HasGameSaved()
    {
        return File.Exists(Application.persistentDataPath + "/CubeInfo.json");
    }

    private static CubeModel LoadCubeModel()
    {
        CubeModel cubeModel = JsonUtility.FromJson<CubeModel>(File.ReadAllText( Application.persistentDataPath + "/CubeInfo.json" ));
        return cubeModel;
    }
}
