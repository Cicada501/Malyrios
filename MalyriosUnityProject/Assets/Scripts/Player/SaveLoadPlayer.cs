using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SaveLoadPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI positionText;

    private Player player;
    private Inventory inventory;
    

    private void Awake()
    {
        LoadPlayer();
    }
    void Start()
    {
        LoadPlayer();
        player = GetComponent<Player>(); 
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SavePlayer(player);
        SaveSystem.SaveInventory(inventory);
        Debug.Log("Saved"+player.gameObject.transform.position);
    }


    public static void SavePlayer(Player player){
        SaveSystem.SavePlayer(player);
    }

    //Called in Start() Should later only be called once at Application start
    public void LoadPlayer(){
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
        positionText.text = position.ToString();
    }
}
