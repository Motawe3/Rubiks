using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GestureManager : MonoBehaviour
{
    #region Singleton

    private static GestureManager _instance;

    public static GestureManager Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public Action<bool> OnCapturing;
    [ShowInInspector, ReadOnly] public float accHorizontalAxisMovement { get; private set; }
    [ShowInInspector, ReadOnly] public float accVerticalAxisMovement { get; private set; }
    [ShowInInspector, ReadOnly] public float horizontalAxisMovement { get; private set; }
    [ShowInInspector, ReadOnly] public float verticalAxisMovement { get; private set; }
    [ShowInInspector, ReadOnly] public DirectionType currentDirectionType { get; private set; }

    private void Update()
    {
        EvaluateCaturing();
    }

    private void EvaluateCaturing()
    {
        if (Input.GetMouseButtonDown(0))
            StartCapturing();
        if (Input.GetMouseButton(0))
            UpdateCapturing();
        if (Input.GetMouseButtonUp(0))
            StopCapturing();
    }

    private void StartCapturing()
    {
        OnCapturing?.Invoke(true);
    }
    
    private void UpdateCapturing()
    {
        horizontalAxisMovement = Input.GetAxis("Mouse X");
        verticalAxisMovement = Input.GetAxis("Mouse Y");
        
        accHorizontalAxisMovement += horizontalAxisMovement;
        accVerticalAxisMovement += verticalAxisMovement;
        
        currentDirectionType = Math.Abs(accHorizontalAxisMovement) > Math.Abs(accVerticalAxisMovement) ? DirectionType.Horizontal : DirectionType.Vertical;
    }
    
    private void StopCapturing()
    {
        OnCapturing?.Invoke(false);

        accHorizontalAxisMovement = 0;
        accVerticalAxisMovement = 0;
    }

}

public enum DirectionType
{
    Horizontal,
    Vertical
}