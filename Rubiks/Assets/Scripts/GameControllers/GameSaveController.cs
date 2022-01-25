using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameSaveController : MonoBehaviour
{
    [Button]
    public void SaveCurrentGame()
    {
        if (GameManager.Instance.currentCube3D != null)
            SaveCubeModel();
    }
    
    public CubeModel LoadLastGame()
    {
        return File.Exists(Application.persistentDataPath + "/CubeInfo.json") ? LoadCubeModel() : null;
    }
    

    private void SaveCubeModel()
    {
        string cubeModel = JsonUtility.ToJson(GameManager.Instance.currentCube3D.cubeModel);
        File.WriteAllText(Application.persistentDataPath + "/CubeInfo.json", cubeModel);
    }

    private CubeModel LoadCubeModel()
    {
        CubeModel cubeModel = JsonUtility.FromJson<CubeModel>(File.ReadAllText( Application.persistentDataPath + "/CubeInfo.json" ));
        return cubeModel;
    }
}
