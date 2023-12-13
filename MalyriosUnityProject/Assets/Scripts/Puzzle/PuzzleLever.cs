using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleLever : MonoBehaviour, IInteractable
{
    [HideInInspector]
    public bool state;
    private Animator animator;
    private TextMeshProUGUI interactableText;
    [SerializeField] private PuzzleStation station;
    public string leverID;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        interactableText = ReferencesManager.Instance.interactableText;
        //leverID = gameObject.name + transform.position.x;
    }
     void UseLever()
    {
        state = !state;
        if (state)
        {
            animator.Play("Activate");
        }
        else
        {
            animator.Play("Disable");
        }
        station.UpdateDisplayedValue();

    }

     public void ApplyLoadedState()
     {
         if (state)
         {
             animator.Play("Activate");
         }
         station.UpdateDisplayedValue();
     }

    public void Interact()
    {
        UseLever();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableText.text = state ? "Deaktiviern" : "Aktivieren";
            interactableText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableText.gameObject.SetActive(false);
        }
    }
}

[System.Serializable]
public class LeverData
{
    public bool state;
    public string leverID;
}

[System.Serializable]
public class LeverDataList
{
    public List<LeverData> leverDataList = new List<LeverData>();
}

