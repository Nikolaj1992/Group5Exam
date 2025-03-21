using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponInfo))]  // Replace with your actual script class name
public class WeaponEditor : Editor
{
    private SerializedProperty g_disabled;
    private SerializedProperty g_unique;
    
    private SerializedProperty l_baseDamage;
    private SerializedProperty l_damageMultiplier;
    private SerializedProperty l_hasStatusEffectCondition;
    private SerializedProperty l_cooldown;
    private SerializedProperty l_amount;
    
    private SerializedProperty h_baseDamage;
    private SerializedProperty h_damageMultiplier;
    private SerializedProperty h_hasStatusEffectCondition;
    private SerializedProperty h_cooldown;
    private SerializedProperty h_amount;
    
    // TODO: add the missing fields in the future
    // TODO: add status effect dropdown as well
    
    private void OnEnable()
    {
        // Get references to the serialized properties of the fields you want to customize
        g_disabled = serializedObject.FindProperty("g_disabled");
        g_unique = serializedObject.FindProperty("g_unique");
        
        // Light properties
        l_baseDamage = serializedObject.FindProperty("l_baseDamage");
        l_damageMultiplier = serializedObject.FindProperty("l_damageMultiplier");
        l_hasStatusEffectCondition = serializedObject.FindProperty("l_hasStatusEffectCondition");
        l_cooldown = serializedObject.FindProperty("l_cooldown");
        l_amount = serializedObject.FindProperty("l_amount");
        
        // Heavy properties
        h_baseDamage = serializedObject.FindProperty("h_baseDamage");
        h_damageMultiplier = serializedObject.FindProperty("h_damageMultiplier");
        h_hasStatusEffectCondition = serializedObject.FindProperty("h_hasStatusEffectCondition");
        h_cooldown = serializedObject.FindProperty("h_cooldown");
        h_amount = serializedObject.FindProperty("h_amount");
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
        string description = GetDescription((WeaponInfo.Unique)g_unique.enumValueIndex);
        EditorGUILayout.HelpBox(description, MessageType.Info);
        
        EditorGUILayout.Space();
        
        // Light section
        EditorGUILayout.LabelField("Light", EditorStyles.boldLabel);
        
        // Display fields with raw variable names
        EditorGUILayout.PropertyField(l_baseDamage, new GUIContent("l_baseDamage"));
        EditorGUILayout.PropertyField(l_damageMultiplier, new GUIContent("l_damageMultiplier"));
        EditorGUILayout.PropertyField(l_hasStatusEffectCondition, new GUIContent("l_hasStatusEffectCondition"));
        EditorGUILayout.PropertyField(l_cooldown, new GUIContent("l_cooldown"));
        EditorGUILayout.PropertyField(l_amount, new GUIContent("l_amount"));
        
        EditorGUILayout.Space();
        
        // Heavy section
        EditorGUILayout.LabelField("Heavy", EditorStyles.boldLabel);
        
        // Display fields with raw variable names
        EditorGUILayout.PropertyField(h_baseDamage, new GUIContent("h_baseDamage"));
        EditorGUILayout.PropertyField(h_damageMultiplier, new GUIContent("h_damageMultiplier"));
        EditorGUILayout.PropertyField(h_hasStatusEffectCondition, new GUIContent("h_hasStatusEffectCondition"));
        EditorGUILayout.PropertyField(h_cooldown, new GUIContent("h_cooldown"));
        EditorGUILayout.PropertyField(h_amount, new GUIContent("h_amount"));
        
        serializedObject.ApplyModifiedProperties();
    }
    
    private string GetDescription(WeaponInfo.Unique unique)
    {
        switch (unique)
        {
            case WeaponInfo.Unique.None:
                return "No unique ability.";
            case WeaponInfo.Unique.Unpredictable:
                return "20% chance for 1 out of 3 effects.";
            case WeaponInfo.Unique.Lingering:
                return "Light attacks applies a damage over time effect, dealing 15% of the original 3 times.";
            default:
                return "Unknown unique ability.";
        }
    }
}