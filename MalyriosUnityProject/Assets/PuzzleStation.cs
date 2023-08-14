using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleStation : MonoBehaviour, IInteractable
{
    [SerializeField] private int slotCount; //muss ungerade sein, da immer 1 True/False slot, dann 1 Operator slot, usw. Darf nicht mit Operator slot aufhören
    private GameObject puzzleWindow;
    private TextMeshProUGUI interactableText;
    private Transform itemSlotsParent;
    [SerializeField] private GameObject itemSlotPrefab;
    
    void Awake()
    {
        interactableText = ReferencesManager.Instance.interactableText;
        puzzleWindow = ReferencesManager.Instance.puzzleWindow;
        itemSlotsParent = ReferencesManager.Instance.itemSlotsParent;
    }
    
    void Update()
    {
        
    }

    public void Interact()
    {
        ShowPuzzleDialog();
    }

    private void ShowPuzzleDialog()
    {
        puzzleWindow.SetActive(true);
        interactableText.gameObject.SetActive(false);
        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(itemSlotPrefab, itemSlotsParent);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableText.text = "Open";
            interactableText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactableText.gameObject.SetActive(false);
            ClosePuzzleWindow();

        }
    }

    private void ClosePuzzleWindow()
    {
        puzzleWindow.SetActive(false);
        foreach (Transform child in itemSlotsParent)
        {
            Destroy(child.gameObject);
        }
    }
}
