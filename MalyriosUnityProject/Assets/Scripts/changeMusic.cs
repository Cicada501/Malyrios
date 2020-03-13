using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMusic : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool playerInHell = false;
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag =="Player"){
            
            if(!playerInHell){
                playerInHell = true;
            }
            else{
                playerInHell = false;
            }
        }
    }

}
