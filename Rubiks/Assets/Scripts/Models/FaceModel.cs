using System;
using Sirenix.OdinInspector;

[Serializable]
public class FaceModel
{
    public int planarSize;
    public CellColor[,] cells;

    public FaceModel(int planarSize)
    {
        this.planarSize = planarSize;
        CreateCells();
    }
    
    private void CreateCells()
    {
        cells = new CellColor[planarSize , planarSize];
    }

    public void SetColor(CellColor color)
    {
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = color;
            }
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