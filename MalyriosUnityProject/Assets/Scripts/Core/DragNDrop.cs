using System.Collections.Generic;
using Malyrios.Core;
using Malyrios.Items;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform rectTransform;
    private GameObject canvasUi;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Transform startParent;
    private Vector3 startPosition;
    
    public ISlot MySlot { get; set; }
    
    private void Start()
    {
        this.rectTransform = GetComponent<RectTransform>();
        this.canvasUi = GameObject.FindGameObjectWithTag("CanvasUI");
        this.canvasGroup = GetComponent<CanvasGroup>();
        this.canvas = this.canvasUi.GetComponent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.rectTransform.anchoredPosition += eventData.delta / this.canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        this.canvasGroup.blocksRaycasts = false;
        this.canvasGroup.alpha = 0.7f;
        this.startParent = this.transform.parent;
        this.startPosition = this.transform.position;
        this.transform.parent = this.canvasUi.transform;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.canvasGroup.blocksRaycasts = true;
        
        if (this.transform.parent == this.canvasUi.transform)
        {
            this.transform.position = this.startPosition;
            this.transform.parent = this.startParent;
            this.canvasGroup.alpha = 1.0f;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            this.transform.parent.GetComponent<IOnSlotRightClick>().OnRightMouseButtonClick();
        }
    }
}
