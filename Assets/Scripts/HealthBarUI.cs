using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private HealthHandler healthHandler;
    private Transform parentTransform;
    private Vector3 offset = new Vector3(0, 2.5f, 0); // Moves the health bar above the player
    private UnityEngine.UI.Image healthBarFill; // Removed SerializeField to make it dynamic

    private void Awake()
    {
        // Find the HealthBarFill automatically
        healthBarFill = transform.Find("HealthBarBackground/HealthBarFill").GetComponent<UnityEngine.UI.Image>();

        if (healthBarFill == null)
        {
            Debug.LogError("HealthBarFill is missing from the HealthBarUI prefab!");
        }
    }

    public void SetHealthHandler(HealthHandler handler)
    {
        healthHandler = handler;
        parentTransform = handler.transform;
        healthHandler.OnHealthChanged += UpdateHealthBar;
        UpdateHealthBar();
    }

    private void LateUpdate()
    {
        if (parentTransform != null)
        {
            // Keep the health bar above the Player
            transform.position = parentTransform.position + offset;
        }
    }

    private void OnDestroy()
    {
        if (healthHandler != null)
            healthHandler.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        if (healthHandler != null && healthBarFill != null)
        {
            healthBarFill.fillAmount = Mathf.Clamp01(healthHandler.health / 100f);
        }
    }
}
