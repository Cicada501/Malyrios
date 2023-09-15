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
    
    private BaseAttributes baseAttributes;

    private Color playerOrigionalColor;
    private Color barFillOrigionalColor;
    
    public Transform currentSpawnPoint;
    private Transform player;


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

        if (this.baseAttributes.CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        deathScreen.SetActive(true);
        player.GetComponent<PlayerMovement>().disableMovement = true;
    }

    public void Heal(int heal)
    {
        baseAttributes.CurrentHealth += heal;
    }
    
    public void Respawn()
    {
        deathScreen.SetActive(false);
        player.transform.position = currentSpawnPoint.position;
        baseAttributes.CurrentHealth = baseAttributes.MaxHealth;
        player.GetComponent<PlayerMovement>().disableMovement = false;
        // Aktiviere die Steuerungen oder andere Dinge, die du beim Respawn reaktivieren möchtest
    }
}