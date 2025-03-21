using UnityEngine;

public class ArmorInfo : MonoBehaviour
{
    public enum Unique {None, Rejecting, Bored_Fighter}
    // TODO: add methods to handle each Unique in the player attack script
    
    // general
    // general: variables
    [HideInInspector] public bool g_disabled = false; // used to disable the weapon, can be used while interact with stuff and the ui
    [HideInInspector] public Unique g_unique = Unique.None;
    [HideInInspector] public float g_healthIncrease = 0;
    [HideInInspector] [Range(0, 100)] public float g_damageReduction = 0;
}
