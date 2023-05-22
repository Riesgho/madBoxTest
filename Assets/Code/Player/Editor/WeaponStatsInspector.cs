using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponView))]
public class WeaponStatsInspector : Editor
{
    private SerializedProperty weaponConfig;

    private void OnEnable()
    {
        weaponConfig = serializedObject.FindProperty("weaponInfo");
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

        if (weaponConfig.objectReferenceValue != null)
        {
            SerializedObject weaponInfo = new SerializedObject(weaponConfig.objectReferenceValue);
            SerializedProperty fields = weaponInfo.GetIterator();
            bool enterChildren = true;

            while (fields.NextVisible(enterChildren))
            {
                EditorGUILayout.PropertyField(fields, true);
                enterChildren = false;
            }

            weaponInfo.ApplyModifiedProperties();
        }
        else
        {
            EditorGUILayout.HelpBox("No ScriptableObject assigned.", MessageType.Info);
        }
    }
}
