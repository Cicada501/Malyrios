using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Malyrios.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class Item : ScriptableObject, IPointerDownHandler
    {
        [SerializeField] private string itemName = null;
        [SerializeField] private Sprite icon = null;
        [TextArea(3, 10)]
        [SerializeField] private string description = null;

        public string ItemName => this.itemName;
        public Sprite Icon => this.icon;
        public string Description => this.description;
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("test");
        }
    }
}