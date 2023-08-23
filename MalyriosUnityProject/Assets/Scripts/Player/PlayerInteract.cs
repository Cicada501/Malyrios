using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    IInteractable interactable = null;
    void Update(){
        
    }

    public void Interact(){
        interactable = GetClosestInteractable().GetComponent<IInteractable>();
        interactable.Interact();
    }

    
    GameObject GetClosestInteractable(){
        GameObject[] interactables;
        interactables = GameObject.FindGameObjectsWithTag("Interactable");

        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in interactables)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
