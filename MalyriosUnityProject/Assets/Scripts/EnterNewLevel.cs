using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnterNewLevel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshProUGUI;
    [SerializeField]
    string levelToEnter;
    [SerializeField]
    string displayText;

    

    bool ePressed = false;

    private void Start() {
        textMeshProUGUI.text = displayText;
        
    }

    private void Update() {
        ePressed = Input.GetKey(KeyCode.E);
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            //Player.spawnPoint = (0,0);
            textMeshProUGUI.gameObject.SetActive(true);
            if(ePressed){
                SceneManager.LoadScene(levelToEnter);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
         if(other.gameObject.CompareTag("Player")){
            textMeshProUGUI.gameObject.SetActive(false);
            
        }
    }
    
}
