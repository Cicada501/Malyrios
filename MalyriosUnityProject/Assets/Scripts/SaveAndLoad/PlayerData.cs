using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;

    public PlayerData(Player player){
        
        position = new float[3];
        var savePosition = player.transform.position;
        position[0] = savePosition.x;
        position[1] = savePosition.y;
        position[2] = savePosition.z; 
    }    
}
