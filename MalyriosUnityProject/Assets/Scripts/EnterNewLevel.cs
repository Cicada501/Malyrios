using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnterNewLevel : MonoBehaviour
{
    public Text interactToEnter;
    public string levelToEnter;

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag =="Player"){
            interactToEnter.gameObject.SetActive(true);
            if(Input.GetKey(KeyCode.E)){
                print("got Key");
                SceneManager.LoadScene(levelToEnter);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
         if(other.gameObject.tag =="Player"){
            interactToEnter.gameObject.SetActive(false);
            
        }
    }
    
}
