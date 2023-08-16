using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using TMPro;
using UnityEngine;
using System.Linq;
using System.Text;

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
    private List<GameObject> symbolPrefabs;
    private GameObject inventoryUI;
    private bool windowOpen;

    void Awake()
    {
        puzzleElements = GetComponent<Puzzle>().puzzleElements;
        slotCount = puzzleElements.Count(element => element.elementType == PuzzleElement.ElementType.Empty);
        interactableText = ReferencesManager.Instance.interactableText;
        puzzleWindow = ReferencesManager.Instance.puzzleWindow;
        itemSlotsParent = ReferencesManager.Instance.itemSlotsParent;
        symbolPrefabs = ReferencesManager.Instance.logicSymbols;
        inventoryUI = ReferencesManager.Instance.inventoryUI;
        itemIDsArray = new int[slotCount];
    }

    private void Update()
    {
        print($"Station {name} has Formula: {GetCurrentFormula()} and truth value: {EvaluateFormula(GetCurrentFormula())}");
    }

    public void Interact()
    {
        ShowPuzzleDialog();
    }

    private void ShowPuzzleDialog()
    {
        puzzleWindow.SetActive(true);
        inventoryUI.SetActive(true);
        windowOpen = true;
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&!windowOpen)
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
        inventoryUI.SetActive(false);
        windowOpen = false;
        foreach (Transform child in itemSlotsParent)
        {
            slots.Remove(child.GetComponent<PuzzleSlot>());
            Destroy(child.gameObject);
        }
    }
    
    public string GetCurrentFormula()
    {
        StringBuilder formula = new StringBuilder();
        int emptySlotIndex = 0;

        foreach (var elem in puzzleElements)
        {
            switch (elem.elementType)
            {
                case PuzzleElement.ElementType.TRUE:
                    formula.Append("TRUE ");
                    break;
                case PuzzleElement.ElementType.FALSE:
                    formula.Append("FALSE ");
                    break;
                case PuzzleElement.ElementType.AND:
                    formula.Append("AND ");
                    break;
                case PuzzleElement.ElementType.OR:
                    formula.Append("OR ");
                    break;
                case PuzzleElement.ElementType.XOR:
                    formula.Append("XOR ");
                    break;
                case PuzzleElement.ElementType.IMP:
                    formula.Append("IMP ");
                    break;
                case PuzzleElement.ElementType.Empty:
                    string value = "";
                    switch (itemIDsArray[emptySlotIndex])
                    {
                        case 20:
                            value = "TRUE";
                            break;
                        case 21:
                            value = "FALSE";
                            break;
                        case 22:
                            value = "AND";
                            break;
                        case 23:
                            value = "OR";
                            break;
                        case 24:
                            value = "XOR";
                            break;
                        case 25:
                            value = "IMP";
                            break;
                        default:
                            value = "Empty";
                            break;
                    }
                    formula.Append(value + " ");
                    emptySlotIndex++;
                    break;
            }
        }

        return formula.ToString().Trim();
    }
    
    public bool? EvaluateFormula(string formula)
    {
        string[] tokens = formula.Split(' ');

        Stack<bool> values = new Stack<bool>();
        Stack<string> ops = new Stack<string>();

        bool expectingValue = true; // Wir erwarten zuerst einen Wert

        for (int i = 0; i < tokens.Length; i++)
        {
            if (tokens[i] == "Empty")
                return null; // Kann nicht bewertet werden, wenn die Formel nicht vollständig ist.

            if (tokens[i] == "TRUE" || tokens[i] == "FALSE")
            {
                if (!expectingValue)
                    return null; // Ungültige Anordnung von Werten

                values.Push(tokens[i] == "TRUE");
                expectingValue = false; // Nächstes Token sollte ein Operator sein
            }
            else if (tokens[i] == "AND" || tokens[i] == "OR" || tokens[i] == "XOR" || tokens[i] == "IMP")
            {
                if (expectingValue)
                    return null; // Ungültige Anordnung von Operatoren

                while (ops.Count > 0 && GetPrecedence(tokens[i]) <= GetPrecedence(ops.Peek()))
                {
                    ApplyOperator(values, ops);
                }

                ops.Push(tokens[i]);
                expectingValue = true; // Nächstes Token sollte ein Wert sein
            }
        }

        if (expectingValue)
            return null; // Ungültige Anordnung am Ende

        while (ops.Count > 0)
        {
            ApplyOperator(values, ops);
        }

        return values.Pop();
    }


    private int GetPrecedence(string op)
    {
        switch (op)
        {
            case "AND": 
                return 3;
            case "OR":  
            case "XOR": 
                return 2;
            case "IMP": 
                return 1;
            default:
                throw new Exception("Ungültiger Operator");
        }
    }


    private void ApplyOperator(Stack<bool> values, Stack<string> ops)
    {
        bool rightValue = values.Pop();
        bool leftValue = values.Pop();
        string op = ops.Pop();

        bool result;

        switch (op)
        {
            case "AND":
                result = leftValue && rightValue;
                break;
            case "OR":
                result = leftValue || rightValue;
                break;
            case "XOR":
                result = leftValue ^ rightValue;
                break;
            case "IMP":
                result = !leftValue || rightValue;
                break;
            default:
                throw new Exception("Ungültiger Operator");
        }

        values.Push(result);
    }


}