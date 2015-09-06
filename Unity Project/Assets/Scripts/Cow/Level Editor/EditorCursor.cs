using UnityEngine;
using UnityEngine.Events;
using Cow.Editor;

namespace Cow.Editor
{
    [RequireComponent(typeof(EditorCursorGraphic))]
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
        [HideInInspector]
        public EditorCursorGraphic graphic;

        Plane groundPlane;

        Vector3 mousePos;
        TileCoord mouseTile;
        TileCoord lastTile;
        Camera cam;

        bool isOverScene = true;
        bool validCursor = false;

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
        void Awake()
        {
            graphic = GetComponent<EditorCursorGraphic>();
        }
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
        void UpdateMouseButtons(bool mouseUsable)
        {
            #region Left Click

            if (Input.GetMouseButtonDown(0) && mouseUsable)
            {
                OnCursorDown.Invoke(cursorTile);
                validCursor = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnCursorUp.Invoke(cursorTile);
                validCursor = false;
            }

            if (Input.GetMouseButton(0) && !lastTile.Equals(mouseTile) && mouseUsable && validCursor)
            {
                lastTile = mouseTile;
                OnCursorMoveHold.Invoke(cursorTile);
            }
            #endregion
        }
        void Update()
        {
            isOverScene = !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && MouseInWindow();
            if (validCursor && !isOverScene)
                validCursor = false;
            UpdateMousePos();
            transform.position = new Vector3(mouseTile.x, mousePlaneOffset, mouseTile.y);
            UpdateMouseButtons(isOverScene);
        }
        bool MouseInWindow()
        {
            Vector3 mousePos = Input.mousePosition;
            return mousePos.x >= 0 && mousePos.x < Screen.width && mousePos.y >= 0 && mousePos.y < Screen.height;
        }
    }
}