using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyToSpawn
{
    [Header("Add Enemy Prefabs and Spawnpoints")]
    public GameObject enemy;
    public Enemy.EnemyTypes type;
    public float respawnCooldown;
    public List<Transform> spawnPoints;
}
