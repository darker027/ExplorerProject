using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController _instance;
    public static CameraController Instance { get { return _instance; } }

    public Transform followObject;
    public LayerMask aimMask;

    [HideInInspector] public Vector3 aimLocation;
    [HideInInspector] public float aimDistance = 100.0f;

    [HideInInspector] public float xSpeed = 10f;
    [HideInInspector] public float ySpeed = 10f;

    [HideInInspector] public float yMin = -90f;
    [HideInInspector] public float yMax = 90f;

    [HideInInspector] public bool InvertY = false;

    [HideInInspector] public float cameraDistance = 2f;
    float xAxis = 0f;
    float yAxis = 0f;

    float rotateX;
    float rotateY;

    private float wallDistance;

    public Vector3 anchorOffset;
    public Vector3 cameraOffset;
    private Vector3 Offset;
    private Vector3 updateOffset;

    public float cameraSmoothing = 0.125f;
    public float rotationSmoothing = 3f;

    Camera mainCamera;

    Vector3 refVector = Vector3.zero;

    void Awake()
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

    void Start()
    {
        mainCamera = gameObject.GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        updateOffset = cameraOffset;
    }


    void Update()
    {
        if (followObject == null)
            return;

        xAxis += Input.GetAxis("Mouse X") * xSpeed;
        yAxis -= Input.GetAxis("Mouse Y") * ySpeed;

        yAxis = Mathf.Clamp(yAxis, yMin, yMax);

        rotateX = Mathf.Lerp(rotateX, xAxis, Time.deltaTime * rotationSmoothing);
        rotateY = Mathf.Lerp(rotateY, yAxis, Time.deltaTime * rotationSmoothing);

        Quaternion rotation = Quaternion.Euler(yAxis, xAxis, 0);
        transform.rotation = rotation;

        transform.position = Vector3.SmoothDamp(transform.position, followObject.position + anchorOffset, ref refVector, cameraSmoothing);

        // mainCamera.transform.localPosition = cameraOffset;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out RaycastHit hit, aimDistance, aimMask))
        {
            aimLocation = hit.point;
        }
        else
        {
            aimLocation = mainCamera.transform.forward * aimDistance;
        }

        if (Physics.Raycast(mainCamera.transform.position, -this.gameObject.transform.forward, out RaycastHit backRayHit))
        {
            wallDistance = Vector3.Distance(mainCamera.transform.position, backRayHit.point);

            if (wallDistance < 1 && updateOffset.z < -1)
            {
                updateOffset.z += 10 * Time.deltaTime;
            }
            else if (updateOffset.z >= -1)
            {
                updateOffset.z = -1;
            }

            if (wallDistance > 1 && updateOffset.z > -5)
            {
                updateOffset.z -= 10 * Time.deltaTime;
            }
            else if (updateOffset.z <= -5)
            {
                updateOffset.z = -5;
            }
        }

        mainCamera.transform.localPosition = updateOffset;

    }
}
