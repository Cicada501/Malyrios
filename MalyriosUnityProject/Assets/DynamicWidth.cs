using UnityEngine;
using UnityEngine.UI;

public class DynamicWidth : MonoBehaviour
{
    [SerializeField] private RectTransform childB; // Referenz zum Kindobjekt B
    public float widthPerChild; // Die Breite, die für jedes Kindobjekt hinzugefügt werden soll
    public RectTransform container;

    public void UpdateContainerWidth()
    {
        int childCount = childB.transform.childCount; 
        float newWidth = widthPerChild * childCount; // Berechnet die neue Breite basierend auf der Anzahl der Kinder

        // Setzt die neue Breite des Containers
        container.sizeDelta = new Vector2(newWidth, container.sizeDelta.y);
    }
}