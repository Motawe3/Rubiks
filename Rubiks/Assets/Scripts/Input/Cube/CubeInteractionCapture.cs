//#define Debugging
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeInteractionCapture : MonoBehaviour
{
    private bool isInteraction;
    
    private Camera mainCamera;
    
    private GameObject firstHitFace;
    private Vector3 firstHitPoint;
    private GameObject secondHitFace;
    private Vector3 secondHitPoint;
    private Vector3 interactionDirection;
    private Vector3 rotationDirection;

    private void Start()
    {
        mainCamera = Camera.main;
        InteractionManager.Instance.OnCapturing += UpdateIsRotating;
    }

    private void OnDestroy()
    {
        InteractionManager.Instance.OnCapturing -= UpdateIsRotating;
    }

    private void UpdateIsRotating(bool isCapturing)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (isCapturing && !CubeSliceRotator.isCubeSliceRotating && Physics.Raycast(ray, out var hit) && hit.collider.CompareTag("CubeCell"))
        {
            StartCapturingInteraction(hit);
        }
        else
        {
            StopCapturingInteraction();
        }
    }

    private void StartCapturingInteraction(RaycastHit hit)
    {
        isInteraction = true;
        firstHitFace = hit.collider.gameObject;
        firstHitPoint = hit.point;
        StartCoroutine(CaptureInteraction());
    }
    
    private void StopCapturingInteraction()
    {
        isInteraction = false;
    }

    IEnumerator CaptureInteraction()
    {
        GameObject hitFace = firstHitFace;
        
        while (hitFace == firstHitFace && isInteraction)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit) && hit.collider.CompareTag("CubeCell"))
            {
                hitFace = hit.collider.gameObject;
                if(hitFace == firstHitFace)
                    secondHitPoint = hit.point;
            }
            
            yield return null;
        }

        SliceRotationCaptured();
    }

    private void SliceRotationCaptured()
    {
        Vector3 hitUnitPosition = firstHitFace.transform.position;
        Vector3 hitPointPosition = firstHitFace.transform.TransformPoint(firstHitFace.GetComponent<BoxCollider>().center);
        
        interactionDirection = secondHitPoint - firstHitPoint;
        rotationDirection = GetRelativeUnitDirection(interactionDirection , firstHitFace.transform);
        
        if(rotationDirection != Vector3.zero)
            CommandsHistoryManager.PushCommand(new SliceRotationCommand(hitUnitPosition , hitPointPosition, rotationDirection));
    }

    private Vector3 GetRelativeUnitDirection(Vector3 interactionDirection , Transform unitTransform)
    {
        if (Vector3.Dot(interactionDirection, unitTransform.up) > 0.2f)
            return unitTransform.up;
        
        if (Vector3.Dot(interactionDirection, unitTransform.up) < -0.2f)
            return - unitTransform.up;
        
        if (Vector3.Dot(interactionDirection, unitTransform.right) > 0.2f)
            return unitTransform.right;
        
        if (Vector3.Dot(interactionDirection, unitTransform.right) < -0.2f)
            return - unitTransform.right;

        return Vector3.zero;
    }


#if Debugging
    private void OnDrawGizmos()
    {
        if(firstHitFace == null) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(firstHitFace.transform.position , rotationDirection * 5.0f );
        Gizmos.color = Color.green;
        Gizmos.DrawRay(firstHitPoint , (secondHitPoint - firstHitPoint) * 5.0f );
        
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(firstHitFace.transform.position , 0.3f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(firstHitFace.transform.TransformPoint(firstHitFace.GetComponent<BoxCollider>().center) , 0.3f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(firstHitFace.transform.position +  rotationDirection, 0.3f);
      
    }
#endif
}
