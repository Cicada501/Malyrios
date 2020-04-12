using System.Collections;
using System.Collections.Generic;
using Malyrios.Character;
using Malyrios.Core;
using Malyrios.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BaseAttributes))]
public class PlayerHealth : MonoBehaviour, IHealthController
{
    [SerializeField] float healthRegen = 0.03f;
    [SerializeField] float flashTime;
    [SerializeField] SpriteRenderer PlayerRenderer;
    [SerializeField] Image HealthFill;
    [SerializeField] Animator CamAnimator;

    private Rigidbody2D rb;
    private BaseAttributes baseAttributes;

    private float currentHealth;
    private float tmpCurrentHealth;

    private Color playerOrigionalColor;
    private Color barFillOrigionalColor;


    // Use this for initialization
    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.baseAttributes = GetComponent<BaseAttributes>();

        //Remember original colors to reset after Flash
        this.playerOrigionalColor = PlayerRenderer.color;
        this.barFillOrigionalColor = HealthFill.color;

        UIManager.Instance.SetMaxHealth(this.baseAttributes.MaxHealth);
    }

    private void FixedUpdate()
    {
        if (this.baseAttributes.CurrentHealth < this.baseAttributes.MaxHealth)
        {
            this.baseAttributes.CurrentHealth += healthRegen;
        }
    }

    private void FlashOnDamage()
    {
        HealthFill.color = Color.white;
        PlayerRenderer.color = Color.red;
        Invoke(nameof(ResetColor), flashTime);
    }

    private void ResetColor()
    {
        HealthFill.color = barFillOrigionalColor; 
        PlayerRenderer.color = playerOrigionalColor;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
