using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SaveLoadPlayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI positionText;

    Player player;
    private void Awake()
    {
        LoadPlayer();
    }
    void Start()
    {
        LoadPlayer();
        player = GetComponent<Player>();
    }


    static public void SavePlayer(Player player){
        SaveSystem.savePlayer(player);
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
