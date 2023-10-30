using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Character;
using UnityEngine;

public class SetSpawnPoint : MonoBehaviour
{
    private Transform player;
    private BaseAttributes baseAttributes;
    private PlayerHealth playerHealth;

    private float healRate = 100f; // Die Menge an Leben und Mana, die pro Sekunde wiederhergestellt wird.
    private bool isPlayerNearFire = false;

    private void Awake()
    {
        player = ReferencesManager.Instance.player.transform;
        baseAttributes = player.GetComponent<BaseAttributes>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(playerHealth)
            {
                playerHealth.currentSpawnPoint = this.transform;
            }
            
            isPlayerNearFire = true;
            StartCoroutine(HealPlayerOverTime());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerNearFire = false;
        }
    }
    private IEnumerator HealPlayerOverTime() {
        while(isPlayerNearFire) {
            // Stelle sicher, dass das Leben nicht mehr als 3/4 des maximalen Lebens ist
            int healthToHeal = Mathf.Min(20, (baseAttributes.MaxHealth * 3 / 4) - baseAttributes.CurrentHealth);
            int manaToHeal = Mathf.Min(20, 500 - baseAttributes.Mana);

            if(healthToHeal > 0) {
                baseAttributes.CurrentHealth += healthToHeal;
            }

            if(manaToHeal > 0) {
                baseAttributes.Mana += manaToHeal;
            }

            yield return new WaitForSeconds(.5f);
        }
    }

}


