#define Debugging
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeSliceRotator : MonoBehaviour
{
    public static bool isCubeSliceRotating;
    
    public static Action OnSliceRotationStarted;
    public static Action OnSliceRotationEnded;
    
    private static GameObject rotator;
    private static GameObject destinationRotatorTarget;

    private Cube3D cube3D;

    public void Start()
    {
        cube3D = GameManager.Instance.currentCube3D;
        CreateRotationHelpers();
        
        CommandsHistoryManager.OnCommandPushed += ExecuteRotation;
        CommandsHistoryManager.OnCommandPoped += ExecuteRotation;
    }

    private void OnDestroy()
    {
        CommandsHistoryManager.OnCommandPushed -= ExecuteRotation;
        CommandsHistoryManager.OnCommandPoped -= ExecuteRotation;
        Destroy(rotator);
        Destroy(destinationRotatorTarget);
    }

    private static void CreateRotationHelpers()
    {
        if (!rotator)
            rotator = new GameObject("Rotator");
        if (!destinationRotatorTarget)
            destinationRotatorTarget = new GameObject("DestinationRotatorTarget");
    }

    private void ExecuteRotation(SliceRotationCommand rotationCommand)
    {
        isCubeSliceRotating = true;
        StartCoroutine(RotateUnits(rotationCommand));
    }
    
    private IEnumerator RotateUnits(SliceRotationCommand rotationCommand)
    {
        OnSliceRotationStarted?.Invoke();
        
        var slicePlane = CreateRotationPlane(rotationCommand);
        var sliceUnits = GetSliceUnitsOnPlane(slicePlane);
        
        ResetRotator(slicePlane.normal.Round());
        
        ParentSliceToRotator(sliceUnits);

        while (rotator.transform.rotation != destinationRotatorTarget.transform.rotation)
        {
            rotator.transform.rotation = Quaternion.Slerp(rotator.transform.rotation , destinationRotatorTarget.transform.rotation , 15.0f * Time.deltaTime);
            yield return null;
        }

        ParentSliceToCube(sliceUnits);
        
        isCubeSliceRotating = false;
        OnSliceRotationEnded?.Invoke();
    }

    private static Plane CreateRotationPlane(SliceRotationCommand rotationCommand)
    {
        Plane slicePlane = new Plane(rotationCommand.HitUnitPosition, rotationCommand.HitPointPosition,
            rotationCommand.HitUnitPosition + rotationCommand.RotationDirection);
        return slicePlane;
    }

    private List<Unit3D> GetSliceUnitsOnPlane(Plane slicePlane)
    {
        List<Unit3D> sliceUnits = new List<Unit3D>();

        var cubeUnits = cube3D.unitsArray;
        
        for (int x = 0; x < cube3D.Size; x++)
        {
            for (int y = 0; y < cube3D.Size; y++)
            {
                for (int z = 0; z < cube3D.Size; z++)
                {
                    float signedDistance = slicePlane.GetDistanceToPoint(cubeUnits[x, y, z].transform.position);
                    if (Mathf.Abs(signedDistance) < 0.1f)
                    {
                        sliceUnits.Add(cubeUnits[x, y, z]);
                    }
                }
            }
        }

        return sliceUnits;
    }

    private void ResetRotator(Vector3 rotationAxis)
    {
        destinationRotatorTarget.transform.rotation = rotator.transform.rotation = Quaternion.identity;
        destinationRotatorTarget.transform.RotateAround(Vector3.zero, rotationAxis, 90.0f);
    }

    private void ParentSliceToRotator(List<Unit3D> sliceUnits)
    {
        foreach (var sliceUnit in sliceUnits)
        {
            sliceUnit.transform.SetParent(rotator.transform);
        }
    }
    
    private void ParentSliceToCube(List<Unit3D> sliceUnits)
    {
        foreach (var sliceUnit in sliceUnits)
        {
            Transform unitTransform = sliceUnit.transform;
            unitTransform.SetParent(cube3D.transform);
            unitTransform.localPosition = sliceUnit.transform.localPosition.Round();
            unitTransform.localScale = sliceUnit.transform.localScale.Round();
            unitTransform.eulerAngles = unitTransform.eulerAngles.Round();
        }
    }

}