using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathCollider : MonoBehaviour

{
    Transform player;
    private void Start() {
    player = ReferencesManager.Instance.player.transform;
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            player.GetComponent<PlayerHealth>().Die();
        }
    }
}
