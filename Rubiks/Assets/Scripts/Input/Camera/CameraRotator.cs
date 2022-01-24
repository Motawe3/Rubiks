using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSensitivity = 3.0f;
    
    [SerializeField,ReadOnly]
    private bool isRotating;
    
    [SerializeField]
    private Transform cameraAnchor;
    
    [SerializeField]
    private Camera mainCamera;

    private void Start()
    {
        GestureCapture.Instance.OnCapturing += UpdateIsRotating;
    }

    private void OnDestroy()
    {
        GestureCapture.Instance.OnCapturing -= UpdateIsRotating;
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
        float rotationX = GestureCapture.Instance.horizontalAxisMovement * rotationSensitivity;
        float rotationY = GestureCapture.Instance.verticalAxisMovement * rotationSensitivity;
        
        cameraAnchor.RotateAround(Vector3.zero, Vector3.up , rotationX); 
        cameraAnchor.RotateAround(Vector3.zero, mainCamera.transform.right , -rotationY); 
    }
}