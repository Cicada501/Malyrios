using System;
using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using TMPro;
using UnityEngine;
using System.Linq;
using System.Text;
using UnityEngine.UI;

[Serializable]
public class PuzzleStationData
{
    public string id;
    public int[] itemIDsArray;

    public PuzzleStationData(PuzzleStation station)
    {
        id = station.id;
        itemIDsArray = station.itemIDsArray;
    }
}

[Serializable]
public class PuzzleStationDataList
{
    public List<PuzzleStationData> puzzleStationDataList;

    public PuzzleStationDataList(List<PuzzleStationData> data)
    {
        puzzleStationDataList = data;
    }
}

public class PuzzleStation : MonoBehaviour, IInteractable
{
    [HideInInspector]
    public string id;
    private int slotCount;
    [HideInInspector]
    public int[] itemIDsArray;
    private GameObject puzzleWindow;
    private Image puzzleWindowImage;
    private TextMeshProUGUI interactableText;
    private Transform itemSlotsParent;
    [SerializeField] private GameObject itemSlotPrefab;
    private List<PuzzleSlot> slots = new();
    private List<PuzzleElement> puzzleElements;
    private List<GameObject> symbolPrefabs;
    private InventoryUI inventoryUI;
    private bool windowOpen;
    private bool inUse;
    [SerializeField] private Sprite stationTrue;
    [SerializeField] private Sprite stationFalse;
    [SerializeField] private Sprite stationNull;
    [SerializeField] private PuzzleGate gate;

    private bool playerGotPunished;

    private void Awake()
    {
        id = gameObject.name + transform.position;
        puzzleElements = GetComponent<Puzzle>().puzzleElements;
        slotCount = puzzleElements.Count(element => element.elementType == PuzzleElement.ElementType.Empty);
        interactableText = ReferencesManager.Instance.interactableText;
        puzzleWindow = ReferencesManager.Instance.puzzleWindow;
        itemSlotsParent = ReferencesManager.Instance.itemSlotsParent;
        symbolPrefabs = ReferencesManager.Instance.logicSymbols;
        inventoryUI = ReferencesManager.Instance.canvasUI.GetComponent<InventoryUI>();
        itemIDsArray = new int[slotCount];
       
        puzzleWindowImage = puzzleWindow.GetComponent<Image>();
        PuzzleStationManager.Instance.AddStation(this);
        
    }

    private void Start()
    {
        PuzzleStationManager.Instance.LoadStation(this);
        UpdateDisplayedValue();
    }

    public void UpdateDisplayedValue()
    {
        bool? value = GetTruthValue();

        if (value == true)
        {   if(gate!=null) gate.OpenGate();
            this.GetComponent<SpriteRenderer>().sprite = stationTrue;
            if (!inUse) return;
            puzzleWindowImage.color = Color.HSVToRGB(120f / 360f, 0.2f, 1f);
            playerGotPunished = false;
        }
        else if (value == false)
        {
            if(gate!=null) gate.CloseGate();
            this.GetComponent<SpriteRenderer>().sprite = stationFalse;
            if (!inUse) return;
            puzzleWindowImage.color = Color.HSVToRGB(0f, 0.2f, 1f);
            if(!playerGotPunished) PunishWrongAnswer();
        }
        else // value == null
        {
            if(gate!=null) gate.CloseGate();
            this.GetComponent<SpriteRenderer>().sprite = stationNull;
            if (!inUse) return;
            puzzleWindowImage.color = Color.HSVToRGB(0f, 0.0f, 1f);
            playerGotPunished = false;
        }
    }
    
    private void PunishWrongAnswer()
    {
        var player = ReferencesManager.Instance.player;
    
        Vector2 playerPosition = player.transform.position;
        Vector2 stationPosition = this.transform.position;

        Vector2 direction;
        if (playerPosition.x > stationPosition.x)
        {
            // Spieler ist links von der Station, wirf ihn nach rechts oben
            direction = new Vector2(1, 1);
        }
        else
        {
            // Spieler ist rechts von der Station, wirf ihn nach links oben
            direction = new Vector2(-1, 1);
        }
    
        direction.Normalize(); // Normalisiere den Vektor, um seine Länge auf 1 zu setzen

        float forceMagnitude = 200; // Adjust the value based on your specific requirements
        player.GetComponent<PlayerHealth>().TakeDamage(100);
        player.GetComponent<PlayerMovement>().disableMovement = true;
        player.GetComponent<Rigidbody2D>().AddForce(direction * forceMagnitude, ForceMode2D.Force);
        SoundHolder.Instance.wrongAnser.Play();
        playerGotPunished = true;
        StartCoroutine(EnableMovementDelayed(.5f));
    }
    
    IEnumerator EnableMovementDelayed(float time)
    {
        var player = ReferencesManager.Instance.player;

        yield return new WaitForSeconds(time);


        player.GetComponent<PlayerMovement>().disableMovement = false;
    }




    public void Interact()
    {
        inUse = true;
        ShowPuzzleDialog();
        FindObjectOfType<InventoryUI>().SetActivePuzzleStation(this);
    }


    private void ShowPuzzleDialog()
    {
        puzzleWindow.SetActive(true);
        inventoryUI.ChangeInventoryOpened(false);
        windowOpen = true;
        interactableText.gameObject.SetActive(false);
        UpdateDisplayedValue();
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
                    case PuzzleElement.ElementType.Lever:
                        var lever = elem.lever;
                        //if lever state true, show true symbol, else false symbol
                        if (lever.state)
                        {
                            Instantiate(symbolPrefabs[6], itemSlotsParent);
                        }
                        else
                        {
                            Instantiate(symbolPrefabs[7], itemSlotsParent);
                        }
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

        UpdateDisplayedValue();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !windowOpen)
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

    public void ClosePuzzleWindow()
    {
        puzzleWindow.SetActive(false);
        if (InventoryUI.inventoryOpen)
        {
            inventoryUI.ChangeInventoryOpened(false);
        }
        windowOpen = false;
        inUse = false;
        FindObjectOfType<InventoryUI>().SetActivePuzzleStation(null);
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
                case PuzzleElement.ElementType.Lever:
                    if (elem.lever != null) 
                    {
                        formula.Append(elem.lever.state ? "TRUE " : "FALSE ");
                    }
                    else
                    {
                        formula.Append("LEVER_NOT_SET ");
                    }
                    break;

            }
        }

        return formula.ToString().Trim();
    }

    public bool? GetTruthValue()
    {
        string formula = GetCurrentFormula();
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