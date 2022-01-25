using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Cube3DFactory
{
    private Transform cubeAnchor;

    public Cube3D CreateCube(CubeModel cubeModel)
    {
        Cube3D cube3D = CreateCube3D(cubeModel);
        CenterCube(cube3D);
        AnchorCube(cube3D);
        return cube3D;
    }

    private Cube3D CreateCube3D(CubeModel cubeModel)
    {
        GameObject cubeObj = new GameObject("RubiksCube");
        Cube3D cube3D = cubeObj.AddComponent<Cube3D>();
        cube3D.Initialize(cubeModel);
        return cube3D;
    }
    
    private void CenterCube(Cube3D cube3D)
    {
        float translationValue = (cube3D.Size * 0.5f * -1) + 0.5f;
        cube3D.transform.Translate(new Vector3(translationValue, translationValue , translationValue));
    }
    
    private void AnchorCube(Cube3D cube3D)
    {
        if (!cubeAnchor)
        {
            cubeAnchor = new GameObject("CubeAnchor").transform;
        }
        
        cube3D.transform.SetParent(cubeAnchor.transform);
    }

   
}
