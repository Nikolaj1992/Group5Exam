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
    
    public IAttack lightAttackScript;
    public IAttack heavyAttackScript;

    private void Awake()
    {
        lightAttackScript = weaponPrefab.transform.GetChild(1).GetComponent<IAttack>();
        heavyAttackScript = weaponPrefab.transform.GetChild(2).GetComponent<IAttack>();
        
        // if (lightAttackScript == null || heavyAttackScript == null)
        // {
        //     Debug.LogError("Child objects must have IAttack components attached.");
        //     return;
        // }
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
            lightAttackScript.ExecuteAttack(muzzle,3);
            // Debug.Log("LIGHT");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            heavyAttackScript.ExecuteAttack(muzzle);
            // Debug.Log("HEAVY");
        }
    }
}
