using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectHandler : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    // private MeshRenderer meshRenderer;
    // private SkinnedMeshRenderer skinnedMeshRenderer;
    private Color originalColor;
    private float flashTime = 0.2f;
    private float damageFlashStrength = 0.55f;
    
    private List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();
    private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    private List<Color> originalColorsSMR = new List<Color>();
    private List<Color> originalColorsMR = new List<Color>();
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        
        // meshRenderer = GetComponent<MeshRenderer>();
        // originalColor = meshRenderer.material.color;
        
        // skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        // originalColor = skinnedMeshRenderer.material.color;
        
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            skinnedMeshRenderers.Add(skinnedMeshRenderer);
            originalColorsSMR.Add(skinnedMeshRenderer.material.color);
        }

        foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
        {
            meshRenderers.Add(meshRenderer);
            originalColorsMR.Add(meshRenderer.material.color);
        }
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
            // meshRenderer.material.color = newColor;
            // skinnedMeshRenderer.material.color = newColor;
            skinnedMeshRenderers.ForEach(skinnedMeshRenderer => skinnedMeshRenderer.material.color = newColor);
            meshRenderers.ForEach(meshRenderer => meshRenderer.material.color = newColor);
        }
        else
        {
            // If the color is dark, overwrite original color with red
            // meshRenderer.material.color = Color.red;
            // skinnedMeshRenderer.material.color = Color.red;
            skinnedMeshRenderers.ForEach(skinnedMeshRenderer => skinnedMeshRenderer.material.color = Color.red);
            meshRenderers.ForEach(meshRenderer => meshRenderer.material.color = Color.red);
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
        // meshRenderer.material.color = originalColor;
        // skinnedMeshRenderer.material.color = originalColor;
        for (int i = 0; i < skinnedMeshRenderers.Count; i++)
        {
            skinnedMeshRenderers[i].material.color = originalColorsSMR[i];
        }

        for (int i = 0; i < meshRenderers.Count; i++)
        {
            meshRenderers[i].material.color = originalColorsMR[i];
        }
    }
}
