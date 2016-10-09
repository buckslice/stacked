using UnityEngine;
using UnityEngine.Assertions;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(AllBoolStat))]
[CustomPropertyDrawer(typeof(AnyBoolStat))]
[CustomPropertyDrawer(typeof(MultiplierFloatStat))]
[CustomPropertyDrawer(typeof(MultiplierIntStat))]
[CustomPropertyDrawer(typeof(AdditiveFloatStat))]
[CustomPropertyDrawer(typeof(AdditiveIntStat))]
class StatDrawer : PropertyDrawer
{

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(position, property.FindPropertyRelative("baseValue"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}