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
        }

        #endregion

        #region Monobehaviour

        private void OnDestroy()
        {
            BaseAttributes.OnCurrentHealthChanged -= OnCurrentHealthChanged;
        }

        #endregion

        #region Attribute Events

        public void OnCurrentHealthChanged(float health, int maxHealth)
        {
            this.healthBarSlider.value = health / maxHealth;
        }

        #endregion

        public void SetMaxHealth(float maxHealth)
        {
            throw new NotImplementedException();
        }
    }
}
