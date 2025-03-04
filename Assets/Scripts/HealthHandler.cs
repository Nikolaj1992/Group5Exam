using System.Collections;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    [Range(0.0f, 500.0f)] public float health = 100; // added a slider in the inspector, 100 is default max hp
    [HideInInspector] public bool alive;
    public enum DamageType {Piercing, Impact, Elemental} // there is no resistance towards piercing
    [SerializeField] private float generalDamageResistance; // given as 20 for a 20% damage reduction, hence the math in "dealDamage"
    [SerializeField] private float impactResistance;
    [SerializeField] private float elementalResistance;
    private StatusEffectHandler statusEffectHandler;
    
    void Awake()
    {
        alive = true;
        statusEffectHandler = gameObject.GetComponent<StatusEffectHandler>();
    }
    
    void Update()
    {
        if (health <= 0 && alive)
        {
            alive = false;
            health = 0;
            Debug.Log(gameObject.name + " has died.");
        }
    }

    public void DealDamage(float damage, DamageType damageType)
    {
        if (!alive)
        {
            Debug.Log(gameObject.name + " is already dead.");
            return;
        }
        float damageToDeal = 0;
        switch (damageType)
        {
            case DamageType.Piercing:
                damageToDeal = damage;
                break;
            case DamageType.Impact:
                damageToDeal = impactResistance > 0 ? damage * (1 - (impactResistance / 100)) : damage;
                break;
            case DamageType.Elemental:
                damageToDeal = elementalResistance > 0 ? damage * (1 - (elementalResistance / 100)) : damage;
                break;
        }
        damageToDeal = generalDamageResistance > 0 ? damageToDeal * (1 - (generalDamageResistance / 100)) : damageToDeal;
        health -= damageToDeal;
        Debug.Log(gameObject.name + " took " + damageToDeal + " damage");
    }

    public void DealDamageOverTime(float damage, DamageType damageType, float duration)
    {
        StartCoroutine(DOT(damage, damageType, duration));
    }

    private IEnumerator DOT(float damage, DamageType type, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            if (!alive) yield break;
            DealDamage(damage, type);
            yield return new WaitForSeconds(1f);
        }
    }
}
