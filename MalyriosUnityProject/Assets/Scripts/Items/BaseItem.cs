using UnityEngine;

namespace Malyrios.Items
{
    public class BaseItem : ScriptableObject
    {
        public enum SpriteTypes
        {
            RedFlower
        }

        [Header("Base Item Properties")] [SerializeField]
        private string itemName;

        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private int sellPrice;
        [SerializeField] private int purchasePrice;
        [SerializeField] private float dropChance;
        [SerializeField] private bool isStackable;
        [SerializeField] private int maxStackAmount;
        [SerializeField] private SpriteTypes spriteType;

        public string ItemName => this.itemName;
        public string Description => description;
        public Sprite Icon => this.icon;
        public int SellPrice => this.sellPrice;
        public int PurchasePrice => this.purchasePrice;
        public float DropChance => this.dropChance;
        public bool IsStackable => this.isStackable;
        public int MaxStackAmount => this.maxStackAmount;
        public SpriteTypes SpriteType => this.spriteType;

        public BaseItem InitItem(string name, string description, SpriteTypes spriteType, bool isStackable = false, int maxStackAmount = 0)
        {
            this.itemName = name;
            this.description = description;
            this.icon = GetSprite(spriteType);
            this.isStackable = isStackable;
            this.maxStackAmount = maxStackAmount;

            return this;
        }

        private Sprite GetSprite(SpriteTypes spriteType)
        {
            switch (spriteType)
            {
                case SpriteTypes.RedFlower:
                    return ItemAssets.Instance.Flower;
                default:
                    return null;
            }
        }
    }
}