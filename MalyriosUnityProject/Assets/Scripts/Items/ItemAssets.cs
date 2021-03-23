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

        [Header("Sprite Assets")]
        [SerializeField] private Sprite flower = null;
        [SerializeField] private Sprite ironSword = null;

        [Header("Prefab Assets")]
        [SerializeField] private GameObject ironSwordPrefab = null;

        public Sprite Flower => this.flower;
        public Sprite IronSword => this.ironSword;

        public GameObject IronSwordPrefab => this.ironSwordPrefab;
    }
}