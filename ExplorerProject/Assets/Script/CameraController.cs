using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController _instance;
    public static CameraController Instance { get { return _instance; } }

    public Transform targetObj;
    public LayerMask aimMask;

    [HideInInspector] public Vector3 aimLocation;
    [HideInInspector] public float aimDistance = 100.0f;

    [HideInInspector] public float xSpeed = 10f;
    [HideInInspector] public float ySpeed = 10f;

    [HideInInspector] public float yMin = -90f;
    [HideInInspector] public float yMax = 90f;

    [HideInInspector] public bool InvertY = false;

    [HideInInspector] public float cameraDistance = 2f;
    float x = 0f;
    float y = 0f;

    float rotX;
    float rotY;

    public Vector3 anchorOffset;
    public Vector3 cameraOffset;

    public float cameraSmoothing = 0.125f;
    public float rotationSmoothing = 3f;

    Camera cam;

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
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (targetObj == null)
            return;

        x += Input.GetAxis("Mouse X") * xSpeed;
        y -= Input.GetAxis("Mouse Y") * ySpeed;

        y = Mathf.Clamp(y, yMin, yMax);

        rotX = Mathf.Lerp(rotX, x, Time.deltaTime * rotationSmoothing);
        rotY = Mathf.Lerp(rotY, y, Time.deltaTime * rotationSmoothing);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;

        transform.position = Vector3.SmoothDamp(transform.position, targetObj.position + anchorOffset, ref refVector, cameraSmoothing);

        cam.transform.localPosition = cameraOffset;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, aimDistance, aimMask))
        {
            aimLocation = hit.point;
        }
        else
        {
            aimLocation = cam.transform.forward * aimDistance;
        }
    }
}
