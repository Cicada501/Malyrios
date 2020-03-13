using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

	public Slider healthBarSlider;
    public float maxHealth = 100;
    float currentHealth;

    public float flashTime;
    Color origionalColor;
    public SpriteRenderer renderer;
	Transform player;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;
		
        origionalColor = renderer.color;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		healthBarSlider.maxValue = maxHealth;
		healthBarSlider.value = currentHealth;

        if(currentHealth<=0 ||rb.velocity.y < -35f){
            Die();
        }
 
    }
    void FixedUpdate(){
        if(currentHealth<maxHealth){
            currentHealth += 0.01f;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Let player blink red
		FlashRed();

        if (currentHealth <= 50)
        {
			player.GetComponent<Player>().GiveSuperJumps();
            //Die animation 
            //UI asking to restart 
        }
    }

    void FlashRed()
    {
        renderer.color = Color.red;
        Invoke("ResetColor", flashTime);
    }
    void ResetColor()
    {
       
        renderer.color = origionalColor;
    }

    void Die(){

        SceneManager.LoadScene("SampleScene");

    }


}
