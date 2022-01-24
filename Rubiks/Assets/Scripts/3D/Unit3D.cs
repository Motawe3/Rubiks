using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit3D : MonoBehaviour
{
    public UnitIndex UnitIndex { get; private set; }
    public void SetIndex(UnitIndex unitIndex)
    {
        this.UnitIndex = unitIndex;
    }
}

public struct UnitIndex
{
    public int x { get; private set; }
    public int y { get; private set; }
    public int z { get; private set; }
    
    public UnitIndex(int x , int y , int z)
    {
        this.x = x;
        this.y = x;
        this.z = x;
    }
}