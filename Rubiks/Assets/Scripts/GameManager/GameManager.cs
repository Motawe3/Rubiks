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
        CubeModel cubeModel = CreateNewCubeModel(size);
        currentCube3D = Cube3DFactory.CreateCube(cubeModel);
        OnGameStarted?.Invoke();
    }
    
    public void LoadSavedGame()
    {
        CubeModel cubeModel = CubeSaverLoader.LoadLastGame();
        currentCube3D = Cube3DFactory.CreateCube(cubeModel);
        OnGameStarted?.Invoke();
    }
    
    public void PauseGame()
    {
        
    }
    
    public void EndGame()
    {
        if (currentCube3D)
            DestroyCurrentCube();
        
        OnGameEnded?.Invoke();
    }
    
    private CubeModel CreateNewCubeModel(int cubeSize)
    {
        CubeModel cubeModel = new CubeModel(cubeSize);
        cubeModel.ResetCubeColors();
        return cubeModel;
    }
   
    private void DestroyCurrentCube()
    {
        Destroy(currentCube3D.gameObject);
        currentCube3D = null;
    }
}
