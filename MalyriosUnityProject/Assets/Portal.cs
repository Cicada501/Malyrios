using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Portal : MonoBehaviour , IInteractable
{
    [SerializeField] TextMeshProUGUI interactableText;
    [SerializeField] GameObject teleportPanel;
    bool playerInPortal = false;
    bool interacting = false;

    bool tpPanelOpen = false;
    bool buttonPressed = false;

    //Implement interact Method for interface
    void IInteractable.Interact(){
        OpenTeleportPanel();
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
