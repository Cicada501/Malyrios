using TMPro;
using UnityEngine;
public class PlayerMoney : MonoBehaviour
{
    #region Singleton
    public static  PlayerMoney Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            print("Playermoney Instanz existiert bereits");
        }
    }
    

    #endregion
    
    
    
    private int currentMoney = 0;

    [SerializeField] private TextMeshProUGUI moneyText;

    public int CurrentMoney
    {
        get { return currentMoney; }
        set
        {
            currentMoney = value;
            UpdateMoneyText();
        }
    }
    

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateMoneyText();
    }
    
    public void RemoveMoney(int amount)
    {
        currentMoney -= amount;
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        moneyText.text = currentMoney.ToString();
    }
}

