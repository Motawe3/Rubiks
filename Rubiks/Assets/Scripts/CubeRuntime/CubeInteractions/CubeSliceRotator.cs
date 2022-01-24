#define Debugging
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeSliceRotator : MonoBehaviour
{
    public Action OnSliceRotated;
    private Cube3D cube3D;
    private CubeRotationCommandsHistory cubeRotationCommandsHistory;
    private CubeInteractionCapture cubeInteractionCapture;
    private bool isRotating;
    private GameObject rotator;
    private GameObject destinationRotatorTarget;

    public void Initialize(Cube3D cube3D, CubeInteractionCapture cubeInteractionCapture)
    {
        this.cube3D = cube3D;
        this.cubeInteractionCapture = cubeInteractionCapture;
        cubeRotationCommandsHistory = new CubeRotationCommandsHistory(cube3D);
        rotator = new GameObject("Rotator");
        destinationRotatorTarget = new GameObject("DestinationRotatorTarget");
    }
    
    private void Start()
    {
        cubeInteractionCapture.OnCubeRotationCaptured += ExecuteRotation;
    }

    private void OnDestroy()
    {
        cubeInteractionCapture.OnCubeRotationCaptured -= ExecuteRotation;
    }

    private void ExecuteRotation(Vector3 hitUnitPosition, Vector3 hitFacePosition, Vector3 rotationDirection)
    {
        if(rotationDirection == Vector3.zero || isRotating) return;
        
        isRotating = true;

        SliceRotationCommand rotationCommand = new SliceRotationCommand(hitUnitPosition, hitFacePosition, rotationDirection);
        cubeRotationCommandsHistory.PushCommand(rotationCommand);

        StartCoroutine(RotateUnits(rotationCommand));
    }
    
    [Button]
    private void UndoLastRotation()
    {
        if(cubeRotationCommandsHistory.HasCommands() == false || isRotating) return;
        
        isRotating = true;
        
        SliceRotationCommand rotationCommand = cubeRotationCommandsHistory.PopLastCommand();
        StartCoroutine(RotateUnits(rotationCommand.UndoCommand()));
    }

    
    private IEnumerator RotateUnits(SliceRotationCommand rotationCommand)
    {
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
        
        isRotating = false;
        
        OnSliceRotated?.Invoke();
    }

    private static Plane CreateRotationPlane(SliceRotationCommand rotationCommand)
    {
        Plane slicePlane = new Plane(rotationCommand.hitUnitPosition, rotationCommand.hitFacePosition,
            rotationCommand.hitUnitPosition + rotationCommand.rotationDirection);
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