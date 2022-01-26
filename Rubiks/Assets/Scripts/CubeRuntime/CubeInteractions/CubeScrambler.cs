using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeScrambler : MonoBehaviour
{
    public bool IsScrambling { get; private set; }
    
    private Cube3D cube3D;
    private System.Random random = new System.Random();
    
    public void ScrambleCube()
    {
        IsScrambling = true;
        cube3D = GameManager.Instance.currentCube3D;
        StartCoroutine(Scrambling(cube3D.Size * 4));
    }

    IEnumerator Scrambling(int numberOfScrambles)
    {
        yield return null;

        for (int i = 0; i < numberOfScrambles; i++)
        {
            PushRandomRotation();
            
            while (CubeSliceRotator.isCubeSliceRotating)
            {
                yield return null;
            }
            
            yield return null;
        }

        CommandsHistoryManager.ClearHistory();
        IsScrambling = false;
    }

    private void PushRandomRotation()
    {
        Transform randomCellTransform = cube3D.cells[random.Next(0, cube3D.cells.Count)].transform;

        Vector3 hitUnitPosition = randomCellTransform.position;
        Vector3 hitPointPosition = randomCellTransform.TransformPoint(randomCellTransform.GetComponent<BoxCollider>().center);

        Vector3 rotationDirection = randomCellTransform.right;
        int randomFactor = random.Next(0, 100);
        if(randomFactor < 25)
            rotationDirection = randomCellTransform.right;
        else if(randomFactor < 50)
            rotationDirection = -randomCellTransform.right;
        else if(randomFactor < 75)
            rotationDirection = randomCellTransform.up;
        else if(randomFactor < 100)
            rotationDirection = -randomCellTransform.up;
        
        SliceRotationCommand rotationCommand = new SliceRotationCommand(hitUnitPosition, hitPointPosition, rotationDirection);
        CommandsHistoryManager.PushCommand(rotationCommand);
    }
}