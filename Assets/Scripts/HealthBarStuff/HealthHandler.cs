using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthHandler : MonoBehaviour
{
    public GameObject healthBarPrefab; // Drag HealthBarUI Prefab here
    private HealthBarUI healthBarUI;
    
    [Range(0, 500)] public float maxHealth = 100;
    private float currentHealth;

    public float health
    {
    get => currentHealth;
    set
        {
        // currentHealth = Mathf.Clamp(value, 0, 100); // Prevents values outside 0-100
        currentHealth = Mathf.Clamp(value, 0, maxHealth); // Prevents values outside 0-100
        OnHealthChanged?.Invoke(); // 🔥 Triggers health bar update
        }
    }
    [HideInInspector] public bool alive;
    public enum DamageType {Piercing, Impact, Elemental} // there is no resistance towards piercing
    [SerializeField] private float generalDamageResistance; // given as 20 for a 20% damage reduction, hence the math in "dealDamage"
    [SerializeField] private float impactResistance;
    [SerializeField] private float elementalResistance;
    private StatusEffectHandler statusEffectHandler;
    
    
    void Start() 
    { 
        currentHealth = maxHealth;
        
        if (healthBarPrefab != null) 
        { 
            // Instantiate the health bar and make it a child of the Player
            GameObject healthBarInstance = Instantiate(healthBarPrefab, transform.position, Quaternion.identity, transform);
            
            // Check if the HealthBarUI component exists before using it
            healthBarUI = healthBarInstance.GetComponent<HealthBarUI>();
            
            if (healthBarUI != null) 
            { 
                healthBarUI.SetHealthHandler(this); 
            }
            else 
            { 
                Debug.LogError("HealthBarUI component is missing on the instantiated prefab!"); 
            } 
        }
        else 
        { 
            Debug.LogError("HealthBarPrefab is not assigned in the Inspector!"); 
        } 
    }
    
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

    public delegate void HealthChanged();
    public event HealthChanged OnHealthChanged;

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
        
        OnHealthChanged?.Invoke(); // Notify health bar UI
        
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
            OnHealthChanged?.Invoke(); // Update health bar each tick
            yield return new WaitForSeconds(1f); 
        } 
    }
}
