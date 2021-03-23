using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMusic : MonoBehaviour
{
    [SerializeField] string changeMusicTo = null;
    // Start is called before the first frame update
    public static string playerInZone;
    void Start(){
        playerInZone = "Normal";
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag =="Player"){
            
            
            if(playerInZone != changeMusicTo){
                playerInZone = changeMusicTo;
            }
          
        }
    }

}
