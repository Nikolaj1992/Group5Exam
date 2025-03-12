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

    public WeaponInfo weaponInfo;
    private IAttack lightAttackScript;
    private IAttack heavyAttackScript;
    
    // TODO: start on the armor, possible rename file or create a new one to handle armor

    private void Awake()
    {
        weaponInfo = weaponPrefab.transform.GetChild(0).GetComponent<WeaponInfo>();
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
            lightAttackScript.ExecuteAttack(muzzle,weaponInfo.l_amount);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            heavyAttackScript.ExecuteAttack(muzzle, weaponInfo.h_amount);
        }
    }
    
    public static void ApplyEffectsToTarget(Vector3 knockbackDirection, float knockbackForce, GameObject target)
    {
            if (target != null && target.layer == LayerMask.NameToLayer("Enemy") && target.GetComponent<Rigidbody>() != null)
            {
                EnemyEffectHandler EEH = target.GetComponent<EnemyEffectHandler>();
                if (!EEH) return;
                EEH.ApplyKnockback(knockbackDirection, knockbackForce);
                EEH.DamageFlash();
            }
    }
    
    public static void ApplyEffectsToTargets(Vector3 knockbackDirection, float knockbackForce, List<GameObject> targets)
    {
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
