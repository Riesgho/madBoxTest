using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponView))]
public class WeaponStatsInspector : Editor
{
    private SerializedProperty scriptableObjectProperty;

    private void OnEnable()
    {
        scriptableObjectProperty = serializedObject.FindProperty("weaponInfo");
    }

     public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawHeader();
        // Display default inspector fields
        DrawDefaultInspector();

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawHeader()
    {
        EditorGUILayout.LabelField("Weapon Stats", EditorStyles.boldLabel);

        if (scriptableObjectProperty.objectReferenceValue != null)
        {
            SerializedObject scriptableObject = new SerializedObject(scriptableObjectProperty.objectReferenceValue);
            SerializedProperty scriptableObjectField = scriptableObject.GetIterator();
            bool enterChildren = true;

            while (scriptableObjectField.NextVisible(enterChildren))
            {
                EditorGUILayout.PropertyField(scriptableObjectField, true);
                enterChildren = false;
            }

            scriptableObject.ApplyModifiedProperties();
        }
        else
        {
            EditorGUILayout.HelpBox("No ScriptableObject assigned.", MessageType.Info);
        }
    }
}
