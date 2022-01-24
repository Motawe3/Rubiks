using System;
using System.Linq;
using UnityEngine;

public class CubeResultEvaluator : MonoBehaviour
{
    public static Action OnCubeSolved;
    
    private Cube3D cube3D;
    
    public void Initialize(Cube3D cube3D, CubeSliceRotator cubeSliceRotator)
    {
        this.cube3D = cube3D;
        cubeSliceRotator.OnSliceRotated += EvaluateCubeResult;
    }

    private void EvaluateCubeResult()
    {
        var coloredCellsCollections = cube3D.cells.GroupBy(x => x.color);

        foreach (var coloredCellsCollection in coloredCellsCollections)
        {
            var normal = coloredCellsCollection.First().transform.forward;
            if(coloredCellsCollection.Any(x => x.transform.forward != normal))
                return;
        }
        
        OnCubeSolved?.Invoke();
    }
}