#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Puzzle))]
public class PuzzleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Puzzle puzzle = (Puzzle)target;

        // Länge der Puzzle-Liste bearbeiten
        int newCount = EditorGUILayout.IntField("Puzzle Length", puzzle.puzzleElements.Count);
        while (newCount < puzzle.puzzleElements.Count)
            puzzle.puzzleElements.RemoveAt(puzzle.puzzleElements.Count - 1);
        while (newCount > puzzle.puzzleElements.Count)
            puzzle.puzzleElements.Add(new PuzzleElement());

        // Elemente in der Liste bearbeiten
        for (int i = 0; i < puzzle.puzzleElements.Count; i++)
        {
            puzzle.puzzleElements[i].elementType = (PuzzleElement.ElementType)EditorGUILayout.EnumPopup("Element Type", puzzle.puzzleElements[i].elementType);
        }

        // Änderungen speichern
        if (GUI.changed)
            EditorUtility.SetDirty(puzzle);
    }
}
#endif