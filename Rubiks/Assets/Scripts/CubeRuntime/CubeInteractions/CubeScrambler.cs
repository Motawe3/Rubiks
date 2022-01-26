using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeScrambler : MonoBehaviour
{
    public bool IsScrambling { get; private set; }
    
    private Cube3D cube3D;
    private System.Random random = new System.Random();
    
    public void Start()
    {
        cube3D = GameManager.Instance.currentCube3D;
    }
    
    [Button]
    public void ScrambleCube(int numberOfScrambles = 15)
    {
        IsScrambling = true;
        StartCoroutine(Scrambling(numberOfScrambles));
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
        Vector3 hitFacePosition = randomCellTransform.TransformPoint(randomCellTransform.GetComponent<BoxCollider>().center);
        Vector3 rotationDirection = random.Next(0,10) > 5 ? randomCellTransform.right : -randomCellTransform.right;

        SliceRotationCommand rotationCommand = new SliceRotationCommand(hitUnitPosition, hitFacePosition, rotationDirection);
        CommandsHistoryManager.PushCommand(rotationCommand);
    }
}