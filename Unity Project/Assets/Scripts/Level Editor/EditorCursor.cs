using UnityEngine;
using UnityEngine.Events;

public class EditorCursor : MonoBehaviour
{
    #region Static Accessors
    private static EditorCursor cursorInstance;
    public static EditorCursor Instance
    {
        get
        {
            if (cursorInstance == null)
            {
                cursorInstance = FindObjectOfType<EditorCursor>();
                if (cursorInstance == null)
                {
                    Debug.Log("Uh-Oh! We need at least one <i>EditorCursor</i> script in the scene");
                }
            }
            return cursorInstance;
        }
    }
    public static Vector3 CursorPos
    {
        get { return Instance.cursorPos; }
    }
    public static TileCoord CursorTile
    {
        get { return Instance.cursorTile; }
    }
    #endregion

    public float mousePlaneOffset = 2f;

    Plane groundPlane;

    Vector3 mousePos;
    TileCoord mouseTile;
    TileCoord lastTile;
    Camera cam;

    [System.Serializable]
    public class CursorEvent : UnityEvent<TileCoord> { };
    public CursorEvent OnCursorDown;
    public CursorEvent OnCursorUp;
    public CursorEvent OnCursorMoveHold;

    #region Public Accessors
    public Vector3 cursorPos
    {
        get { return mousePos; }
    }
    public TileCoord cursorTile
    {
        get { return mouseTile; }
    }
    #endregion

    void Start()
    {
        cam = EditorCamera.instance.GetComponent<Camera>();
        groundPlane = new Plane(Vector3.down, mousePlaneOffset);
    }

    void UpdateMousePos()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 pos = ray.GetPoint(distance);
            mousePos = new Vector3(pos.x, 0, pos.z);
            mouseTile = TileCoord.FromVector3(mousePos);
        }
    }
    void UpdateMouseButtons()
    {
        #region Left Click

        if (Input.GetMouseButtonDown(0))
            OnCursorDown.Invoke(cursorTile);

        if (Input.GetMouseButtonUp(0))
            OnCursorUp.Invoke(cursorTile);

        if (Input.GetMouseButton(0) && !lastTile.Equals(mouseTile))
        {
            lastTile = mouseTile;
            OnCursorMoveHold.Invoke(cursorTile);
        }
        #endregion
    }
    void Update()
    {
        UpdateMousePos();
        transform.position = new Vector3(mouseTile.x, mousePlaneOffset, mouseTile.y);
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && MouseInWindow())
            UpdateMouseButtons();
    }
    bool MouseInWindow()
    {
        Vector3 mousePos = Input.mousePosition;
        return mousePos.x >= 0 && mousePos.x < Screen.width && mousePos.y >= 0 && mousePos.y < Screen.height; 
    }
}