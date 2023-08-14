using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PuzzleElement
{
    public enum ElementType { TRUE, FALSE, AND, OR, XOR, IMP, Empty }

    public ElementType elementType;
}


public class Puzzle : MonoBehaviour
{
    public List<PuzzleElement> puzzleElements = new();
}
