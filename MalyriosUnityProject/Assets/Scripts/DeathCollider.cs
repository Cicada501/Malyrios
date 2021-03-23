using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathCollider : MonoBehaviour

{
    [SerializeField] Vector3 spawnPoint =new Vector3(0.0f,0.0f,0.0f);
    Transform player;
    private void Start() {
    player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            StaticData.spawnPoint = spawnPoint;
            player.GetComponent<PlayerHealth>().Die();
        }
    }
}
