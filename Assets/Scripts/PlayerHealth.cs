using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;

    public UiHandler uihandler;

    public Image frontHealthBar;
    public Image backHealthBar;
    private bool gmod = false;

    AudioSource audioData;

    void Start() {
        health = maxHealth;
        audioData = GetComponent<AudioSource>();
    }

    void Update() {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        // gmod = Godmode.godmode;
    }

    public void UpdateHealthUI(){
        //debug health amount
        //Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction){
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction){
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        if(health <= 0){
            //uihandler.GetComponent<UiHandler>().Gameover();
        }
    }

    public void ApplyDamage(float damage) {
        if (!gmod){
        //Debug.Log("Player take Damage");
        health -= damage;
        audioData.Play(0);
        lerpTimer = 0f;
            if(health <= 0) {
        // KILL player object
        //Debug.Log("YOU DIED! NOOB");
            }  
        }
    }

    public void RestoreHealth(float healAmount){
        health += healAmount;
        lerpTimer = 0f;
    }

}
