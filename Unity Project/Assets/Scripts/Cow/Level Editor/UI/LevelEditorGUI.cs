using UnityEngine;
using System.Collections;
using Cow.Editor;
using UnityEngine.UI;
public class LevelEditorGUI : MonoBehaviour
{
    EditorManager editor;

    void Awake()
    {
        editor = FindObjectOfType<EditorManager>();
        if (editor == null)
            Debug.LogError("Cannot find an EditorManager");
    }

    public void SetBrush(float value)
    {
        editor.SelectedBrush = Mathf.FloorToInt(value);
    }

    public void SetBrushSize(float value)
    {
        editor.BrushSize = Mathf.FloorToInt(value);
    }
}