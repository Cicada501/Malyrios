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
    [SerializeField] private GameObject[] spawnPoints = null;
    [SerializeField] private float healthRegen;
    [SerializeField] private float flashTime;
    [FormerlySerializedAs("PlayerRenderer")] [SerializeField]
    private SpriteRenderer playerRenderer = null;
    [FormerlySerializedAs("HealthFill")] [SerializeField]
    private Image healthFill = null;
    //[FormerlySerializedAs("RegHealthFill")] [SerializeField] Image regHealthFill = null;
    //[SerializeField] Animator CamAnimator = null;

    private Rigidbody2D rb;
    private BaseAttributes baseAttributes;

    //private float currentHealth;
    //private float tmpCurrentHealth;

    private Color playerOrigionalColor;
    private Color barFillOrigionalColor;

    //int regHealth;

    public Transform currentSpawnPoint;
    private Transform player;


    // Use this for initialization
    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
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
            this.baseAttributes.CurrentHealth += healthRegen;
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
        //regHealth -= damage/2;

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

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Heal(int heal)
    {
        baseAttributes.CurrentHealth += heal;
    }
}
