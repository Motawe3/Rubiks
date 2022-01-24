﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Cube3D : MonoBehaviour
{
    private CubeModel cubeModel;
    private GameObject cubeUnitPrefab;
    private GameObject cubeCellPrefab;
    public int Size => cubeModel.cubicalSize;

    public Unit3D[,,] unitsArray { get; private set; }
    private List<Cell3D> cells;

    public void Initialize(CubeModel cubeModel)
    {
        this.cubeModel = cubeModel;
        
        InitializeCollections();
        LoadResourcesPrefabs();
        Create3DCubeUnits();
        CreateCells();
        AddCubeSliceRotator();
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

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    GameObject cubeUnit = Instantiate(cubeUnitPrefab , new Vector3(i , j , k) , Quaternion.identity);
                    cubeUnit.transform.SetParent(transform);
                    
                    Unit3D unit3D = cubeUnit.AddComponent<Unit3D>();
                    unitsArray[i, j, k] = unit3D;
                    unit3D.SetIndex(new UnitIndex(i,j,k));
                }
            }
        }
    }

    private void CreateCells()
    {
        for (int x = 0; x < Size; x++)
        {
            for (int y = 0; y < Size; y++)
            {
                // Draw Bottom Cell
                DrawCell(Vector3.down, cubeModel.faces[0].cells[x, y], unitsArray[x, 0, y].transform);
                
                // Draw Left Cell
                DrawCell(Vector3.left, cubeModel.faces[1].cells[x, y], unitsArray[0,y,x].transform);
                
                // Draw Front Cell
                DrawCell(Vector3.back, cubeModel.faces[2].cells[x, y], unitsArray[x,y,0].transform);
                
                // Draw Right Cell
                DrawCell(Vector3.right, cubeModel.faces[3].cells[x, y], unitsArray[Size - 1,y,x].transform);
                
                // Draw Back Cell
                DrawCell(Vector3.forward, cubeModel.faces[4].cells[x, y], unitsArray[x,y,Size - 1].transform);
                
                // Draw Top Cell
                DrawCell(Vector3.up, cubeModel.faces[5].cells[x, y], unitsArray[x,Size - 1,y].transform);
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
    
    private void AddCubeSliceRotator()
    {
        CubeSliceRotator sliceRotator = gameObject.AddComponent<CubeSliceRotator>();
        sliceRotator.Initialize(this);
    }
}
