//#define Debugging
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CubeInteractionCapture : MonoBehaviour
{
    #region Singleton

    private static CubeInteractionCapture _instance;

    public static CubeInteractionCapture Instance
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
    
    [SerializeField,ReadOnly]
    private bool isRotating;
    
    private Camera mainCamera;
    
    private GameObject firstHitFace;
    private Vector3 firstHitPoint;
    
    private GameObject secondHitFace;
    private Vector3 secondHitPoint;

    private Vector3 interactionDirection;
    private Vector3 rotationDirection;

    public Action<Vector3 , Vector3, Vector3> OnCubeRotationCaptured;
    
    private void Start()
    {
        mainCamera = Camera.main;
        GestureCapture.Instance.OnCapturing += UpdateIsRotating;
    }

    private void OnDestroy()
    {
        GestureCapture.Instance.OnCapturing -= UpdateIsRotating;
    }

    private void UpdateIsRotating(bool isCapturing)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (isCapturing && Physics.Raycast(ray, out var hit) && hit.collider.CompareTag("CubeCell"))
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
        isRotating = true;
        firstHitFace = hit.collider.gameObject;
        firstHitPoint = hit.point;
        StartCoroutine(CaptureInteraction());
    }
    
    private void StopCapturingInteraction()
    {
        isRotating = false;
    }

    IEnumerator CaptureInteraction()
    {
        GameObject secondHitUnit = firstHitFace;
        
        while (secondHitUnit == firstHitFace && isRotating)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit) && hit.collider.CompareTag("CubeCell"))
            {
                secondHitUnit = hit.collider.gameObject;
                secondHitPoint = hit.point;
            }
            
            yield return null;
        }

        SliceRotationCaptured();
    }

    public void SliceRotationCaptured()
    {
        Vector3 hitUnitPosition = firstHitFace.transform.position;
        Vector3 hitFacePosition = firstHitFace.transform.TransformPoint(firstHitFace.GetComponent<BoxCollider>().center);
        
        interactionDirection = secondHitPoint - firstHitPoint;
        rotationDirection = GetRelativeUnitDirection(interactionDirection , firstHitFace.transform);
        
        OnCubeRotationCaptured?.Invoke(hitUnitPosition , hitFacePosition, rotationDirection);
    }

    private Vector3 GetRelativeUnitDirection(Vector3 interactionDirection , Transform unitTransform)
    {
        if (Vector3.Dot(interactionDirection, unitTransform.up) > 0.3f)
            return unitTransform.up;
        
        if (Vector3.Dot(interactionDirection, unitTransform.up) < -0.3f)
            return - unitTransform.up;
        
        if (Vector3.Dot(interactionDirection, unitTransform.right) > 0.3f)
            return unitTransform.right;
        
        if (Vector3.Dot(interactionDirection, unitTransform.right) < -0.3f)
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
