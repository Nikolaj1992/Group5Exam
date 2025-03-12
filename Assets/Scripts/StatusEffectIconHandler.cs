using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectIconHandler : MonoBehaviour
{
    private Camera playerCamera;
    private float scaleFactor = 0.025f;
    private Canvas canvas;
    private float originalCanvasYPosition;
    private RawImage iconImage;
    private TextMeshProUGUI durationText;
    
    private bool showIcon = false;
    private HealthHandler healthHandler;
    
    void Awake()
    {
        playerCamera = Camera.main;
        canvas = gameObject.GetComponentInChildren<Canvas>();
        originalCanvasYPosition = canvas.transform.localPosition.y;
        iconImage = gameObject.GetComponentInChildren<RawImage>();
        durationText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        healthHandler = gameObject.GetComponentInParent<HealthHandler>();
    }
    
    void Update()
    {
        if (!showIcon && canvas.enabled)
        {
            canvas.enabled = false;
        }
        else if (showIcon && !canvas.enabled)
        {
            canvas.enabled = true;
        }
        if (playerCamera)
        { 
            gameObject.transform.rotation = playerCamera.transform.rotation;
            float camHeight;
            if (playerCamera.orthographic)
            {
                camHeight = playerCamera.orthographicSize * 2;
            }
            else
            {
                float distanceToCamera = Vector3.Distance(playerCamera.transform.position, transform.position);
                camHeight = 2.0f * distanceToCamera * Mathf.Tan(Mathf.Deg2Rad * (playerCamera.fieldOfView * 0.5f));
            }
            float scale = camHeight * scaleFactor;
            if ((scale >= 3)) return;
            transform.localScale = new Vector3(scale, scale, scale);
            canvas.transform.localPosition = new Vector3(0.0f, (originalCanvasYPosition * (scale * scaleFactor)) + originalCanvasYPosition, 0.0f);
        }
    }

    public void ShowIcon(string iconName)
    {
        if (showIcon) return; // makes it unable to respond to new calls if it is already active
        showIcon = true;
        iconImage.texture = StatusEffect.icons[iconName];
        StartCoroutine(CountDown(StatusEffect.premadeStatusEffects[iconName].duration));
    }

    public void HideIcon()
    {
        showIcon = false;
    }

    private IEnumerator CountDown(float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            if (!healthHandler.alive) yield break;
            durationText.text = (duration-i) + "s";
            yield return new WaitForSeconds(1f);
        }
    }
}
