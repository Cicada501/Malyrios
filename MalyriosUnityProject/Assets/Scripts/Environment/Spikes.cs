using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField] private int damage = 10;
    [SerializeField] private float damageInterval = 0.3f;
    private GameObject player;
    private Coroutine damageCoroutine;
    [SerializeField] private LayerMask playerLayer;
    private int numberOfCollisions;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(DealDamageOverTime(collision.GetComponent<PlayerHealth>()));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& damageCoroutine != null&& numberOfCollisions==0)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private IEnumerator DealDamageOverTime(PlayerHealth playerHealth)
    {
        Collider2D[] results = new Collider2D[2];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(playerLayer);
        contactFilter.useTriggers = true;

        while (true)
        {
            numberOfCollisions = Physics2D.OverlapCollider(GetComponent<Collider2D>(), contactFilter, results);
            bool playerFound = false;

            for (int i = 0; i < numberOfCollisions; i++)
            {
                if (results[i].CompareTag("Player"))
                {
                    playerFound = true;
                    break;
                }
            }

            if (playerFound)
            {
                playerHealth.TakeDamage(damage);
            }
            else
            {
                // Stop the coroutine if the player is no longer inside the spike area
                break;
            }

            yield return new WaitForSeconds(damageInterval);
        }

        damageCoroutine = null;
    }
}


