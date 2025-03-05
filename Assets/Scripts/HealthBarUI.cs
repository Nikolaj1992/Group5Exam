using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
     private HealthHandler healthHandler;
    [SerializeField] private UnityEngine.UI.Image healthBarFill;

 private void Start()
    {
        // Automatically find the HealthHandler on the same GameObject or its parent
        healthHandler = GetComponentInParent<HealthHandler>();

        if (healthHandler == null)
        {
            Debug.LogError("HealthHandler not found on " + gameObject.name);
            return;
        }

        // Subscribe to health change event
        healthHandler.OnHealthChanged += UpdateHealthBar;
        UpdateHealthBar(); // Initialize the bar
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (healthHandler != null)
            healthHandler.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        healthBarFill.fillAmount = healthHandler.health / 100f; // Normalize health
    }
}