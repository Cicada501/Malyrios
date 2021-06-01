using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;

    public PlayerData(Player player){
        
         position = new float[3];
        position[0] = player.spawnPoint2.x;
        position[1] = player.spawnPoint2.y;
        position[2] = player.spawnPoint2.z; 
    }    
}
