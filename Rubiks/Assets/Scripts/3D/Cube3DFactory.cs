using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Cube3DFactory : MonoBehaviour
{
    private Transform cubeAnchor;
    private Cube3D currentCube;

    private void Start()
    {
        CreateCube(3);
    }

    [Button]
    public void CreateCube(int cubeSize)
    {
        if(cubeSize < 2 || cubeSize > 6)
        {
            Debug.LogError("Cube Size Not Supported!");
            return;
        }
        
        CubeModel cubeModel = CreateNewCubeModel(cubeSize);
        CreateCube3D(cubeModel);
        CenterCurrentCube();
        AnchorCube();
    }

    private void CreateCube3D(CubeModel cubeModel)
    {
        GameObject cubeObj = new GameObject("RubiksCube");
        currentCube = cubeObj.AddComponent<Cube3D>();
        currentCube.Initialize(cubeModel);
    }

    private CubeModel CreateNewCubeModel(int cubeSize)
    {
        CubeModel cubeModel = new CubeModel(cubeSize);
        cubeModel.ResetCubeColors();
        return cubeModel;
    }
    
    private void CenterCurrentCube()
    {
        float translationValue = (currentCube.Size * 0.5f * -1) + 0.5f;
        currentCube.transform.Translate(new Vector3(translationValue, translationValue , translationValue));
    }
    
    private void AnchorCube()
    {
        if (!cubeAnchor)
        {
            cubeAnchor = new GameObject("CubeAnchor").transform;
        }
        
        currentCube.transform.SetParent(cubeAnchor.transform);
    }

    [Button]
    public void DestroyCurrentCube()
    {
        Destroy(currentCube.gameObject);
        currentCube = null;
    }
   
}
