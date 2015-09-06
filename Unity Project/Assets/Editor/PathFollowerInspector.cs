using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(PathFollower)), CanEditMultipleObjects]
public class PathFollowerInspector : Editor
{
    ReorderableList list;
    PathFollower pf;
    float lWidth;
    GUIStyle labelStyle;
    void OnEnable()
    {
        lWidth = EditorGUIUtility.labelWidth;
        pf = target as PathFollower;

        list = new ReorderableList(serializedObject, serializedObject.FindProperty("path"), true, true, true, true);

        // Tell the list how to render it's contents
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUIUtility.labelWidth = 14f;

            rect.width /= 3;
            EditorGUI.LabelField(rect, string.Format("Point {0}", index));
            rect.x += rect.width;
            EditorGUI.PropertyField(rect, element.FindPropertyRelative("x"), new GUIContent("X"));
            rect.x += rect.width;
            EditorGUI.PropertyField(rect, element.FindPropertyRelative("y"), new GUIContent("Y"));
            EditorGUIUtility.labelWidth = lWidth;
        };
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Path Points");
        };
        list.onSelectCallback = (ReorderableList l) =>
        {
            SceneView.RepaintAll();
        };
        labelStyle = new GUIStyle();
        labelStyle.fontSize = 16;
        labelStyle.fontStyle = FontStyle.Bold;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUIUtility.labelWidth = 50f;

        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("speed"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("heightOffset"), new GUIContent("Height"));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("forward"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("loop"));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("faceForward"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("smooth"));
        GUILayout.EndHorizontal();

        EditorGUIUtility.labelWidth = lWidth;
        EditorGUILayout.Space();

        list.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }
    void OnSceneGUI()
    {
        Handles.color = Color.green;
        Vector3[] points = new Vector3[pf.path.Length];
        for (int i = 0; i < pf.path.Length; i++)
        {
            Vector3 pos = pf.path[i].ToVector3(pf.heightOffset);
            if (i == list.index)
            {
                Vector3 newPoint = Vector3.one * .5f + Handles.DoPositionHandle(pos, Quaternion.identity);
                if (pos != newPoint)
                {
                    Undo.RecordObject(pf, "Move Path Point");
                    pf.path[i] = newPoint;
                }
            }
            else
            {
                if (Handles.Button(pos, Quaternion.identity, HandleUtility.GetHandleSize(pos) * .1f, HandleUtility.GetHandleSize(pos) * .1f, Handles.DotCap))
                {
                    Debug.Log("Selected " + i);
                    list.index = i;
                }
            }
            Handles.Label(pos, string.Format("Position {0}", i), labelStyle);
            points[i] = pf.path[i].ToVector3(pf.heightOffset);
        }
        Handles.DrawAAPolyLine(3f, points);
        if (pf.loop)
        {
            Handles.color = Color.blue;
            Handles.DrawDottedLine(points[0], points[points.Length - 1], 15f);
        }
    }
}