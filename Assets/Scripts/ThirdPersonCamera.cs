using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float sensitivity;
    public float cameraZoom;

    public float verticalRotationMin;
    public float verticalRotationMax;

    public Transform playerTransform;

    private float currentRotationVertical;
    private float currentRotationHorizontal;

    private float currentCameraZoom;

    private Transform boomTransform;
    private Transform cameraTransform;

    public LayerMask avoidLayer;

    // Start is called before the first frame update
    void Start()
    {
        boomTransform = transform.GetChild(0);
        cameraTransform = boomTransform.GetChild(0);

        currentCameraZoom = cameraZoom;

        currentRotationHorizontal = transform.localEulerAngles.y;

        currentRotationVertical = boomTransform.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        currentRotationHorizontal += Input.GetAxis("Mouse X") * sensitivity;
        currentRotationVertical -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotationVertical = Mathf.Clamp(currentRotationVertical, verticalRotationMin, verticalRotationMax);

        transform.position = playerTransform.position;


        transform.localEulerAngles = new Vector3(0, currentRotationHorizontal, 0);
        boomTransform.localEulerAngles = new Vector3(currentRotationVertical, 0, 0);

        Vector3 directionToCamera = cameraTransform.position - transform.position;
        directionToCamera.Normalize();

        if(Physics.Raycast(transform.position, directionToCamera, out RaycastHit hit, cameraZoom, avoidLayer))
        {
            currentCameraZoom = hit.distance;
        }
        else
        {
            currentCameraZoom = cameraZoom;
        }

        cameraTransform.localPosition = new Vector3(0, 0, -currentCameraZoom);
    }

    private void OnDrawGizmos()
    {
        boomTransform = transform.GetChild(0);
        cameraTransform = boomTransform.GetChild(0);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, cameraTransform.position);
    }
}
