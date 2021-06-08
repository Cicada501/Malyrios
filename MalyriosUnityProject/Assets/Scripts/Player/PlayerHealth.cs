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
        regHealth -= damage/2;

        FlashOnDamage();
        
        if (this.baseAttributes.CurrentHealth <= 0)
        {
            Die();
        }
    }
    
    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        player.spawnPoint2 = getLastSpawnPoint(spawnPoints);
        SavePlayer();
        
    }

    public void Heal(int heal)
    {
        baseAttributes.CurrentHealth += heal;
    }

    Vector3 getLastSpawnPoint(GameObject[] SpawnPoints){
        float mindist = 100000;
        GameObject nearestSpawnPoint = null;
        float[] distances = new float[SpawnPoints.Length];
        for(int i = 0; i < SpawnPoints.Length; i++)
        {
            distances[i] = Vector3.Distance(player.transform.position,SpawnPoints[i].transform.position);
            if(distances[i] < mindist){
                mindist = distances[i];
                nearestSpawnPoint = spawnPoints[i];
            }
        }
        return nearestSpawnPoint.transform.position;
        //lastSpawnPoint = ()
        //spwanpoint = last Spawnpoint
    }

    IEnumerator savePlayerDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        SavePlayer();
        Debug.Log("PlayerSaved");
    }

    public void SavePlayer(){
        SaveSystem.savePlayer(player);
    }
}
