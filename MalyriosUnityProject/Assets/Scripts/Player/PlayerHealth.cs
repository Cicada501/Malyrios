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
    private BaseAttributes baseAttributes;
    [SerializeField] private Animator deathTextAnimator;

    private Color playerOrigionalColor;
    private Color barFillOrigionalColor;
    
    public Transform currentSpawnPoint;
    private Transform player;
    
    public Image deathScreenPanelImage;
    [SerializeField] private Color deathScreenColor;

    public bool isDead;


    // Use this for initialization
    private void Start()
    {
        player = GetComponent<Transform>();
        this.baseAttributes = GetComponent<BaseAttributes>();

        //Remember original colors to reset after Flash
        this.playerOrigionalColor = playerRenderer.color;
        this.barFillOrigionalColor = healthFill.color;

        //UIManager.Instance.SetMaxHealth(this.baseAttributes.MaxHealth);

        //regHealth = baseAttributes.MaxHealth;

        currentSpawnPoint = player.transform;
        
        //deathScreenPanelImage = deathScreen.GetComponent<Image>();
        deathScreenPanelImage.color = new Color(0.5f, 0.5f, 0.5f, 0f); // Setze die anfängliche Transparenz auf 0
    }

    private void FixedUpdate()
    {
        //Debug.Log("Current Health: " + baseAttributes.CurrentHealth);

        if (this.baseAttributes.CurrentHealth < this.baseAttributes.MaxHealth)
        {
            this.baseAttributes.CurrentHealth += (int)healthRegen;
        }

        //Debug.Log("CurrentSpawnPoint: " + currentSpawnPoint);
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
        deathScreen.SetActive(true);
        player.GetComponent<PlayerMovement>().disableMovement = true;
        deathTextAnimator.Play("youDiedText");
        StartCoroutine(FadeInDeathScreen());
    }

    public void Heal(int heal)
    {
        baseAttributes.CurrentHealth += heal;
    }
    
    public void Respawn()
    {
        isDead = false;
        respawnButtionGO.SetActive(false);
        deathScreen.SetActive(false);
        player.transform.position = currentSpawnPoint.position;
        baseAttributes.CurrentHealth = baseAttributes.MaxHealth;
        player.GetComponent<PlayerMovement>().disableMovement = false;
        // Aktiviere die Steuerungen oder andere Dinge, die du beim Respawn reaktivieren möchtest
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