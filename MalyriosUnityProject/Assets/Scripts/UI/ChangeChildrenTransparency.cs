using UnityEngine;
using UnityEngine.UI;

public class ChangeChildrenTransparency : MonoBehaviour
{
    [SerializeField]
    private Slider transparencySlider;

    private void Start()
    {
        transparencySlider.onValueChanged.AddListener(ChangeTransparency);
        ChangeTransparency(transparencySlider.value);
    }

    private void ChangeTransparency(float transparency)
    {
        foreach (Transform child in transform)
        {
            SetTransparency(child, transparency);
        }
    }

    private void SetTransparency(Transform parent, float transparency)
    {
        var graphic = parent.GetComponent<Graphic>();
        if (graphic != null)
        {
            var color = graphic.color;
            color.a = transparency;
            graphic.color = color;
        }
        if (parent.childCount > 0)
        {
            foreach (Transform child in parent)
            {
                SetTransparency(child, transparency);
            }
        }
    }
}