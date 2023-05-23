using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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

    #region Singleton

    public static StatsWindow Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one instance of ActiveItemWindow found!");
            return;
        }

        Instance = this;
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        baseAttributes = ReferencesManager.Instance.player.GetComponent<BaseAttributes>();
    }


    public void OnStatsButtonPressed()
    {
        statsWindow.SetActive(!statsWindow.activeSelf);
        if (statsWindow.activeSelf)
        {
            UpdateStatTexts();
        }
    }

    public void UpdateStatTexts()
    {
        healthText.text = baseAttributes.MaxHealth.ToString(CultureInfo.InvariantCulture);
        energyText.text = baseAttributes.Energy.ToString(CultureInfo.InvariantCulture);
        balanceText.text = baseAttributes.Balance.ToString(CultureInfo.InvariantCulture);
        strengthText.text = baseAttributes.Strength.ToString(CultureInfo.InvariantCulture);
        hasteText.text = baseAttributes.Haste.ToString(CultureInfo.InvariantCulture);
    }
}