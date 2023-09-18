using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathCollider : MonoBehaviour

{
    PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = ReferencesManager.Instance.player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.Die();
        }
    }
}