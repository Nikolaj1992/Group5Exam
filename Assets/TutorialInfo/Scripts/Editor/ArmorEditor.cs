using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ArmorInfo))]  // Replace with your actual script class name
public class ArmorEditor : Editor
{
    private SerializedProperty g_disabled;
    private SerializedProperty g_unique;
    
    private SerializedProperty g_healthIncrease;
    private SerializedProperty g_damageReduction;
    
    // TODO: add the missing fields in the future
    // TODO: add status effect dropdown as well
    
    private void OnEnable()
    {
        // Get references to the serialized properties of the fields you want to customize
        g_disabled = serializedObject.FindProperty("g_disabled");
        g_unique = serializedObject.FindProperty("g_unique");
        
        g_healthIncrease = serializedObject.FindProperty("g_healthIncrease");
        g_damageReduction = serializedObject.FindProperty("g_damageReduction");
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        // General section
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(g_disabled, new GUIContent("g_disabled"));
        
        // Display the g_unique enum
        EditorGUILayout.PropertyField(g_unique, new GUIContent("g_unique"));
        string description = GetDescription((ArmorInfo.Unique)g_unique.enumValueIndex);
        EditorGUILayout.HelpBox(description, MessageType.Info);
        
        EditorGUILayout.Space();
        
        // Stat section
        EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
        
        // Display fields with raw variable names
        EditorGUILayout.PropertyField(g_healthIncrease, new GUIContent("g_healthIncrease"));
        EditorGUILayout.PropertyField(g_damageReduction, new GUIContent("g_damageReduction"));
        
        serializedObject.ApplyModifiedProperties();
    }
    
    private string GetDescription(ArmorInfo.Unique unique)
    {
        switch (unique)
        {
            case ArmorInfo.Unique.None:
                return "No unique ability.";
            case ArmorInfo.Unique.Rejecting:
                return "Enemies receive 10% of the damage they deal to you.";
            case ArmorInfo.Unique.Bored_Fighter:
                return "Decreases the damage received from a specific enemy each time they hit you.";
            default:
                return "Unknown unique ability.";
        }
    }
}