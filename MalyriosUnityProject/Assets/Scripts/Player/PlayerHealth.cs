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
        player.transform.position = currentSpawnPoint.position;
        baseAttributes.CurrentHealth = baseAttributes.MaxHealth;
        //Delete resources from inventory
        //Respawn bosses
    }

    public void Heal(int heal)
    {
        baseAttributes.CurrentHealth += heal;
    }
}