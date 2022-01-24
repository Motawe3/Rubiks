#define Debugging
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeSliceRotator : MonoBehaviour
{
    private Cube3D cube3D;
    private CubeRotationCommandsManager cubeRotationCommandsManager;
    
    private GameObject rotator;
    private GameObject destinationRotatorTarget;


    public void Initialize(Cube3D cube3D)
    {
        this.cube3D = cube3D;
        cubeRotationCommandsManager = new CubeRotationCommandsManager(cube3D);
        rotator = new GameObject("Rotator");
        destinationRotatorTarget = new GameObject("DestinationRotatorTarget");
    }
    
    private void Start()
    {
        CubeInteractionCapture.Instance.OnCubeRotationCaptured += ExecuteRotation;
    }

    private void OnDestroy()
    {
        CubeInteractionCapture.Instance.OnCubeRotationCaptured -= ExecuteRotation;
    }

    
    private void ExecuteRotation(Vector3 hitUnitPosition, Vector3 hitFacePosition, Vector3 rotationDirection)
    {
        if(rotationDirection == Vector3.zero || isRotating) return;
        
        this.hitUnitPosition = hitUnitPosition;
        this.hitFacePosition = hitFacePosition;
        this.rotationDirection = rotationDirection;
        
        slicePlane = new Plane(hitUnitPosition , hitFacePosition ,  hitUnitPosition + rotationDirection);

        sliceUnits = new List<Unit3D>();

        var cubeUnits = cube3D.unitsArray;
        for (int i = 0; i < cube3D.Size; i++)
        {
            for (int j = 0; j < cube3D.Size; j++)
            {
                for (int k = 0; k < cube3D.Size; k++)
                {
                    float signedDistance = slicePlane.GetDistanceToPoint(cubeUnits[i,j,k].transform.position);
                    if(Mathf.Abs(signedDistance) < 0.1f)
                    {
                        sliceUnits.Add(cubeUnits[i, j, k]);
                    }
                }
            }
        }

        StartCoroutine(RotateUnits());


        // SliceRotationCommand sliceRotationCommand = new SliceRotationCommand(cube3D, hitUnitPosition, hitFacePosition, rotationDirection);
        // sliceRotationCommand.Execute();
        // cubeRotationCommandsManager.PushCommand(sliceRotationCommand);
    }

    IEnumerator RotateUnits()
    {
        destinationRotatorTarget.transform.rotation = rotator.transform.rotation = Quaternion.identity;
        destinationRotatorTarget.transform.RotateAround(Vector3.zero , slicePlane.normal , 90.0f);
        
        foreach (var sliceUnit in sliceUnits)
        {
            sliceUnit.transform.SetParent(rotator.transform);
        }

        isRotating = true;
        
        yield return new WaitForSeconds(0.5f);

        foreach (var sliceUnit in sliceUnits)
        {
            sliceUnit.transform.SetParent(cube3D.transform);
        }
        
        isRotating = false;
    }

    private bool isRotating;
    private void Update()
    {
        if(isRotating)
            rotator.transform.rotation = Quaternion.Slerp(rotator.transform.rotation , destinationRotatorTarget.transform.rotation , 20.0f * Time.deltaTime);
    }


    [Button]
    public void UndoLastRotation()
    {
        cubeRotationCommandsManager.PopLastCommand().Undo();
    }
    
    
#if Debugging
    private Plane slicePlane;
    Vector3 hitUnitPosition, hitFacePosition, rotationDirection;
    private List<Unit3D> sliceUnits;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(hitUnitPosition , 0.3f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(hitFacePosition , 0.3f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(hitUnitPosition +  rotationDirection, 0.3f);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(hitUnitPosition , slicePlane.normal * 5.0f );
        
        if (sliceUnits != null)
        {
            foreach (var sliceUnit in sliceUnits)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(sliceUnit.transform.position , 0.2f);
            }
        }
    }
#endif
}

public class SliceRotationCommand
{
    private Cube3D cube3D;
    private Vector3 hitUnitPosition;
    private Vector3 hitFacePosition;
    private Vector3 rotationDirection;

    public SliceRotationCommand(Cube3D cube3D, Vector3 hitUnitPosition, Vector3 hitFacePosition, Vector3 rotationDirection)
    {
        this.cube3D = cube3D;
        this.hitUnitPosition = hitUnitPosition;
        this.hitFacePosition = hitFacePosition;
        this.rotationDirection = rotationDirection;
    }

    public void Execute()
    {
        Plane slicePlane = new Plane(hitUnitPosition , hitFacePosition ,  hitUnitPosition + rotationDirection);

        List<Unit3D> sliceUnits = new List<Unit3D>();

        var cubeUnits = cube3D.unitsArray;
        for (int i = 0; i < cube3D.Size; i++)
        {
            for (int j = 0; j < cube3D.Size; j++)
            {
                for (int k = 0; k < cube3D.Size; k++)
                {
                    float signedDistance = slicePlane.GetDistanceToPoint(cube3D.transform.position);
                    if(Mathf.Acos(signedDistance) < 0.1f)
                        sliceUnits.Add(cubeUnits[i,j,k]);
                }
            }
        }

    }

    public void Undo()
    {
        
    }
    

}

