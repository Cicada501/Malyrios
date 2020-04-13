using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Malyrios.Items
{
    public class ItemAssets : MonoBehaviour
    {
        public static ItemAssets Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        [SerializeField] private Sprite flower;

        public Sprite Flower => this.flower;
    }
}