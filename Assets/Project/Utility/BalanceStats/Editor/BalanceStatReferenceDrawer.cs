using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(BalanceStatReference))]
class BalanceStatReferenceDrawer : PropertyDrawer {

    const float scriptFieldSize = 180f;

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var targetRect = new Rect(position.x, position.y, scriptFieldSize, position.height);
        var functionRect = new Rect(position.x + scriptFieldSize + 5, position.y, position.width - (scriptFieldSize + 5), position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(targetRect, property.FindPropertyRelative("target"), GUIContent.none);
        EditorGUI.PropertyField(functionRect, property.FindPropertyRelative("function"), GUIContent.none);
        
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}