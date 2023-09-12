using TMPro;
using UnityEngine;
public class PlayerMoney : MonoBehaviour
{
    private int currentMoney = 0;

    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        UpdateMoneyText();
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "Geld: " + currentMoney.ToString();
    }
}

