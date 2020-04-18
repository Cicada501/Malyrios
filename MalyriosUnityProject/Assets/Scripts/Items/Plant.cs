using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malyrios.Items
{
    [CreateAssetMenu(fileName = "New Plant", menuName = "Items/Plant/NewPlant")]
    public class Plant : BaseItem, IItemDescriber
    {
        public string GetDescription()
        {
            return $"{base.itemName}\n" +
                   $"{base.description}";
        }
    }
}


