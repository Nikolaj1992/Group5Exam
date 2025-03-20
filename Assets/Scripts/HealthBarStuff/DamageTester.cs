using UnityEngine;

public class DamageTester : MonoBehaviour
{
    private HealthHandler healthHandler;

    void Start()
    {
        healthHandler = GetComponent<HealthHandler>();

        if (healthHandler == null)
        {
            Debug.LogError("HealthHandler not found on " + gameObject.name);
        }
    }

 void Update()
{
    if (Input.GetKeyDown(KeyCode.T)) // Take damage
    {
        healthHandler.DealDamage(10, HealthHandler.DamageType.Impact);
        Debug.Log(gameObject.name + " took 10 damage! (test)");
    }

    if (Input.GetKeyDown(KeyCode.H)) // Heal
    {
        healthHandler.health += 10; // This now triggers OnHealthChanged!
        Debug.Log(gameObject.name + " healed 10 HP! (test)");
    }
}


}
