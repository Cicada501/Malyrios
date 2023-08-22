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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        interactableText = ReferencesManager.Instance.interactableText;
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

    public void Interact()
    {
        UseLever();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableText.text = state ? "Disable" : "Activate";
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
