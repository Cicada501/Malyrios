using System.Collections;
using System.Collections.Generic;
using Malyrios.Character;
using TMPro;
using UnityEngine;

public class StatsWindow : MonoBehaviour
{
        private BaseAttributes baseAttributes;

        [SerializeField] private GameObject statsWindow;

        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI energyText;
        [SerializeField] private TextMeshProUGUI balanceText;
        [SerializeField] private TextMeshProUGUI strengthText;
        [SerializeField] private TextMeshProUGUI hasteText;
        // Start is called before the first frame update
        void Start()
        {
            baseAttributes = ReferencesManager.Instance.player.GetComponent<BaseAttributes>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void OnStatsButtonPressed()
        {
            statsWindow.SetActive(!statsWindow.activeSelf);
            if (statsWindow.activeSelf)
            {
                //GetCurrentStats();
                //UpdateStats();
            }
        }
}
