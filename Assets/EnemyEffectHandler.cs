using UnityEngine;

public class EnemyEffectHandler : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    private MeshRenderer meshRenderer;
    private Color originalColor;
    private float flashTime = 0.2f;
    private float damageFlashStrength = 0.55f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }
    
    public void ApplyKnockback(Vector3 knockbackDirection, float knockbackForce)
    { 
            rb.AddForce(knockbackDirection * knockbackForce + Vector3.up * (knockbackForce / 3), ForceMode.Impulse);
    }

    public void DamageFlash()
    {
        float brightness = CalculateBrightness(originalColor);
        // If brightness is above a threshold, blend red color with the original color
        if (brightness > 0.35f)
        {
            // Blend red with the original color based on brightness
            Color newColor = Color.Lerp(originalColor, Color.red, damageFlashStrength);
            meshRenderer.material.color = newColor;
        }
        else
        {
            // If the color is dark, overwrite original color with red
            meshRenderer.material.color = Color.red;
        }
        // Resets the color to the original once "flashTime" amount of seconds has passed
        Invoke("ResetColor", flashTime);
    }

    private float CalculateBrightness(Color color)
    {
        return (0.299f * color.r) + (0.587f * color.g) + (0.114f * color.b);
    }

    private void ResetColor()
    {
        meshRenderer.material.color = originalColor;
    }
}
