using System.Collections;
using System.Collections.Generic;
using Malyrios.Character;
using Malyrios.Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UI;
using UnityEngine.Serialization;

[RequireComponent(typeof(BaseAttributes))]
public class PlayerHealth : MonoBehaviour, IHealthController
{
    [SerializeField] private float healthRegen;
    [SerializeField] private float flashTime;
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private Image healthFill;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject respawnButtionGO;
    [SerializeField] private Animator deathTextAnimator;
    [SerializeField] private Color deathScreenColor;

    private Animator playerAnimator;
    private BaseAttributes baseAttributes;
    private Color playerOrigionalColor;
    private Color barFillOrigionalColor;
    private Transform player;
    
    public Transform currentSpawnPoint;
    public bool isDead;
    public Image deathScreenPanelImage;
    



    // Use this for initialization
    private void Start()
    {
        player = GetComponent<Transform>();
        this.baseAttributes = GetComponent<BaseAttributes>();
        playerAnimator = GetComponent<Animator>();

        //Remember original colors to reset after Flash
        this.playerOrigionalColor = playerRenderer.color;
        this.barFillOrigionalColor = healthFill.color;
        
        deathScreenPanelImage.color = new Color(0.5f, 0.5f, 0.5f, 0f); //Initial Transparency = 0
    }

    private void FlashOnDamage()
    {
        healthFill.color = Color.white;
        playerRenderer.color = Color.red;
        Invoke(nameof(ResetColor), flashTime);
    }

    private void ResetColor()
    {
        healthFill.color = barFillOrigionalColor;
        playerRenderer.color = playerOrigionalColor;
    }

    public void TakeDamage(int damage)
    {
        this.baseAttributes.CurrentHealth -= damage;
        FlashOnDamage();

        if (this.baseAttributes.CurrentHealth <= 0 &&!isDead)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        playerAnimator.SetTrigger("Die");
        deathScreen.SetActive(true);
        player.GetComponent<PlayerMovement>().disableMovement = true;
        deathTextAnimator.Play("youDiedText");
        SoundHolder.Instance.playerDeath.Play();
        StartCoroutine(FadeInDeathScreen());
    }

    public void Heal(int heal)
    {
        baseAttributes.CurrentHealth += heal;
    }
    
    public void Respawn()
    {
        isDead = false;
        playerAnimator.SetTrigger("Respawn");
        respawnButtionGO.SetActive(false);
        deathScreen.SetActive(false);
        player.transform.position = currentSpawnPoint.position;
        baseAttributes.CurrentHealth = baseAttributes.MaxHealth;
        player.GetComponent<PlayerMovement>().disableMovement = false;
    }
    
    private IEnumerator FadeInDeathScreen()
    {
        float elapsedTime = 0f;
        float duration = 2f; // Dauer der Transition in Sekunden

        Color startColor = new Color(deathScreenColor.a,deathScreenColor.b,deathScreenColor.g,0f); // Beginne mit 0 Transparenz
        Color endColor = deathScreenColor; // Endet mit voller Transparenz

        while (elapsedTime < duration)
        {
            deathScreenPanelImage.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        deathScreenPanelImage.color = endColor; // Stelle sicher, dass die Farbe am Ende der Transition korrekt gesetzt ist
        respawnButtionGO.SetActive(true);
       
    }
}