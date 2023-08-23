using UnityEngine;
using UnityEngine.UI;

public class DynamicWidth : MonoBehaviour
{
    [SerializeField] private RectTransform childB; // Referenz zum Kindobjekt B
    private float widthPerChild = 55f; //Width = 50, Spacing = 5
    public RectTransform container;

    public void UpdateContainerWidth()
    {
        int childCount = childB.transform.childCount; 
        float allSymbolsWidth = widthPerChild * childCount -5f; //always -5f because its always one spacing less than childCount
        
        float newWidth = allSymbolsWidth + 40f;
        if (newWidth < 150f)
        {
            newWidth = 150f;
        }

        // Setzt die neue Breite des Containers
        container.sizeDelta = new Vector2(newWidth, container.sizeDelta.y);
    }
}