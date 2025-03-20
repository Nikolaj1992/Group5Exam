using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    private HealthHandler healthHandler;
    private Transform parentTransform;
    private Vector3 offset = new Vector3(0, 2.5f, 0); // Moves the health bar above the player
    private RectTransform healthBarTransform;
    private float fullWidth; // Stores the original full width of the health bar
    private Camera playerCamera;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        
        // Get HealthBarFill's RectTransform
        healthBarTransform = transform.Find("HealthBarBackground/HealthBarFill").GetComponent<RectTransform>();

        if (healthBarTransform == null)
        {
            Debug.LogError("HealthBarFill is missing from the HealthBarUI prefab!");
        }

        // Store the original full width of the health bar
        fullWidth = healthBarTransform.sizeDelta.x;
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
            transform.rotation = playerCamera.transform.rotation;
        }
    }

    private void OnDestroy()
    {
        if (healthHandler != null)
            healthHandler.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        float normalizedHealth = healthHandler.health / 100f;

        // Set the width of the health bar (scales from the left)
        healthBarTransform.sizeDelta = new Vector2(Mathf.Max(normalizedHealth * fullWidth), healthBarTransform.sizeDelta.y);
    }
}
