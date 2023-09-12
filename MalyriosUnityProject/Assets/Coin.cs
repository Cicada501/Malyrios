using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public enum CoinType
{
    SilverCoin = 1,
    GoldCoin = 5,
    RedCoin = 20,
    YellowGem = 2,
    RedGem = 7,
    GrayGem = 12,
    BlueGem = 14,
    GreenGem = 10
}

public class CoinScript : MonoBehaviour
{
    [SerializeField] private CoinType coinType;
    [SerializeField] private PlayerMoney playerMoney;

    private void Start()
    {
        if(playerMoney == null)
        {
            playerMoney = FindObjectOfType<PlayerMoney>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMoney.AddMoney((int)coinType);
            Destroy(gameObject);
        }
    }
}

