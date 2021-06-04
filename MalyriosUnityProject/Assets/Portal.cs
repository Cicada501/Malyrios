using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactableText;
    [SerializeField] GameObject teleportPanel;
    bool playerInPortal = false;
    bool interacting = false;

    bool tpPanelOpen = false;
    bool buttonPressed = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
/*         print(buttonPressed);
        buttonPressed = ButtonScript.onInteractClick;


        if(playerInPortal){            
            if(teleportPanel.activeSelf == false && buttonPressed){
                teleportPanel.SetActive(true);
            }
            else if(buttonPressed && teleportPanel.activeSelf == true){
                teleportPanel.SetActive(false);
            }
        } */
    }

    public void OpenTeleportPanel(){
        if(teleportPanel.activeSelf == false){
            teleportPanel.SetActive(true);
        }else{
            teleportPanel.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.tag =="Player"){
                interactableText.text = "teleport";
                interactableText.gameObject.SetActive(true);
                playerInPortal = true;

        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag =="Player"){
                interactableText.gameObject.SetActive(false);
            if(teleportPanel.activeSelf == true){
                teleportPanel.SetActive(false);
                playerInPortal = false;
                
            }
        }
    }
}
