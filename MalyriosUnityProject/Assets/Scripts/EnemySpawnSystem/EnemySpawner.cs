using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //create a map with enemy and spawntimes for each enemy

    [SerializeField] public List<EnemyToSpawn> enemiesToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var enemyToSpawn in enemiesToSpawn)
        {
            var enemy = enemyToSpawn.enemy;
            foreach (var spawnPoint in enemyToSpawn.spawnPoints)
            {
                Instantiate(enemy, spawnPoint);
            }
        }
    }

    public void Respawn(Enemy.EnemyTypes type, Transform spawnPoint)
    {
        foreach (var enemyToSpawn in enemiesToSpawn)
        {
            if (enemyToSpawn.enemy.GetComponent<Enemy>().enemyType == type)
            {
                StartCoroutine(Spawn(enemyToSpawn.respawnCooldown, enemyToSpawn.enemy, spawnPoint));
            }
        }
    }

    private static IEnumerator Spawn(float cooldown, GameObject enemy, Transform spawnPoint)
    {
        yield return new WaitForSeconds(cooldown);
        Instantiate(enemy, spawnPoint);
        
    }
}