using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float zoomStep = 2.0f;
    [SerializeField] float zoomSpeed = 10.0f;
    [SerializeField] float minDistance = 8.0f;
    [SerializeField] float maxDistance = 30.0f;

    private Transform cameraTransform;
    private Transform zoomTargetTransform;
    private float currentCameraDistance;
    
    void Start()
    {
        cameraTransform = camera.transform;
        currentCameraDistance = Vector3.Distance(cameraTransform.position, Vector3.zero);
        CreateZoomTargetObject();
    }

    private void CreateZoomTargetObject()
    {
        GameObject targetObject = new GameObject("TargetObject");
        zoomTargetTransform = targetObject.transform;
        zoomTargetTransform.SetParent(cameraTransform.parent);
        zoomTargetTransform.position = cameraTransform.position;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f )
        {
            MoveTargetIn();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f )
        {
            MoveTargetOut();
        }

        if (cameraTransform.position != zoomTargetTransform.position)
            FollowTarget();
    }

    private void MoveTargetIn()
    {
        Vector3 newTargetPosition = cameraTransform.position + cameraTransform.forward * zoomStep;
        if(minDistance > Vector3.Distance(newTargetPosition , Vector3.zero))
            return;
        zoomTargetTransform.position = newTargetPosition;
    }
    
    private void MoveTargetOut()
    {
        Vector3 newTargetPosition = cameraTransform.position - cameraTransform.forward * zoomStep;
        if(maxDistance < Vector3.Distance(newTargetPosition , Vector3.zero))
            return;
        zoomTargetTransform.position = newTargetPosition;
    }
    
    private void FollowTarget()
    {
        cameraTransform.position = Vector3.Slerp(cameraTransform.position, zoomTargetTransform.position, Time.deltaTime * zoomSpeed);
    }
}
