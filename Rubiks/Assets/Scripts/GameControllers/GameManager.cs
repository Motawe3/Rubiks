using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Action<Cube3D> OnCubeCreated;
    public Action OnCubeDestroyed;
    
    private Cube3DFactory cube3DFactory;
    private Cube3D currentCube3D;
    
    void Start()
    {
        if (cube3DFactory == null)
            cube3DFactory = new Cube3DFactory();

        CreateNewCube(3);
    }

    [Button]
    public void CreateNewCube(int size)
    {
        currentCube3D = cube3DFactory.CreateCube(3);
    }

    public void StartGame()
    {
        
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
