using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[Serializable]
public class FaceModel
{
    public int planarSize;
    // orderer by 2D coordinates, replaced using [,] because of time it would take to hanlde saving
    public List<CellColor> cells;
    
    public FaceModel(int planarSize)
    {
        this.planarSize = planarSize;
        CreateCells();
    }
    
    private void CreateCells()
    {
        cells = new List<CellColor>(new CellColor[(int)Math.Pow(planarSize , 2)]);
    }

    public void SetFaceColor(CellColor color)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            cells[i] = color;
        }
    }
}

public enum CellColor
{
    White = 0,
    Red = 1,
    Blue = 2,
    Orange = 3,
    Green = 4,
    Yellow = 5
}