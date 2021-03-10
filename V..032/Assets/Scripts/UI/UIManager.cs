using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Malyrios.Items;

namespace Malyrios.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Serialie Fields

        [SerializeField] private GameObject tooltip;

        #endregion

        #region Singleton

        public static UIManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }


        #endregion

        public void ShowTooltip(Vector3 position, Item item)
        {
            this.tooltip.SetActive(true);
            this.tooltip.transform.position = position;
            this.tooltip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{ item.ItemName }\n{ item.Description }";
        }

        public void HideTooltip()
        {
            this.tooltip.SetActive(false);
        }
    }
}
