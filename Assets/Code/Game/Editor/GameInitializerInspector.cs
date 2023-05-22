namespace Code.Game.Editor
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(GameInitializer))]
    public class GameInitializerInspector : Editor
    {
        private SerializedProperty gameConfiguration;

        private void OnEnable()
        {
            gameConfiguration = serializedObject.FindProperty("gameConfiguration");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawGameConfig();

            DrawDefaultInspector();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawGameConfig()
        {
            EditorGUILayout.PropertyField(gameConfiguration);

            if (gameConfiguration.objectReferenceValue != null)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;

                SerializedObject configObject = new SerializedObject(gameConfiguration.objectReferenceValue);
                SerializedProperty amountOfMobs = configObject.FindProperty("amountOfMobs");
                SerializedProperty weaponProbs = configObject.FindProperty("weaponProbs");

                EditorGUILayout.PropertyField(amountOfMobs);
                EditorGUILayout.PropertyField(weaponProbs, true);
                
                configObject.ApplyModifiedProperties();
              
                
                EditorGUILayout.Space();
                if (GUILayout.Button("Re-Roll Weapons"))
                {
                    GameInitializer gameInitializer = (GameInitializer)target;
                    gameInitializer.ReRollWeapon();
                }

            }
        }
    }
}