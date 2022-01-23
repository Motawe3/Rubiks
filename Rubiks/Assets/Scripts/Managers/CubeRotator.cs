using Sirenix.OdinInspector;
using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSensitivity = 3.0f;
    [SerializeField,Required]
    private GestureCapture gestureCapture;
    [SerializeField,ReadOnly]
    private bool isRotating;
    
    private Transform cubeAnchor;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        cubeAnchor = GameObject.FindGameObjectWithTag("CubeAnchor").transform;
        gestureCapture.OnCapturing += UpdateIsRotating;
    }

    private void OnDestroy()
    {
        gestureCapture.OnCapturing -= UpdateIsRotating;
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
            RotateCubeAnchor();
    }

    private void RotateCubeAnchor()
    {
        float rotationX = gestureCapture.horizontalAxisMovement * rotationSensitivity * Mathf.Deg2Rad;
        float rotationY = gestureCapture.verticalAxisMovement * rotationSensitivity * Mathf.Deg2Rad;
        
        cubeAnchor.RotateAround(Vector3.up , -rotationX); 
        cubeAnchor.RotateAround(mainCamera.transform.right , rotationY); 
    }
}