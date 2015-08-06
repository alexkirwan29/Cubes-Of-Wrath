using UnityEngine;
using System.Collections;

public class EditorCamera : MonoBehaviour
{
    [SerializeField]
    float panSpeed = 15f, rotateSpeed = 25f, zoomSpeed = 5f;

    [Range(0, 5)]
    public float orbitHeight = 2f;
    public float mouseSensitivity = 1f;
    public float minZoom = -5f, maxZoom = -150f;
    public float maxVertAngle = 80, minVertAngle = -20;
    public bool UserInput = true;

    Vector3 orbitOrigin = Vector3.zero;
    Vector3 orbitAngle = Vector3.zero;
    float zoom;

    void Awake()
    {
        // Set the zoom to half the zoom value and the orbit angle to isometric.
        ZoomDecimal = 0.5f;
        orbitAngle = new Vector3(30f, 45f, 0);
        orbitOrigin.y = orbitHeight;
    }

    #region Public Accessors
    // Public Accessors for camera controls so other scripts can control the camera
    public Vector3 OrbitOrigin
    {
        get { return orbitOrigin; }
        set { orbitOrigin = new Vector3(value.x, 0, value.z); }
    }
    public Vector3 OrbitAngle
    {
        get { return orbitAngle; }
        set { orbitAngle = new Vector3(Mathf.Clamp(value.x,minVertAngle,maxVertAngle), value.y, orbitAngle.z);}
    }
    public float Zoom
    {
        get { return zoom; }
        set { zoom = Mathf.Clamp(value, minZoom, maxZoom); }
    }
    public float ZoomDecimal
    {
        get { return (zoom - minZoom) / maxZoom; }
        set { zoom = minZoom + (Mathf.Clamp01(value) * (maxZoom - minZoom)); }
    }
    #endregion

    void Update()
    {
        // We only want the use to be able to move the camera when UserInput is
        // true so if it's false then just return out of the update method.
        if (!UserInput)
            return;
        Vector3 userInput = Quaternion.Euler(0,OrbitAngle.y,0)* new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        userInput.y = 0;

        if (userInput.sqrMagnitude > 1)
            userInput.Normalize();

        OrbitOrigin += userInput * panSpeed * Time.deltaTime * Zoom;
        
        if(Input.GetMouseButton(0))
        {
            OrbitAngle += new Vector3(-Input.GetAxis("Mouse Y") * rotateSpeed * mouseSensitivity,Input.GetAxis("Mouse X") * rotateSpeed * mouseSensitivity);
        }
        Zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * (0.025f + ZoomDecimal);
    }
    void LateUpdate()
    {
        transform.position = new Vector3(orbitOrigin.x,orbitHeight,orbitOrigin.z) + Quaternion.Euler(orbitAngle) * Vector3.forward * -zoom;
        transform.LookAt(orbitOrigin);
    }
}