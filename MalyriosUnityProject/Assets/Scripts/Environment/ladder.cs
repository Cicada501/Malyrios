using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour
{
    public static bool verticalMovementEnabled = false;
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag =="Player"){
            verticalMovementEnabled = true;
            //horizontalMovementEnabled = false; noch hinzufügen
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag =="Player"){
            verticalMovementEnabled = false;
        }
    }
}
