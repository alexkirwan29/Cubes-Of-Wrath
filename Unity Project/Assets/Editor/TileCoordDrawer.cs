using UnityEngine;
using UnityEditor;
using System.Collections;

using Cow;

[CustomPropertyDrawer(typeof(TileCoord))]
public class TileCoordDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Let unity know where out custom property starts at.
        label = EditorGUI.BeginProperty(position, label, property);

        // Get the position of where we can put the content of this property and
        // create a label at the same time.
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);

        // half the width of the content area so we can fit two items inside.
        contentPosition.width /= 2;

        // force the width of the label to be smaller and don't let unity
        // automatically add indents.
        EditorGUIUtility.labelWidth = 14f;
        EditorGUI.indentLevel = 0;

        // Draw the x variable and set it's label to X.
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("x"),new GUIContent("X"));

        // Move the contentPosition over by the width for the second variable
        contentPosition.x += contentPosition.width;

        // Draw the y variable and set it's label to Y.
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("y"), new GUIContent("Y"));

        // Let unity know that this is the end of this custom property.
        EditorGUI.EndProperty();
    }
}
