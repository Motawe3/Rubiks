using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Cube3D currentCube3D { get; private set; }
    public Action OnGameStarted;
    public Action OnGameCleared;
    public Action OnGameEnded;

    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion
    
    public void StartNewGame(int size)
    {
        ClearGame();
        CubeModel cubeModel = CreateNewCubeModel(size);
        currentCube3D = Cube3DFactory.CreateCube(cubeModel);
        
        TimeManager.Instance.SetTimer(0.0f);
        OnGameStarted?.Invoke();
        StartCoroutine(BeginNewGame(cubeModel));
    }
    
    private CubeModel CreateNewCubeModel(int cubeSize)
    {
        CubeModel cubeModel = new CubeModel(cubeSize);
        cubeModel.ResetCubeColors();
        return cubeModel;
    }
    
    IEnumerator BeginNewGame(CubeModel cubeModel)
    {
        var cubeScrambler = currentCube3D.GetComponent<CubeScrambler>();
        cubeScrambler.ScrambleCube();
        while (cubeScrambler.IsScrambling)
        {
            yield return null;
        }
        
        yield return null;
        
        InteractionManager.Instance.EnablePlayerInteraction(true);
        OnGameStarted?.Invoke();
    }
    
    public void LoadSavedGame()
    {
        ClearGame();
        SavedGameModel savedGameModel = CubeSaverLoader.LoadLastGame();
        CubeModel cubeModel = savedGameModel.cubeModel;
        currentCube3D = Cube3DFactory.CreateCube(cubeModel);
        
        TimeManager.Instance.SetTimer(savedGameModel.playedTime);
        OnGameStarted?.Invoke();    
        InteractionManager.Instance.EnablePlayerInteraction(true);
    }
    
    public void SaveGame()
    {
        CubeSaverLoader.SaveGame(currentCube3D.cubeModel,TimeManager.Instance.PlayTime);
    }
    
    public void PauseGame(bool isPaused)
    {
        InteractionManager.Instance.EnablePlayerInteraction(!isPaused);
    }
    
    public void EndGame()
    {
        ClearGame();
        OnGameEnded?.Invoke();
    }

    private void ClearGame()
    {
        InteractionManager.Instance.EnablePlayerInteraction(false);
        if(!currentCube3D) return;
        CommandsHistoryManager.ClearHistory();
        DestroyCurrentCube();
        OnGameCleared?.Invoke();
    }
   
    private void DestroyCurrentCube()
    {
        Destroy(currentCube3D.gameObject);
        currentCube3D = null;
    }
}
