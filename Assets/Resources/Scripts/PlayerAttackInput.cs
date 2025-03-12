using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAttackInput : MonoBehaviour
{
    // public event Action StrafeStart;
    // public event Action StrafeEnd;
    // public event Action Attack;

    public GameObject weaponPrefab;
    public Transform muzzle;
    
    // TODO: make variables for projectileAmount, cooldown and so on for both light and heavy
    // light: script and variables
    [SerializeField] private IAttack lightAttackScript;
    private float l_baseDamage;
    private float l_damageMultiplier = 1;
    // private ENUM l_damageType;
    // private List<ENUM> l_statusEffects;  // need the statusEffect scripts, and is a list in case specific weapons will have multiple
    private bool l_hasStatusEffectCondition;
    private float l_cooldown;
    private int l_amount = 1;
    // private ENUM l_unique // yet to be made
    
    // heavy: script and variables
    [SerializeField] private IAttack heavyAttackScript;
    private float h_baseDamage;
    private float h_damageMultiplier = 1;
    // private ENUM h_damageType;
    // private List<ENUM> h_statusEffects;  // need the statusEffect scripts, and is a list in case specific weapons will have multiple
    private bool h_hasStatusEffectCondition;
    private float h_cooldown;
    private int h_amount = 1;
    // private ENUM h_unique // yet to be made
    
    // TODO: start on the armor

    private void Awake()
    {
        lightAttackScript = weaponPrefab.transform.GetChild(1).GetComponent<IAttack>();
        heavyAttackScript = weaponPrefab.transform.GetChild(2).GetComponent<IAttack>();
    }

    void Update()
    {
        // if (Input.GetMouseButtonDown(1))
        // {
        //     StrafeStart?.Invoke();
        //     Debug.Log("Strafe Start");
        // }
        //
        // if (Input.GetMouseButtonUp(1))
        // {
        //     StrafeEnd?.Invoke();
        //     Debug.Log("Strafe End");
        // }
        //
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Attack?.Invoke();
        //     Debug.Log("Attack");
        // }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            lightAttackScript.ExecuteAttack(muzzle,l_amount);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            heavyAttackScript.ExecuteAttack(muzzle, h_amount);
        }
    }
    
    public static void ApplyEffectsToTargets(Vector3 knockbackDirection, float knockbackForce, List<GameObject> targets)
    {
        Debug.Log("METHOD");
        foreach (GameObject target in targets)
        {
            if (target != null && target.layer == LayerMask.NameToLayer("Enemy") && target.GetComponent<Rigidbody>() != null)
            {
                EnemyEffectHandler EEH = target.GetComponent<EnemyEffectHandler>();
                if (!EEH) return;
                EEH.ApplyKnockback(knockbackDirection, knockbackForce);
                EEH.DamageFlash();
            }
        }
    }
}
