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
    public void SaveButton()
    {
        editor.level.SaveToFile("test");
    }
    public void OpenButton()
    {
        editor.level.LoadFromFile("test");
    }
    public void PlayLevel()
    {
        throw new System.NotImplementedException("LOL, that part has not been added yet");
    }
}