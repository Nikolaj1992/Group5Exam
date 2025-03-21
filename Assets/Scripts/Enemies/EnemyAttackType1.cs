using System;
using UnityEngine;

public class EnemyAttackType1 : MonoBehaviour
{
    public float damage;
    private float lastAttackTime = 0f;
    public float attackInterval = 1.5f; // Time between attacks
    
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastAttackTime >= attackInterval)
            {
                lastAttackTime = Time.time; // Update last attack time
                other.GetComponent<PlayerArmor>().DamagePlayer(damage, gameObject);
            }
        }
    }
}
