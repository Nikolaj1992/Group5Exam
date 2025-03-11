using System.Collections;
using UnityEngine;

public class PlayerFadeInEffect : MonoBehaviour
{

    private Renderer[] renderers;
    
    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        StartCoroutine(FadeIn(1f));     // Player "fades-in" in 1 second, we can adjust later once animation is done
    }
    
    private IEnumerator FadeIn(float duration)
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            float alpha = elapsedTime / duration;   // Gradually increase alpha until our player is fully visible
            SetAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        SetAlpha(1f);
    }
    
    private void SetAlpha(float alpha)
    {
        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    Color color = mat.color;
                    color.a = alpha;
                    mat.color = color;
                }
            }
        }
    }

}
