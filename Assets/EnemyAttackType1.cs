using System;
using UnityEngine;

public class EnemyAttackType1 : MonoBehaviour
{
    public float damage;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerArmor>().DamagePlayer(damage, gameObject);
        }
    }
}
