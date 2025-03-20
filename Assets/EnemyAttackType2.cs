using System;
using UnityEngine;

public class EnemyAttackType2 : MonoBehaviour
{
    public float damage;
    private float lastAttackTime = 0f;
    private float attackInterval = 3f; // Time between attacks
    private Transform muzzle;
    private IAttack attackScript;
    private bool shouldAttack = false;

    void Awake()
    {
        attackScript = gameObject.GetComponent<IAttack>();
        attackScript.EquipOnEnemy();
        muzzle = transform.Find("AttackMuzzle").transform;
    }

    void Update()
    {
        if (shouldAttack)
        {
            if (Time.time - lastAttackTime >= attackInterval)
            {
                Debug.Log("AA: " + muzzle.position);
                lastAttackTime = Time.time; // Update last attack time
                // other.GetComponent<PlayerArmor>().DamagePlayer(damage, gameObject);
                Debug.Log(gameObject.name + " - dealing " + damage + " damage to Player");
                Debug.Log("A: " + muzzle.position);
                attackScript.ExecuteAttack(muzzle, 1);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldAttack = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldAttack = false;
        }
    }
}
