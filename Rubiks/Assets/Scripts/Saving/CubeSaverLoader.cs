using System.IO;
using UnityEngine;

public static class CubeSaverLoader
{
    private static string filePath = "/GameInfo.json";
    
    public static void SaveGame(CubeModel cubeModel, double playedTime)
    {
        SavedGameModel savedGameModel = new SavedGameModel(cubeModel, playedTime);
        SavedGameModel(savedGameModel);
    }
    
    public static SavedGameModel LoadLastGame()
    {
        return HasGameSaved()? LoadSavedGameModel() : null;
    }
    
    public static bool HasGameSaved()
    {
        return File.Exists(Application.persistentDataPath + filePath);
    }
    
    private static void SavedGameModel(SavedGameModel savedGameModel)
    {
        string savedGameStr = JsonUtility.ToJson(savedGameModel);
        File.WriteAllText(Application.persistentDataPath + filePath, savedGameStr);
    }

    private static SavedGameModel LoadSavedGameModel()
    {
        SavedGameModel savedGameModel = JsonUtility.FromJson<SavedGameModel>(File.ReadAllText( Application.persistentDataPath + filePath ));
        return savedGameModel;
    }
}

public class SavedGameModel
{
    public CubeModel cubeModel;
    public double playedTime;
    public SavedGameModel(CubeModel cubeModel , double playedTime)
    {
        this.cubeModel = cubeModel;
        this.playedTime = playedTime;
    }
}
