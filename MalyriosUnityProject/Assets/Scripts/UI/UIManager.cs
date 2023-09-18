using System;
using Malyrios.Character;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Health UI")]
        [SerializeField] private Slider healthBarSlider = null;
        [SerializeField] private Slider manaBarSlider = null;
        [SerializeField] private RectTransform healthBarRect;
        [SerializeField] private RectTransform healthBarBackgroundRect;
        private float baseWidth = 300;  
        
        private void Awake()
        {
            BaseAttributes.OnCurrentHealthChanged += OnCurrentHealthChanged;
            BaseAttributes.OnManaChanged += OnManaChanged;
            BaseAttributes.OnMaxHealthChanged += OnMaxHealthChanged;
        }

        private void OnDestroy()
        {
            BaseAttributes.OnCurrentHealthChanged -= OnCurrentHealthChanged;
            BaseAttributes.OnManaChanged -= OnManaChanged;
            BaseAttributes.OnMaxHealthChanged -= OnMaxHealthChanged;
        }
        

        public void OnCurrentHealthChanged(float health, int maxHealth)
        {
            this.healthBarSlider.value = health / maxHealth;
        }
        
        public void OnManaChanged(int mana)
        {
            this.manaBarSlider.value = mana / 1000f;
        }


        public void OnMaxHealthChanged(int newMaxHealth)
        {
            //Resize the current health rect (so that slider max value is the same size as the background)
            healthBarRect.sizeDelta = new Vector2(baseWidth * (newMaxHealth / 1000f), healthBarRect.sizeDelta.y);
            healthBarBackgroundRect.sizeDelta = new Vector2(baseWidth * (newMaxHealth / 1000f)+5, healthBarBackgroundRect.sizeDelta.y);
        }
    }
}
