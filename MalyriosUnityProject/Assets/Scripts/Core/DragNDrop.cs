﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private GameObject canvasUi;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Transform startParent;
    private Vector3 startPosition;
    
    private void Start()
    {
        this.rectTransform = GetComponent<RectTransform>();
        this.canvasUi = GameObject.FindGameObjectWithTag("CanvasUI");
        this.canvasGroup = GetComponent<CanvasGroup>();
        this.canvas = this.canvasUi.GetComponent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
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
            Debug.Log("ParentChange");
            this.transform.position = this.startPosition;
            this.transform.parent = this.startParent;
        }
    }
}
