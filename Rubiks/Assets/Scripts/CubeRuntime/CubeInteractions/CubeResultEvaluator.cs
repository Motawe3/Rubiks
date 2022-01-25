using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeResultEvaluator : MonoBehaviour
{
    public static Action OnCubeSolved;
    
    private Cube3D cube3D;
    
    public void Initialize(Cube3D cube3D, CubeSliceRotator cubeSliceRotator)
    {
        this.cube3D = cube3D;
        cubeSliceRotator.OnSliceRotated += ReEvaluateCube;
    }

    void ReEvaluateCube()
    {
        EvaluateCubeModel();
        EvaluateCubeSolving();
    }

    private void EvaluateCubeModel()
    {
        var facesCollections = cube3D.cells.GroupBy(x => x.transform.forward.Round());

        foreach (var faceCollection in facesCollections)
        {
            if (faceCollection.Key == Vector3.down)
                cube3D.cubeModel.faces[0].cells = faceCollection
                    .OrderBy(x => x.transform.position.x)
                    .GroupBy(x => x.transform.position.x)
                    .OrderByDescending(x => x.Key)
                    .SelectMany(x => x)
                    .Select(x => x.color)
                    .ToList();

            if (faceCollection.Key == Vector3.left)
                cube3D.cubeModel.faces[1].cells = faceCollection
                    .OrderByDescending(x => x.transform.position.z)
                    .GroupBy(x => x.transform.position.y)
                    .OrderBy(x => x.Key)
                    .SelectMany(x => x)
                    .Select(x => x.color)
                    .ToList();

            if (faceCollection.Key == Vector3.back)
                cube3D.cubeModel.faces[2].cells = faceCollection
                    .OrderBy(x => x.transform.position.x)
                    .GroupBy(x => x.transform.position.y)
                    .OrderBy(x => x.Key)
                    .SelectMany(x => x)
                    .Select(x => x.color)
                    .ToList();

            if (faceCollection.Key == Vector3.right)
                cube3D.cubeModel.faces[3].cells = faceCollection
                    .OrderBy(x => x.transform.position.z)
                    .GroupBy(x => x.transform.position.y)
                    .OrderBy(x => x.Key)
                    .SelectMany(x => x)
                    .Select(x => x.color)
                    .ToList();

            if (faceCollection.Key == Vector3.forward)
                cube3D.cubeModel.faces[4].cells = faceCollection
                    .OrderByDescending(x => x.transform.position.x)
                    .GroupBy(x => x.transform.position.y)
                    .OrderBy(x => x.Key)
                    .SelectMany(x => x)
                    .Select(x => x.color)
                    .ToList();

            if (faceCollection.Key == Vector3.up)
                cube3D.cubeModel.faces[5].cells = faceCollection
                    .OrderBy(x => x.transform.position.x)
                    .GroupBy(x => x.transform.position.z)
                    .OrderBy(x => x.Key)
                    .SelectMany(x => x)
                    .Select(x => x.color)
                    .ToList();
        }
    }

    private static List<CellColor> GetSortedColors(IGrouping<Vector3, Cell3D> faceCollection)
    {
        return faceCollection
            .OrderBy(x => x.transform.localPosition.x)
            .ThenBy(x => x.transform.localPosition.y)
            .ThenBy(x => x.transform.localPosition.x)
            .Select(x => x.color).ToList();
    }

    private void EvaluateCubeSolving()
    {
        foreach (var face in cube3D.cubeModel.faces)
        {
            var color = face.cells.First();
            if(face.cells.Any(x => x != color))
                break;
        }
        
        OnCubeSolved?.Invoke();
    }
}