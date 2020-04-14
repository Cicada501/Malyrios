using Malyrios.Items;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private GameObject canvasUi;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Transform startParent;
    private Vector3 startPosition;
    
    public BaseItem Item { get; set; }
    
    private void Start()
    {
        this.rectTransform = GetComponent<RectTransform>();
        this.canvasUi = GameObject.FindGameObjectWithTag("CanvasUI");
        this.canvasGroup = GetComponent<CanvasGroup>();
        this.canvas = this.canvasUi.GetComponent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition += eventData.delta / this.canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        
        this.canvasGroup.blocksRaycasts = false;
        this.startParent = this.transform.parent;
        this.startPosition = this.transform.position;
        this.transform.parent = this.canvasUi.transform;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        this.canvasGroup.blocksRaycasts = true;
        
        if (this.transform.parent == this.canvasUi.transform)
        {
            this.transform.position = this.startPosition;
            this.transform.parent = this.startParent;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            this.transform.parent.GetComponent<InventorySlot>().OnRightMouseButtonClick();
        }
    }
}
