using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using TMPro;
using UnityEngine;
using System.Linq;

[Serializable]
public class PuzzleStationData
{
    private int id;
    private string level;
}

public class PuzzleStation : MonoBehaviour, IInteractable
{
    private int
        slotCount; //muss ungerade sein, da immer 1 True/False slot, dann 1 Operator slot, usw. Darf nicht mit Operator slot aufhören

    private int[] itemIDsArray;
    private GameObject puzzleWindow;
    private TextMeshProUGUI interactableText;
    private Transform itemSlotsParent;
    [SerializeField] private GameObject itemSlotPrefab;
    private List<PuzzleSlot> slots = new();
    private List<PuzzleElement> puzzleElements;
    [SerializeField] private List<GameObject> symbolPrefabs;

    void Awake()
    {
        puzzleElements = GetComponent<Puzzle>().puzzleElements;
        slotCount = puzzleElements.Count(element => element.elementType == PuzzleElement.ElementType.Empty);
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

    private void Start()
    {
        foreach (var elem in puzzleElements)
        {
            print(elem.elementType);
        }
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
            foreach (var elem in puzzleElements)
            {
                switch (elem.elementType)
                {
                    case PuzzleElement.ElementType.TRUE:
                        Instantiate(symbolPrefabs[0], itemSlotsParent);
                        break;
                    case PuzzleElement.ElementType.FALSE:
                        Instantiate(symbolPrefabs[1], itemSlotsParent);
                        break;
                    case PuzzleElement.ElementType.AND:
                        Instantiate(symbolPrefabs[2], itemSlotsParent);
                        break;
                    case PuzzleElement.ElementType.OR:
                        Instantiate(symbolPrefabs[3], itemSlotsParent);
                        break;
                    case PuzzleElement.ElementType.XOR:
                        Instantiate(symbolPrefabs[4], itemSlotsParent);
                        break;
                    case PuzzleElement.ElementType.IMP:
                        Instantiate(symbolPrefabs[5], itemSlotsParent);
                        break;
                    case PuzzleElement.ElementType.Empty:
                        var slot = Instantiate(itemSlotPrefab, itemSlotsParent);
                        var puzzleSlot = slot.GetComponent<PuzzleSlot>();
                        puzzleSlot.SetPuzzleStation(this);
                        slots.Add(puzzleSlot);
                        break;
                    
                }
            }
            ReferencesManager.Instance.dynamicPuzzleWindowWidth.UpdateContainerWidth();
        }

        //check if station contains items already
        if (Array.Exists(itemIDsArray, slot => slot != 0))
        {
            for (int i = 0; i < slotCount; i++)
            {
                //add items to slots
                if (itemIDsArray[i] > 1)
                {
                    slots[i].SetItem(ItemDatabase.GetItem(itemIDsArray[i]));
                }
            }
        }
    }

    public void UpdateItemID(int index, int itemID)
    {
        if (index >= 0 && index < itemIDsArray.Length)
        {
            itemIDsArray[index] = itemID;
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
            slots.Remove(child.GetComponent<PuzzleSlot>());
            Destroy(child.gameObject);
        }
    }
}