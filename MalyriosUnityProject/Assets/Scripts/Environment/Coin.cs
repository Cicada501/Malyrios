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

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinType coinType;
    private PlayerMoney playerMoney;
    private bool isCollected;

    private void Start()
    {
        playerMoney = ReferencesManager.Instance.player.GetComponent<PlayerMoney>();
        isCollected = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isCollected)
        {
            SoundHolder.Instance.collectCoin.Play();
            playerMoney.AddMoney((int)coinType);
            print($"added {(int)coinType} Money");
            isCollected = true;
            gameObject.SetActive(false);
        }
    }
}