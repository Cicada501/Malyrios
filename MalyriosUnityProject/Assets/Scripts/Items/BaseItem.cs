using UnityEngine;

namespace Malyrios.Items
{
    public class BaseItem : MonoBehaviour
    {
        [Header("Base Item Properties")] 
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField] private int sellPrice;
        [SerializeField] private int purchasePrice;
        [SerializeField] private float dropChance;
    
        public string ItemName => this.itemName;
        public Sprite Icon => this.icon;
        public int SellPrice => this.sellPrice;
        public int PurchasePrice => this.purchasePrice;
        public float DropChance => this.dropChance;
    }
}


