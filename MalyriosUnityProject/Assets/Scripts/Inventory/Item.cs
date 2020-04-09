using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malyrios.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [TextArea(3, 10)]
        [SerializeField] private string description;

        public string ItemName => this.itemName;
        public Sprite Icon => this.icon;
        public string Description => this.description;
    }
}