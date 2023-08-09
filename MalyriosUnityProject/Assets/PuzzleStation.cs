using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStation : MonoBehaviour, IInteractable
{
    private int slots; //muss ungerade sein, da immer 1 True/False slot, dann 1 Operator slot, usw. Darf nicht mit Operator slot aufhören
    private GameObject puzzleWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        ShowPuzzleDialog(6);
    }

    private void ShowPuzzleDialog(int slots)
    {
        puzzleWindow.SetActive(true);
    }
}
