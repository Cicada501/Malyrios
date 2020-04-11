using System.Collections;
using System.Collections.Generic;
using Malyrios.Character;
using TMPro;
using UnityEngine;
using Malyrios.Items;
using UnityEngine.UI;

namespace Malyrios.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Serialize Fields Health UI

        [Header("Health UI")]
        [SerializeField] private Slider healthBarSlider;

        #endregion

        #region Serialize Fields Inventory UI

        [Header("Inventory UI")]
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

            BaseAttributes.OnCurrentHealthChanged += OnCurrentHealthChanged;
            BaseAttributes.OnMaxHealthChanged += OnMaxHealthChanged;
        }

        #endregion

        #region Monobehaviour

        private void OnDestroy()
        {
            BaseAttributes.OnCurrentHealthChanged -= OnCurrentHealthChanged;
        }

        #endregion

        #region Attribute Events

        public void OnCurrentHealthChanged(float health)
        {
            this.healthBarSlider.value = health;
            Debug.Log("OnCurrentHealthChanged");
        }

        public void OnMaxHealthChanged(int maxHealth)
        {
            this.healthBarSlider.maxValue = maxHealth;
            Debug.Log("OnMaxHealthChanged");
        }

        #endregion

        public void SetMaxHealth(float maxHealth)
        {
            //this.healthBarSlider.maxValue = maxHealth;
            //this.healthBarSlider.value = maxHealth;
        }

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
