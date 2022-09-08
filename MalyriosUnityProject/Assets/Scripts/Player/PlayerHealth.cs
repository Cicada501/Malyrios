 using System.Collections;
using System.Collections.Generic;
using Malyrios.Character;
using Malyrios.Core;
using Malyrios.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(BaseAttributes))]
public class PlayerHealth : MonoBehaviour, IHealthController
{
    [SerializeField] GameObject[] spawnPoints = null;
    [SerializeField] float healthRegen = 0.0f;
    [SerializeField] float flashTime = 0.0f;
    [SerializeField] SpriteRenderer PlayerRenderer = null;
    [SerializeField] Image HealthFill = null;
    [SerializeField] Image RegHealthFill = null;
    //[SerializeField] Animator CamAnimator = null;

    private Rigidbody2D rb;
    private BaseAttributes baseAttributes;

    private float currentHealth;
    private float tmpCurrentHealth;

    private Color playerOrigionalColor;
    private Color barFillOrigionalColor;

    Player player;

    int regHealth;

    public Transform currentSpawnPoint;


    // Use this for initialization
    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        this.baseAttributes = GetComponent<BaseAttributes>();

        //Remember original colors to reset after Flash
        this.playerOrigionalColor = PlayerRenderer.color;
        this.barFillOrigionalColor = HealthFill.color;

        UIManager.Instance.SetMaxHealth(this.baseAttributes.MaxHealth);

        regHealth = baseAttributes.MaxHealth;

        currentSpawnPoint = player.transform;
    }

    private void FixedUpdate()
    {
        if (this.baseAttributes.CurrentHealth < this.baseAttributes.MaxHealth)
        {
            this.baseAttributes.CurrentHealth += healthRegen;
        }

        //Debug.Log("CurrentSpawnPoint: " + currentSpawnPoint);
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
        regHealth -= damage/2;

        FlashOnDamage();
        
        if (this.baseAttributes.CurrentHealth <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        player.transform.position = currentSpawnPoint.position;
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

    /*
    Vector3 GetClosestSpawnPoint(GameObject[] spawnPoints){
        float mindist = 100000;
        GameObject nearestSpawnPoint = null;
        float[] distances = new float[spawnPoints.Length];
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            distances[i] = Vector3.Distance(player.transform.position,spawnPoints[i].transform.position);
            if(distances[i] < mindist){
                mindist = distances[i];
                nearestSpawnPoint = this.spawnPoints[i];
            }
        }
        return nearestSpawnPoint.transform.position;
    }
    */



    public void SavePlayer(){
        SaveSystem.SavePlayer(player);
    }
}
