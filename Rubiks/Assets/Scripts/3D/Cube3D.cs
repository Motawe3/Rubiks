using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Cube3D : MonoBehaviour
{
    [ShowInInspector]
    private CubeModel cubeModel;
    private GameObject cubeUnitPrefab;
    private GameObject cubeCellPrefab;
    public int Size => cubeModel.cubicalSize;

    private Cube3D[,,] unitsArray;
    private List<Cell3D> cells;

    public void Initialize(CubeModel cubeModel)
    {
        this.cubeModel = cubeModel;
        unitsArray = new Cube3D[cubeModel.cubicalSize, cubeModel.cubicalSize, cubeModel.cubicalSize];
        cells = new List<Cell3D>();
        
        cubeUnitPrefab = Resources.Load<GameObject>("CubeComponents/CubeUnit");
        cubeCellPrefab = Resources.Load<GameObject>("CubeComponents/CubeCell");
        Create3DCubeUnits();
        Create3DCells();
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
                    Cube3D cube3D = cubeUnit.AddComponent<Cube3D>();
                    unitsArray[i, j, k] = cube3D;
                    
                }
            }
        }
    }
    
    private void Create3DCells()
    {
        CreateButtomFace(cubeModel.faces[0]);
        CreateLeftFace(cubeModel.faces[1]);
        CreateFrontFace(cubeModel.faces[2]);
        CreateRightFace(cubeModel.faces[3]);
        CreateBackFace(cubeModel.faces[4]);
        CreateTopFace(cubeModel.faces[5]);
    }

    private void CreateButtomFace(FaceModel faceModel)
    {
        for (int x = 0; x < faceModel.cells.GetLength(0); x++)
        {
            for (int y = 0; y < faceModel.cells.GetLength(1); y++)
            {
                GameObject cell = Instantiate(cubeCellPrefab, unitsArray[x,0,y].transform, false);
                cell.transform.rotation = Quaternion.LookRotation(Vector3.down);
                Cell3D cell3D = cell.AddComponent<Cell3D>();
                cell3D.SetColor(faceModel.cells[x,y]);
                cells.Add(cell3D);
            }
        }
    }
    
    private void CreateTopFace(FaceModel faceModel)
    {
        for (int x = 0; x < faceModel.cells.GetLength(0); x++)
        {
            for (int y = 0; y < faceModel.cells.GetLength(1); y++)
            {
                GameObject cell = Instantiate(cubeCellPrefab, unitsArray[x,Size - 1,y].transform, false);
                cell.transform.rotation = Quaternion.LookRotation(Vector3.up);
                Cell3D cell3D = cell.AddComponent<Cell3D>();
                cell3D.SetColor(faceModel.cells[x,y]);
                cells.Add(cell3D);
            }
        }
    }
    
    private void CreateLeftFace(FaceModel faceModel)
    {
        for (int x = 0; x < faceModel.cells.GetLength(0); x++)
        {
            for (int y = 0; y < faceModel.cells.GetLength(1); y++)
            {
                GameObject cell = Instantiate(cubeCellPrefab, unitsArray[0,y,x].transform, false);
                cell.transform.rotation = Quaternion.LookRotation(Vector3.left);
                Cell3D cell3D = cell.AddComponent<Cell3D>();
                cell3D.SetColor(faceModel.cells[x,y]);
                cells.Add(cell3D);
            }
        }
    }
    
    private void CreateRightFace(FaceModel faceModel)
    {
        for (int x = 0; x < faceModel.cells.GetLength(0); x++)
        {
            for (int y = 0; y < faceModel.cells.GetLength(1); y++)
            {
                GameObject cell = Instantiate(cubeCellPrefab, unitsArray[Size - 1,y,x].transform, false);
                cell.transform.rotation = Quaternion.LookRotation(Vector3.right);
                Cell3D cell3D = cell.AddComponent<Cell3D>();
                cell3D.SetColor(faceModel.cells[x,y]);
                cells.Add(cell3D);
            }
        }
    }
    
    private void CreateFrontFace(FaceModel faceModel)
    {
        for (int x = 0; x < faceModel.cells.GetLength(0); x++)
        {
            for (int y = 0; y < faceModel.cells.GetLength(1); y++)
            {
                GameObject cell = Instantiate(cubeCellPrefab, unitsArray[x,y,0].transform, false);
                cell.transform.rotation = Quaternion.LookRotation(Vector3.back);
                Cell3D cell3D = cell.AddComponent<Cell3D>();
                cell3D.SetColor(faceModel.cells[x,y]);
                cells.Add(cell3D);
            }
        }
    }
    
    private void CreateBackFace(FaceModel faceModel)
    {
        for (int x = 0; x < faceModel.cells.GetLength(0); x++)
        {
            for (int y = 0; y < faceModel.cells.GetLength(1); y++)
            {
                GameObject cell = Instantiate(cubeCellPrefab, unitsArray[x,y,Size - 1].transform, false);
                cell.transform.rotation = Quaternion.LookRotation(Vector3.forward);
                Cell3D cell3D = cell.AddComponent<Cell3D>();
                cell3D.SetColor(faceModel.cells[x,y]);
                cells.Add(cell3D);
            }
        }
    }
   
}
