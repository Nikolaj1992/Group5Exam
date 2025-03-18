using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAttack : MonoBehaviour
{
    // public event Action StrafeStart;
    // public event Action StrafeEnd;
    // public event Action Attack;

    public GameObject weaponPrefab;
    public Transform muzzle;

    private WeaponInfo weaponInfo;
    private IAttack lightAttackScript;
    private IAttack heavyAttackScript;

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

    public void HandleUniqueAndDamageTarget(bool lightAttack, GameObject target)
    {
        if (target == null) return;
        HealthHandler t_HH = target.GetComponent<HealthHandler>();
        if (t_HH == null) return;
        StatusEffectHandler t_SEH = target.GetComponent<StatusEffectHandler>();
        if (t_SEH == null) return;
        float damage = lightAttack ? weaponInfo.l_baseDamage : weaponInfo.h_baseDamage;
        
        switch (weaponInfo.g_unique)
        {
            case WeaponInfo.Unique.Unpredictable:
                Debug.Log("Q-U-WEAPON: " + damage);
                break;
            case WeaponInfo.Unique.Lingering:
                Debug.Log("Q-L-WEAPON: " + damage);
                if (lightAttack)
                { 
                    t_HH.DealDamage(damage, HealthHandler.DamageType.Impact); 
                    t_HH.DealDamageOverTime(damage/10, HealthHandler.DamageType.Impact, 3);
                }
                else
                {
                    t_HH.DealDamage(damage, HealthHandler.DamageType.Elemental);
                    t_SEH.ApplyStatusEffect(StatusEffect.premadeStatusEffects["aflame"]);
                }
                break;
        }
    }
    
    public void ApplyEffectsToTarget(Vector3 knockbackDirection, float knockbackForce, GameObject target)
    {
            if (target != null && target.layer == LayerMask.NameToLayer("Enemy") && target.GetComponent<Rigidbody>() != null)
            {
                EnemyEffectHandler EEH = target.GetComponent<EnemyEffectHandler>();
                if (!EEH) return;
                EEH.ApplyKnockback(knockbackDirection, knockbackForce);
                EEH.DamageFlash();
            }
    }
    
    public void ApplyEffectsToTargets(Vector3 knockbackDirection, float knockbackForce, List<GameObject> targets)
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
