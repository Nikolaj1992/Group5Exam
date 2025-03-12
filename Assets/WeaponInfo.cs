using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    public enum Unique {None, Unpredictable, Fulfilling_Fire} 
    
    // general
    // general: variables
    [HideInInspector] public bool g_disabled = false; // used to disable the weapon, can be used while interact with stuff and the ui
    [HideInInspector] public Unique g_unique = Unique.None;
    
    // light
    [HideInInspector] public float l_baseDamage;
    [HideInInspector] public float l_damageMultiplier = 1;
    // [HideInInspector] public ENUM l_damageType;
    // [HideInInspector] public List<ENUM> l_statusEffects; // is a list in case specific weapons will have multiple
    [HideInInspector] public bool l_hasStatusEffectCondition = false;
    [HideInInspector] public float l_cooldown;
    [HideInInspector] public int l_amount = 1;
    
    // heavy
    [HideInInspector] public float h_baseDamage;
    [HideInInspector] public float h_damageMultiplier = 1;
    // [HideInInspector] public ENUM h_damageType;
    // [HideInInspector] public List<ENUM> h_statusEffects; // is a list in case specific weapons will have multiple
    [HideInInspector] public bool h_hasStatusEffectCondition = false;
    [HideInInspector] public float h_cooldown;
    [HideInInspector] public int h_amount = 1;
}