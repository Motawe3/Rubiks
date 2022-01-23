using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GestureCapture : MonoBehaviour
{
    public Action<bool> OnCapturing;
    public float accHorizontalAxisMovement;
    public float accVerticalAxisMovement;
    public float horizontalAxisMovement;
    public float verticalAxisMovement;
    public DirectionType currentDirectionType;
    
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