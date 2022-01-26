using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraRotator : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    
    [SerializeField]
    private float rotationSensitivity = 3.0f;
    
    [SerializeField,ReadOnly]
    private bool isRotating;
    
    [SerializeField]
    private Transform cameraAnchor;

    private void Start()
    {
        GestureManager.Instance.OnCapturing += UpdateIsRotating;
    }

    private void OnDestroy()
    {
        GestureManager.Instance.OnCapturing -= UpdateIsRotating;
    }

    private void UpdateIsRotating(bool isCapturing)
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (isCapturing && !Physics.Raycast(ray, out var hit))
        {
            isRotating = true;
        }
        else
        {
            isRotating = false;
        }
    }

    private void Update()
    {
        if (isRotating)
            RotateCameraAnchor();
    }

    private void RotateCameraAnchor()
    {
        float rotationX = GestureManager.Instance.horizontalAxisMovement * rotationSensitivity;
        float rotationY = GestureManager.Instance.verticalAxisMovement * rotationSensitivity;
        
        cameraAnchor.RotateAround(Vector3.zero, Vector3.up , rotationX); 
        cameraAnchor.RotateAround(Vector3.zero, mainCamera.transform.right , -rotationY); 
    }
}