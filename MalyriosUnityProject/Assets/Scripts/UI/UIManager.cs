using System;
using Malyrios.Character;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        #region Serialize Fields Health UI

        [Header("Health UI")]
        [SerializeField] private Slider healthBarSlider = null;
        [SerializeField] private RectTransform healthBarRect;
        [SerializeField] private RectTransform healthBarBackgroundRect;
        private float baseWidth = 300;  
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
            BaseAttributes.OnMaxHealthChanged -= OnMaxHealthChanged;
        }

        #endregion

        #region Attribute Events

        public void OnCurrentHealthChanged(float health, int maxHealth)
        {
            this.healthBarSlider.value = health / maxHealth;
        }

        #endregion

        public void OnMaxHealthChanged(int newMaxHealth)
        {
            print($"chaniging Max Health to {ReferencesManager.Instance.player.GetComponent<BaseAttributes>().MaxHealth}");
            //healthBarSlider.maxValue = newMaxHealth;
            //healthBarSlider.value = newMaxHealth;
            healthBarRect.sizeDelta = new Vector2(baseWidth * (newMaxHealth / 1000f), healthBarRect.sizeDelta.y);
            healthBarBackgroundRect.sizeDelta = new Vector2(baseWidth * (newMaxHealth / 1000f)+5, healthBarBackgroundRect.sizeDelta.y);
        }
    }
}
