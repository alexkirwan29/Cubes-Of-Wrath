using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LevelManager))]
public class EditorManager : MonoBehaviour
{
    public string[] tools =
    {
        "Nothing",
        "Wall",
        "Destructable Wall"
    };
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    Vector3 GetCursorPos()
    {
        Vector3 mousePos = new Vector3();
        return mousePos;
    }
}