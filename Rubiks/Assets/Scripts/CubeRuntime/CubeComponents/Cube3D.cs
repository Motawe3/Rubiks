﻿using System.Collections.Generic;
using UnityEngine;

public class Cube3D : MonoBehaviour
{
    public CubeModel cubeModel;
    private GameObject cubeUnitPrefab;
    private GameObject cubeCellPrefab;
    public int Size => cubeModel.cubicalSize;

    public Unit3D[,,] unitsArray { get; private set; }
    public List<Cell3D> cells;

    public void Initialize(CubeModel cubeModel)
    {
        this.cubeModel = cubeModel;
        
        InitializeCollections();
        LoadResourcesPrefabs();
        Create3DCubeUnits();
        CreateCells();
        AddCubeComponents();
    }

    private void InitializeCollections()
    {
        unitsArray = new Unit3D[cubeModel.cubicalSize, cubeModel.cubicalSize, cubeModel.cubicalSize];
        cells = new List<Cell3D>();
    }

    private void LoadResourcesPrefabs()
    {
        cubeUnitPrefab = Resources.Load<GameObject>("CubeComponents/CubeUnit");
        cubeCellPrefab = Resources.Load<GameObject>("CubeComponents/CubeCell");
    }

    private void Create3DCubeUnits()
    {
        int size = cubeModel.cubicalSize;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                for (int z = 0; z < size; z++)
                {
                    GameObject cubeUnit = Instantiate(cubeUnitPrefab , new Vector3(x , y , z) , Quaternion.identity);
                    cubeUnit.transform.SetParent(transform);
                    cubeUnit.name = $"{x} {y} {z}";
                    
                    Unit3D unit3D = cubeUnit.AddComponent<Unit3D>();
                    unitsArray[x, y, z] = unit3D;
                    unit3D.SetIndex(new UnitIndex(x,y,z));
                }
            }
        }
    }

    private void CreateCells()
    {
        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                // Draw Bottom Cell
                DrawCell(Vector3.down, cubeModel.faces[0].cells[y * Size + x], unitsArray[x, 0, Size - 1 - y].transform);
                
                // Draw Left Cell
                DrawCell(Vector3.left, cubeModel.faces[1].cells[y * Size + x], unitsArray[0, y, Size - 1 - x].transform);
                
                // Draw Front Cell
                DrawCell(Vector3.back, cubeModel.faces[2].cells[y * Size + x], unitsArray[x,y,0].transform);
                
                // Draw Right Cell
                DrawCell(Vector3.right, cubeModel.faces[3].cells[y * Size + x], unitsArray[Size - 1,y,x].transform);
                
                // Draw Back Cell
                DrawCell(Vector3.forward, cubeModel.faces[4].cells[y * Size + x], unitsArray[Size - 1 - x,y,Size - 1].transform);
                
                // Draw Top Cell
                DrawCell(Vector3.up, cubeModel.faces[5].cells[y * Size + x], unitsArray[x,Size - 1,y].transform);
            }
        }
    }

    private void DrawCell(Vector3 direction, CellColor color, Transform parentUnit)
    {
        GameObject cell = Instantiate(cubeCellPrefab, parentUnit, false);
        cell.transform.rotation = Quaternion.LookRotation(direction);
        Cell3D cell3D = cell.AddComponent<Cell3D>();
        cell3D.SetColor(color);
        cells.Add(cell3D);
    }
    
    private void AddCubeComponents()
    {
        CubeInteractionCapture cubeInteractionCapture = gameObject.AddComponent<CubeInteractionCapture>();
        CubeSliceRotator sliceRotator = gameObject.AddComponent<CubeSliceRotator>();
        CubeScrambler cubeScrambler = gameObject.AddComponent<CubeScrambler>();
        CubeResultEvaluator cubeResultEvaluator = gameObject.AddComponent<CubeResultEvaluator>();
    }
   
}
