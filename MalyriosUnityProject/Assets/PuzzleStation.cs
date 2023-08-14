using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using TMPro;
using UnityEngine;

public class PuzzleStation : MonoBehaviour, IInteractable
{
    [SerializeField] private int slotCount; //muss ungerade sein, da immer 1 True/False slot, dann 1 Operator slot, usw. Darf nicht mit Operator slot aufhören
    private int[] itemIDsArray;
    private GameObject puzzleWindow;
    private TextMeshProUGUI interactableText;
    private Transform itemSlotsParent;
    [SerializeField] private GameObject itemSlotPrefab;
    private List<PuzzleSlot> slots = new();
    
    void Awake()
    {
        interactableText = ReferencesManager.Instance.interactableText;
        puzzleWindow = ReferencesManager.Instance.puzzleWindow;
        itemSlotsParent = ReferencesManager.Instance.itemSlotsParent;
        itemIDsArray = new int[slotCount];
    }

    private void Update()
    {
        string arrayAsString = string.Join(", ", itemIDsArray);
        Debug.Log(arrayAsString);
    }


    public void Interact()
    {
        ShowPuzzleDialog();
    }

    private void ShowPuzzleDialog()
    {
        puzzleWindow.SetActive(true);
        interactableText.gameObject.SetActive(false);
        if (itemSlotsParent.childCount == 0)
        {
            for (int i = 0; i < slotCount; i++)
            {
                var slot= Instantiate(itemSlotPrefab, itemSlotsParent);
                var puzzleSlot = slot.GetComponent<PuzzleSlot>();
                puzzleSlot.SetPuzzleStation(this);
                slots.Add(puzzleSlot);
            }
        }
        
        //check if station contains items already
        if (Array.Exists(itemIDsArray, slot => slot != 0))
        {
            for (int i = 0; i<slotCount; i++)
            {
                //add items to slots
                print($"Slot: {slots[i]}");
                if (itemIDsArray[i] > 1)
                {
                    slots[i].SetItem(ItemDatabase.GetItem(itemIDsArray[i]));
                }
            }
        }
    }
    
    public void UpdateItemID(int index, int itemID)
    {
        print($"Try update item id, index is: {index}, itemID is: {itemID}");
        if (index >= 0 && index < itemIDsArray.Length)
        {
            itemIDsArray[index] = itemID;
        }
        // for (int i = 0; i < slotCount; i++)
        // {
        //     if (slots[i].Item != null)
        //     {
        //         itemIDsArray[i] = slots[i].Item.ItemID;
        //     }
        //     else
        //     {
        //         itemIDsArray[i] = 0;
        //     }
        // }
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
            slots.Remove(child.GetComponent<PuzzleSlot>());
            Destroy(child.gameObject);
        }
    }
}
