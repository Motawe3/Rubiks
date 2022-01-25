using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Cube3D currentCube3D { get; private set; }
    private Cube3DFactory cube3DFactory;

    public Action<Cube3D> OnCubeCreated;
    public Action OnCubeDestroyed;

    [SerializeField] private GameSaveController gameSaveController;

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
    
    void Start()
    {
        if (cube3DFactory == null)
            cube3DFactory = new Cube3DFactory();
    }

    [Button]
    public void CreateNewCube(int size)
    {
        CubeModel cubeModel = CreateNewCubeModel(size);
        CreateCube3D(cubeModel);
    }
    
    [Button]
    public void LoadSavedCube()
    {
        CubeModel cubeModel = gameSaveController.LoadLastGame();
        if(cubeModel != null)
        {
            if(currentCube3D)
                DestroyCurrentCube();
            currentCube3D = cube3DFactory.CreateCube(cubeModel);
        }        else
            Debug.Log("No Saved Game");            
    }
    
    private CubeModel CreateNewCubeModel(int cubeSize)
    {
        CubeModel cubeModel = new CubeModel(cubeSize);
        cubeModel.ResetCubeColors();
        return cubeModel;
    }

    private void CreateCube3D(CubeModel cubeModel)
    {
        if (currentCube3D)
            DestroyCurrentCube();
        currentCube3D = cube3DFactory.CreateCube(cubeModel);
    }
   
    [Button]
    public void DestroyCurrentCube()
    {
        Destroy(currentCube3D.gameObject);
        currentCube3D = null;
    }
    
    [Button]
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title",LoadSceneMode.Single);
    }
}
