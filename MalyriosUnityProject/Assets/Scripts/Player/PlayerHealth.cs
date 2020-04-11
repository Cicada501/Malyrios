using System.Collections;
using System.Collections.Generic;
using Malyrios.Character;
using Malyrios.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BaseAttributes))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100;
    [SerializeField] float healthRegen = 0.03f;
    [SerializeField] float DamageBarDropSpeed = 0.8f;
    [SerializeField] float flashTime;
    [SerializeField] SpriteRenderer PlayerRenderer;
    [SerializeField] Slider healthBarSlider;
    [SerializeField] Slider healthBarDamageSlider;
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
        playerOrigionalColor = PlayerRenderer.color;
        barFillOrigionalColor = HealthFill.color;

        UIManager.Instance.SetMaxHealth(this.baseAttributes.MaxHealth);

        healthBarDamageSlider.maxValue = this.baseAttributes.MaxHealth;
        healthBarDamageSlider.value = this.baseAttributes.MaxHealth;

        //healthBarDamageSlider.maxValue = maxHealth;
        //healthBarDamageSlider.value = maxHealth;
        //healthBarSlider.maxValue = maxHealth;
        //tmpCurrentHealth = maxHealth;
    }

    private void Update()
    {
        //healthBarSlider.value = currentHealth;

        //if(currentHealth < tmpCurrentHealth)
        //{
        //    this.tmpCurrentHealth = currentHealth;
        //    CamAnimator.SetTrigger("Landing");
        //}

        //if (currentHealth <= 0 || rb.velocity.y < -35f)
        //{
        //    Die();
        //}
    }

    private void FixedUpdate()
    {
        if (this.baseAttributes.CurrentHealth < this.baseAttributes.MaxHealth)
        {
            this.baseAttributes.CurrentHealth += healthRegen;
        }
        //if (currentHealth < maxHealth)
        //{
        //    currentHealth += healthRegen;
        //}

        //DamageBar
        //if (healthBarDamageSlider.value > currentHealth)
        //{
        //    healthBarDamageSlider.value -= DamageBarDropSpeed;
        //}
        //else if (healthBarDamageSlider.value < currentHealth)
        //{
        //    healthBarDamageSlider.value = currentHealth;
        //}
    }

    public void TakeDamage(int damage)
    {
        this.baseAttributes.CurrentHealth -= damage;
        //currentHealth -= damage;

        ////Let player blink red
        //FlashOnDamage();

        //if (currentHealth <= 50)
        //{
        //    gameObject.GetComponent<Player>().GiveSuperJumps();
        //    //Die animation 
        //    //UI asking to restart 
        //}
    }

    private void FlashOnDamage()
    {
        //HealthFill.color = Color.white;
        //PlayerRenderer.color = Color.red;
        //Invoke("ResetColor", flashTime);
    }

    private void ResetColor()
    {
        //HealthFill.color = barFillOrigionalColor; 
        //PlayerRenderer.color = playerOrigionalColor;
    }

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
