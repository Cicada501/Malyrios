using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    float currentHealth;

    Color playerOrigionalColor;
    Color barFillOrigionalColor;
    Transform player;
    Rigidbody2D rb;
    
    float tmpCurrentHealth;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;

        //Remember original colors to reset after Flash
        playerOrigionalColor = PlayerRenderer.color;
        barFillOrigionalColor = HealthFill.color;

        rb = GetComponent<Rigidbody2D>();

        healthBarDamageSlider.maxValue = maxHealth;
        healthBarDamageSlider.value = maxHealth;
        healthBarSlider.maxValue = maxHealth;
        tmpCurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        healthBarSlider.value = currentHealth;

        if(currentHealth < tmpCurrentHealth){
            tmpCurrentHealth = currentHealth;
            CamAnimator.SetTrigger("Landing");
        }


        if (currentHealth <= 0 || rb.velocity.y < -35f)
        {
            Die();
        }

    }
    void FixedUpdate()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healthRegen;
        }

        //DamageBar
        if (healthBarDamageSlider.value > currentHealth)
        {

            healthBarDamageSlider.value -= DamageBarDropSpeed;
        }else if(healthBarDamageSlider.value < currentHealth){
            healthBarDamageSlider.value = currentHealth;
        }

        

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        //Let player blink red
        FlashOnDamage();

        if (currentHealth <= 50)
        {
            player.GetComponent<Player>().GiveSuperJumps();
            //Die animation 
            //UI asking to restart 
        }
    }
    void ShrinkHealthDamageSlider()
    {

        

    }

    void FlashOnDamage()
    {
        HealthFill.color = Color.white;
        PlayerRenderer.color = Color.red;
        Invoke("ResetColor", flashTime);
    }
    void ResetColor()
    {
        HealthFill.color = barFillOrigionalColor; 
        PlayerRenderer.color = playerOrigionalColor;
    }

    public void Die()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


}
